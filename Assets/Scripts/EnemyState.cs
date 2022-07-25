using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    // Enemy stats
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float damage = 20;
    [SerializeField] TextMeshProUGUI healthText;

    // Enemy HealthBar
    private RectTransform uiHealth;

    PlayerState ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PlayerState>();                         // Get PlayerState script

        health = maxHealth;                                                                 // Set enemies health to their maximum health
        healthText.text = (int)health + " / " + maxHealth;
        uiHealth = GameObject.Find("EnemyHealth").GetComponent<RectTransform>();
        UpdateHealthbar();                                                                  // Update the healthbar to show the enemy's health
    }

    private void Update()
    {
        healthText.text = (int)health + " / " + maxHealth;                                  // Constantly update the health text
    }

    public void UpdateHealthbar()                                                           // Method for scaling the healthbar
    {
        float x = health / maxHealth;
        float y = uiHealth.localScale.y;
        float z = uiHealth.localScale.z;
        uiHealth.localScale = new Vector3(x, y, z);
    }

    public void NullifyDamage()                                                             // Method to nullify the enemy's damage (used for the shield)
    {
        damage = 0;
    }

    public float GetDamage()                                                                // Method to get enemy damage
    {
        return damage;
    }

    public void TakeDamage(float amount)                                                    // Method for the enemy to take damage
    {
        health -= amount;
        UpdateHealthbar();

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
