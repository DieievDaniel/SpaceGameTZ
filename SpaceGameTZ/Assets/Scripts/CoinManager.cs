using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    private int coinCount;

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
        LoadCoins();
    }
    private void LoadCoins()
    {
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);
        GameUI.Instance.UpdateCoinUI(coinCount); 
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        PlayerPrefs.SetInt("CoinCount", coinCount);
        PlayerPrefs.Save();
        GameUI.Instance.UpdateCoinUI(coinCount); 
    }

    public int GetCoinCount()
    {
        return coinCount;
    }
}
