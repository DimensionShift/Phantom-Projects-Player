using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private PlayerState playerStats;
    [SerializeField] private float healthRestore = 10;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F) && playerStats.GetCurrentHealth() < playerStats.GetMaxHealth())
        {
            playerStats.IncreaseHealth(healthRestore);
            Destroy(this.gameObject);
        }
    }
}
