using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : MonoBehaviour
{
    public int healthCount;
    public GameObject clueText;
    private bool usable;
    private Health playerHealth;
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && usable && playerHealth.health < playerHealth.maxHealth)
        {
            playerHealth.TakeBonus(healthCount);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = true;
            clueText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = false;
            clueText.SetActive(false);
        }
    }
}
