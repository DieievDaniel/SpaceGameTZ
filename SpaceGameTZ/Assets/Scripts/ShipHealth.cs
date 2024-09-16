using System.Collections;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject[] objectsToDeactivate;
    [SerializeField] private GameObject losePopup;
    [SerializeField] private GameObject camera;
    private float currentHealth;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem lowHealthParticles; 
    [SerializeField] private ParticleSystem criticalHealthParticles; 

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthEffects();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            losePopup.SetActive(true);
            camera.SetActive(true);
            DeactivateObjects();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        GameUI.Instance.UpdateHealthUI(currentHealth, maxHealth);
        UpdateHealthEffects();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private void UpdateHealthEffects()
    {
        if (currentHealth <= maxHealth * 0.5f)
        {
            if (!criticalHealthParticles.isPlaying)
            {
                criticalHealthParticles.Play();
            }
        }
        else
        {
            if (criticalHealthParticles.isPlaying)
            {
                criticalHealthParticles.Stop();
            }
        }

        if (currentHealth <= maxHealth * 0.9f) 
        {
            if (!lowHealthParticles.isPlaying)
            {
                lowHealthParticles.Play();
            }
        }
        else
        {
            if (lowHealthParticles.isPlaying)
            {
                lowHealthParticles.Stop();
            }
        }
    }
    public void DeactivateObjects()
    {
        foreach (var obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }
}
