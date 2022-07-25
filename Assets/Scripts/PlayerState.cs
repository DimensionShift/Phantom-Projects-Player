using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    #region Variables

    [Header("Player Stats")]
    [Space]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy = 100;
    [SerializeField] private float weaponDamage = 20;
    [SerializeField] private float unstableChargeDamage = 35;

    [Header("Text Fields")]
    [Space]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI energyText;

    private RectTransform uiHealth;
    private RectTransform uiEnergy;

    private Coroutine energyRegen;
    //private Coroutine healthRegen;

    #endregion

    void Start()
    {
        health = maxHealth;
        healthText.text = (int)health + " / " + maxHealth;
        uiHealth = GameObject.Find("Health").GetComponent<RectTransform>();
        UpdateHealthbar();

        energy = maxEnergy;
        energyText.text = (int)energy + " / " + maxEnergy;
        uiEnergy = GameObject.Find("Energy").GetComponent<RectTransform>();
        UpdateEnergyBar();
    }

    void Update()
    {
        IsDead();
        healthText.text = (int)health + " / " + maxHealth;
        energyText.text = (int)energy + " / " + maxEnergy;
    }

    public void IsDead()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    #region Health Methods

    public void UpdateHealthbar()
    {
        float x = health / maxHealth;
        float y = uiHealth.localScale.y;
        float z = uiHealth.localScale.z;
        uiHealth.localScale = new Vector3(x, y, z);
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealth(float h)
    {
        if (h < 0) health = 0;
        else if (h > maxHealth) health = maxHealth;
        else health = h;
        UpdateHealthbar();
    }

    public void IncreaseHealth(float h)
    {
        SetHealth(health + h);
    }

    public void ReduceHealth(float h)
    {
        SetHealth(health - h);

        /*
        if (healthRegen != null)
        {
            StopCoroutine(healthRegen);
        }

        healthRegen = StartCoroutine(HealthRegen());
        */
    }

    /*private IEnumerator HealthRegen()
    {
        yield return new WaitForSeconds(5f);

        while (health <= maxHealth)
        {
            yield return new WaitForSeconds(3f);

            IncreaseEnergy(1);
        }
    }
    */

    #endregion

    #region Energy Methods

    public void UpdateEnergyBar()
    {
        float x = energy / maxEnergy;
        float y = uiEnergy.localScale.y;
        float z = uiEnergy.localScale.z;
        uiEnergy.localScale = new Vector3(x, y, z);
    }

    public float GetCurrentEnergy()
    {
        return energy;
    }

    public float GetMaxEnergy()
    {
        return maxEnergy;
    }

    public void SetEnergy(float e)
    {
        if (e < 0) e = 0;
        else if (e > maxEnergy) energy = maxEnergy;
        else energy = e;
        UpdateEnergyBar();
    }

    public void IncreaseEnergy(float e)
    {
        SetEnergy(energy + e);
    }

    public void ReduceEnergy(float e)
    {
        SetEnergy(energy - e);

        if (energyRegen != null)
        {
            StopCoroutine(energyRegen);
        }

        energyRegen = StartCoroutine(EnergyRegen());
    }

    private IEnumerator EnergyRegen()
    {
        yield return new WaitForSeconds(5f);

        while (energy <= maxEnergy)
        {
            yield return new WaitForSeconds(0.5f);

            IncreaseEnergy(2);
        }
    }

    #endregion

    #region Damage Methods

    public float GetWeaponDamage()
    {
        return weaponDamage;
    }

    public float GetUnstableChargeDamage()
    {
        return unstableChargeDamage;
    }

    public void DamageIncrease(float amount)
    {
        weaponDamage += amount;
    }

    #endregion
}