using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Referencia al texto UI que muestra el puntaje")]
    public TextMeshProUGUI scoreText;

    private int currentScore = 0;

    void Start()
    {
        UpdateScoreText();
    }

    public void AddPoints(int amount)
    {
        currentScore += amount;
        UpdateScoreText();

        Debug.Log($"✅ Correcto +{amount} puntos | Total: {currentScore}");
    }

    public void SubtractPoints(int amount)
    {
        currentScore -= amount;
        UpdateScoreText();

        Debug.Log($"❌ Incorrecto -{amount} puntos | Total: {currentScore}");
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Puntaje: " + currentScore.ToString();
    }

    // Opcional, por si querés reiniciar el puntaje
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreText();
    }

    // Por si después querés consultar el puntaje actual
    public int GetScore()
    {
        return currentScore;
    }
}
