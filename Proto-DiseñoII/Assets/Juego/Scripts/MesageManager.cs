using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MesageManager : MonoBehaviour
{
    [Header("ReferenciaS UI")]
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Image teacherImage;

    [Header("Duración del mensaje (en segundos)")]
    [SerializeField] private float messageDuration = 10f;

    private void Start()
    {
        // Mostrar mensaje al iniciar la escena
        ShowMessage("Agarra un color y usa el puntero para agarrar las letras y llevarlas a los contenedores vacios, mientras menos errores tengas mas puntos tendrás e intenta tu mejor tiempo.");
    }

    public void ShowMessage(string message)
    {
        // Activar el objeto de texto y mostrar mensaje
        messageText.gameObject.SetActive(true);
        if (teacherImage != null)
            teacherImage.gameObject.SetActive(true);

        messageText.text = message;

        // Iniciar corrutina para ocultarlo después de un tiempo
        StartCoroutine(HideMessageAfterDelay());
    }

    private System.Collections.IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        messageText.gameObject.SetActive(false);
        if (teacherImage != null)
            teacherImage.gameObject.SetActive(false);
    }
}
