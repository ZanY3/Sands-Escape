using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public RectTransform healthBar;
    public bool isPlayer = false;
    public AudioClip playerDamageSound;
    [Space]
    public int enemyDieCoinsDrop = 1;

    private CameraShake cameraShake;
    private CoinsController coinController;
    private AudioSource source;

    private float startBarSize;

    private void Start()
    {
        coinController = FindAnyObjectByType<CoinsController>();
        if (isPlayer)
        {
            cameraShake = FindAnyObjectByType<CameraShake>();
            source = GetComponent<AudioSource>();
            startBarSize = healthBar.sizeDelta.y;
            healthBar.sizeDelta = new Vector2 (healthBar.sizeDelta.x, startBarSize * health / maxHealth);
        }
    }
    private void Update()
    {
        if (isPlayer)
        {
            if (health <= 0)
            {
                SceneManager.LoadScene("Game");
            }
            if(health >= maxHealth)
            {
                health = maxHealth;
            }
        }
        else
        {
            if(health <= 0)
            {
                coinController.TakeCoins(enemyDieCoinsDrop);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (isPlayer)
        {
            cameraShake.ShakeCamera();
            healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x, startBarSize * health / maxHealth);
            source.PlayOneShot(playerDamageSound);
        }
    }
    public void TakeBonus(int count)
    {
        health += count;
        healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x, startBarSize * health / maxHealth);
    }
}