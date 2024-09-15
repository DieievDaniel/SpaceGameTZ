using System.Collections;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance; 

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateHealthUI(100, 100);
        damageText.gameObject.SetActive(false); 
    }

    public void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        healthText.text = $"Health: {currentHealth}/{maxHealth}";
    }

    public void ShowDamage(float damage)
    {
        damageText.gameObject.SetActive(true);
        damageText.text = $"-{damage}% health";
        StartCoroutine(HideDamageText());
    }

    private IEnumerator HideDamageText()
    {
        yield return new WaitForSeconds(2f); 
        damageText.gameObject.SetActive(false); 
    }
}
