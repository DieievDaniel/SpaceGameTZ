using UnityEngine;
using TMPro;
using System.Collections;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI coinText;

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
        if (gameObject.activeInHierarchy)
        {
            UpdateHealthUI(100, 100);
            damageText.gameObject.SetActive(false);
            UpdateCoinUI(CoinManager.Instance.GetCoinCount());
        }
    }

    public void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        if (gameObject.activeInHierarchy && healthText.gameObject.activeInHierarchy)
        {
            healthText.text = $"Health: {currentHealth}/{maxHealth}";
        }
    }

    public void ShowDamage(float damage)
    {
        if (gameObject.activeInHierarchy && damageText.gameObject.activeInHierarchy)
        {
            damageText.gameObject.SetActive(true);
            damageText.text = $"-{damage}% health";
            StartCoroutine(HideDamageText());
        }
    }

    private IEnumerator HideDamageText()
    {
        yield return new WaitForSeconds(2f);

        if (gameObject.activeInHierarchy && damageText.gameObject.activeInHierarchy)
        {
            damageText.gameObject.SetActive(false);
        }
    }

    public void UpdateCoinUI(int coinCount)
    {
        if (gameObject.activeInHierarchy && coinText.gameObject.activeInHierarchy)
        {
            coinText.text = $"Coins: {coinCount}";
        }
    }
}
