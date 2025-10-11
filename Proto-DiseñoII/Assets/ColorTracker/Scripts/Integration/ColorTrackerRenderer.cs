using UnityEngine;
using System.Collections.Generic;
using System;

namespace Vexpot.Integration
{
    [AddComponentMenu("Vexpot/ColorTrackerRenderer")]
    public class ColorTrackerRenderer : MonoBehaviour
    {
        public Canvas canvas;
        public GameObject graphicModel;
        public ColorTrackerPanel trackerPanel;

        private ColorTracker _tracker;
        private List<GameObject> _graphics;
        private Vector3 _reusableScreenPosition;

        // 👇 Variables nuevas
        private TouchableItem grabbedItem;
        private Camera mainCamera;

        void Awake()
        {
            mainCamera = Camera.main;
            if (ValidateComponents())
            {
                _graphics = new List<GameObject>();
                _reusableScreenPosition = new Vector3();
            }
        }

        private bool ValidateComponents()
        {
            Camera cam = GetComponent<Camera>();
            if (cam == null || !cam.enabled)
            {
                Debug.LogError("OnPostRender function will never be called due Camera component is missing or inactive in the parent GameObject");
                return false;
            }

            if (Camera.main == null)
            {
                Debug.LogError("At least one camera must be tagged as main camera");
                return false;
            }

            if (trackerPanel == null)
            {
                Debug.LogError("You must link a ColorTrackerPanel instance to this component");
                return false;
            }

            if (canvas == null)
            {
                Debug.LogError("You must define the canvas to render the tracks");
                return false;
            }

            if (graphicModel == null)
            {
                Debug.LogError("You must define the graphic to render the tracks");
                return false;
            }

            return true;
        }

        private GameObject CreateGraphic()
        {
            GameObject obj = Instantiate(graphicModel) as GameObject;
            obj.transform.SetParent(canvas.transform, false);
            obj.SetActive(false);
            return obj;
        }

        private void DestroyPreviousGraphics()
        {
            for (var gindex = 0; gindex < _graphics.Count; gindex++)
            {
                Destroy(_graphics[gindex]);
            }
            _graphics.Clear();
        }

        private void CreateNewGraphics(int count)
        {
            for (var gindex = 0; gindex < count; gindex++)
            {
                _graphics.Add(CreateGraphic());
            }
        }

        void OnDestroy()
        {
            DestroyPreviousGraphics();
        }

        void OnPostRender()
        {
            if (!trackerPanel) return;

            _tracker = trackerPanel.GetColorTracker();

            if (_tracker == null || Camera.main == null) return;

            if (!_tracker.isRunning)
            {
                DestroyPreviousGraphics();
                return;
            }

            List<TrackerResult> result = _tracker.Compute();

            if (_graphics.Count != result.Count)
            {
                DestroyPreviousGraphics();
                CreateNewGraphics(result.Count);
            }

            for (var i = 0; i < result.Count; i++)
            {
                GameObject actual = _graphics[i];
                TrackerResult target = result[i];
                if (target.state == TrackingState.Tracked)
                {
                    CoordinateMapper.ConvertInputToScreen(_tracker.input, target.center, ref _reusableScreenPosition);
                    CoordinateMapper.ConvertScreenToUI(_reusableScreenPosition, actual.GetComponent<RectTransform>());
                    actual.SetActive(true);

                    // 👇 Detección y movimiento del objeto (solo X e Y)
                    if (i == 0) // usar solo el primer puntero
                    {
                        Ray ray = mainCamera.ScreenPointToRay(_reusableScreenPosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit))
                        {
                            var touchable = hit.collider.GetComponent<TouchableItem>();
                            if (touchable != null)
                            {
                                if (grabbedItem == null)
                                    grabbedItem = touchable;

                                if (grabbedItem != null)
                                {
                                    // Convertir a coordenadas de mundo manteniendo la profundidad original (Z)
                                    float distance = Vector3.Distance(mainCamera.transform.position, grabbedItem.transform.position);
                                    Vector3 worldPoint = mainCamera.ScreenToWorldPoint(
                                        new Vector3(
                                            _reusableScreenPosition.x,
                                            _reusableScreenPosition.y,
                                            distance
                                        )
                                    );

                                    // 🔹 Movimiento limitado a X e Y
                                    grabbedItem.transform.position = new Vector3(
                                        worldPoint.x,
                                        worldPoint.y,
                                        grabbedItem.transform.position.z
                                    );
                                }
                            }
                            else
                            {
                                grabbedItem = null;
                            }
                        }
                        else
                        {
                            grabbedItem = null;
                        }
                    }
                }
                else
                {
                    actual.SetActive(false);
                }
            }
        }
    }
}
