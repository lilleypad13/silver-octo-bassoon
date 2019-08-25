using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region Variables
    private PlayerStats playerStatsReference;
    private WaveSpawner waveSpawnerReference;

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int worth = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    #endregion

    #region Unity Methods

    private void Start()
    {
        playerStatsReference = PlayerStats.GetInstance();
        waveSpawnerReference = WaveSpawner.GetInstance();

        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    private void Die()
    {
        isDead = true;

        playerStatsReference.AddMoney(worth);

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        // Lets the WaveSpawner know that there is one less enemy in the wave
        waveSpawnerReference.enemiesAlive--;

        Destroy(gameObject);
    }

    #endregion
}
