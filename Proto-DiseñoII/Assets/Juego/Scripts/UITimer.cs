using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    [Header("Referencia al texto del timer en el Canvas")]
    public TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private bool isRunning = true; 

    void Update()
    {
        if (!isRunning) return;

        // Aumenta el tiempo
        elapsedTime += Time.deltaTime;

        // Convierte a minutos y segundos
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Actualiza el texto en formato mm:ss
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Para controlar el timer más adelante
    public void ResetTimer()
    {
        elapsedTime = 0f;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}
