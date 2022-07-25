using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPotion : MonoBehaviour
{
    [SerializeField] private PlayerState playerStats;
    [SerializeField] private float energyRestore = 10;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F) && playerStats.GetCurrentEnergy() < playerStats.GetMaxEnergy())
        {
            playerStats.IncreaseEnergy(energyRestore);
            Destroy(this.gameObject);
        }
    }
}
