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

    private float startBarSize;
    private void Start()
    {
        if(isPlayer)
        {
            startBarSize = healthBar.sizeDelta.x;
            healthBar.sizeDelta = new Vector2(startBarSize * health / maxHealth, healthBar.sizeDelta.y);
        }
    }
    private void Update()
    {
        if(isPlayer)
        {
            if(health <= 0)
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(isPlayer)
            healthBar.sizeDelta = new Vector2(startBarSize * health / maxHealth, healthBar.sizeDelta.y);
    }
}
