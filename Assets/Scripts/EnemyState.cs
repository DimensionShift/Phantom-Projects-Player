using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float damage = 20;
    [SerializeField] TextMeshProUGUI healthText;
    private RectTransform uiHealth;

    PlayerState ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PlayerState>();

        health = maxHealth;
        healthText.text = (int)health + " / " + maxHealth;
        uiHealth = GameObject.Find("EnemyHealth").GetComponent<RectTransform>();
        UpdateHealthbar();
    }

    private void Update()
    {
        healthText.text = (int)health + " / " + maxHealth;
    }

    public void UpdateHealthbar()
    {
        float x = health / maxHealth;
        float y = uiHealth.localScale.y;
        float z = uiHealth.localScale.z;
        uiHealth.localScale = new Vector3(x, y, z);
    }

    public void NullifyDamage()
    {
        damage = 0;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateHealthbar();

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
