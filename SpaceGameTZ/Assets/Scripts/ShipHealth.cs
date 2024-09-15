using System.Collections;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Debug.Log("Корабль уничтожен!");
        }

        GameUI.Instance.UpdateHealthUI(currentHealth, maxHealth);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
