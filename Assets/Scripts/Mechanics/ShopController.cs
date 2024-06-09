using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class ShopController : MonoBehaviour
{
    public GameObject shopScreen;
    public int maxHealthUpPrice;
    public int healthUpPrice;
    public int maxManaUpPrice;

    private bool usable = false;
    private bool inShop = false;
    private CoinsController coinController;
    private Health health;
    private Mana mana;

    private void Start()
    {
        coinController = FindAnyObjectByType<CoinsController>();
        mana = FindAnyObjectByType<Mana>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && usable)
        {
            inShop = true;
            shopScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && inShop)
        {
            inShop = false;
            shopScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void MaxHealthLevelUp()
    {
        if(coinController.coins >= maxHealthUpPrice)
        {
            coinController.coins -= maxHealthUpPrice;
            coinController.coinsText.text = coinController.coins.ToString();
            health.UpMaxHealthLevel();
            Debug.Log("Buyed max health");
        }
    }
    public void HealthLevelUp()
    {
        if (coinController.coins >= healthUpPrice)
        {
            coinController.coins -= healthUpPrice;
            coinController.coinsText.text = coinController.coins.ToString();
            health.TakeBonus(health.maxHealth - health.health);
            Debug.Log("Buyed health");
        }
    }
    public void MaxManaLevelUp()
    {
        if (coinController.coins >= maxManaUpPrice)
        {
            coinController.coins -= maxManaUpPrice;
            coinController.coinsText.text = coinController.coins.ToString();
            mana.UpMaxManaLevel();
            Debug.Log("Buyed max mana");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = true;
            health = collision.GetComponent<Health>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = false;
        }
    }
}
