using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerState ps;

    //Shield Ability
    [Header("Ability 1: Shield")]
    [Space]
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shieldDurationUITimer;
    [SerializeField] private GameObject shieldCooldownUITimer;

    private TextMeshProUGUI shieldDurationText;
    private TextMeshProUGUI shieldCooldownText;

    private float shieldDuration = 5.5f;
    private float shieldCooldown = 20.5f;
    private float shieldDurationCounter;
    private float shieldCooldownCounter;
    private bool shieldReady = true;
    private bool shieldActive = false;

    [Header("Ability 2: Unstable Charge")]
    [Space]
    [SerializeField] GameObject energyProjectilePrefab;
    [SerializeField] int chargeEnergyCost = 20;
    [SerializeField] float attackInterval = 0.5f;
    float attackTimer = 0;

    private void Start()
    {
        shieldDurationText = shieldDurationUITimer.GetComponentInChildren<TextMeshProUGUI>();
        shieldCooldownText = shieldCooldownUITimer.GetComponentInChildren<TextMeshProUGUI>();

        shieldDurationCounter = shieldDuration;
        shieldCooldownCounter = shieldCooldown;
    }

    private void Update()
    {
        if (shieldReady)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                shield.SetActive(true);
                shieldReady = false;
                shieldActive = true;

                StartCoroutine(ShieldDuration());
            }
        }
        else if (!shieldReady && !shieldActive)
        {
            ShieldCooldownTimer();
        }

        if (shieldActive)
        {
            ShieldDurationTimer();
        }

        if (attackTimer <= 0)
        {
            attackTimer = 0;
            UnstableCharge();
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            collision.GetComponent<EnemyState>().NullifyDamage();
        }
    }

    IEnumerator ShieldDuration()
    {
        shieldDurationUITimer.SetActive(true);

        yield return new WaitForSecondsRealtime(shieldDuration);

        shieldDurationUITimer.SetActive(false);
        shield.SetActive(false);
        shieldActive = false;
        shieldCooldownCounter = shieldCooldown;

        StartCoroutine(ShieldCooldown());
    }

    private void ShieldDurationTimer()
    {
        shieldDurationCounter -= Time.deltaTime;
        shieldDurationText.text = "Shield Duration: " + (int)shieldDurationCounter;

        if (shieldDurationCounter < 0)
        {
            shieldDurationCounter = 0;
        }
    }

    private void ShieldCooldownTimer()
    {
        shieldCooldownCounter -= Time.deltaTime;
        shieldCooldownText.text = "Shield Cooldown: " + (int)shieldCooldownCounter;

        if (shieldCooldownCounter < 0)
        {
            shieldCooldownCounter = 0;
        }
    }

    IEnumerator ShieldCooldown()
    {
        shieldCooldownUITimer.SetActive(true);

        yield return new WaitForSecondsRealtime(shieldCooldown);

        shieldCooldownUITimer.SetActive(false);
        shieldReady = true;
        shieldDurationCounter = shieldDuration;
    }

    public void ShieldDurationIncrease(float amount)
    {
        shieldDuration += amount;
    }

    public void ShieldCooldownDecrease(float amount)
    {
        shieldCooldown += amount;
    }

    private void UnstableCharge()
    {
        if (Input.GetMouseButtonDown(1) && ps.GetCurrentEnergy() >= chargeEnergyCost)
        {
            if (!controller.facingRight)
            {
                SpawnProjectile(-0.5f, Vector2.left);
                ps.ReduceEnergy(chargeEnergyCost);
            }
            else
            {
                SpawnProjectile(0.5f, Vector2.right);
                ps.ReduceEnergy(chargeEnergyCost);
            }

            attackTimer = attackInterval;
        }
    }

    private void SpawnProjectile(float num, Vector2 direction)
    {
        Vector2 spawnEnergyProjectile = new Vector2(transform.position.x + num, transform.position.y);
        GameObject instance = Instantiate(energyProjectilePrefab, spawnEnergyProjectile, Quaternion.identity);
        instance.GetComponent<UnstableChargeProjectileController>().SetVelocity(direction);
    }
}