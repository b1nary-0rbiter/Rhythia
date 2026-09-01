// HUD manager for hearts, score, and multiplier display
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI heartsText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    public GameObject heartsPanel;
    
    [Header("State")]
    public int currentHearts = 3;
    public int currentScore = 0;
    public int currentMultiplier = 1;
    
    void Start()
    {
        UpdateHUD();
    }
    
    void Update()
    {
        // Update hearts display
        if (heartsText != null)
        {
            heartsText.text = "Hearts: " + currentHearts;
        }
        
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
        
        if (multiplierText != null)
        {
            multiplierText.text = "Multiplier: x" + (currentMultiplier > 0 ? currentMultiplier : 1);
        }
        
        // Game over check
        if (currentHearts <= 0)
        {
            // TODO: Trigger game over
        }
    }
    
    public void UpdateHUD()
    {
        if (heartsText != null)
        {
            heartsText.text = "Hearts: " + currentHearts;
        }
        
        if (multiplierText != null)
        {
            multiplierText.text = "Multiplier: x" + (currentMultiplier > 0 ? currentMultiplier : 1);
        }
    }
    
    public void TakeHeartDamage()
    {
        currentHearts--;
        UpdateHUD();
    }
}