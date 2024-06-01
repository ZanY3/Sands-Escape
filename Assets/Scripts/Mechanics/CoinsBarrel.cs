using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsBarrel : MonoBehaviour
{
    public int minCoinsToDrop = 2;
    public int maxCoinsToDrop = 5;
    public float spawnRadius = 1;
    public GameObject coinPrefab;
    public GameObject dropParticles;

    private int coinsToDrop;

    private void Start()
    {
        coinsToDrop = Random.Range(minCoinsToDrop, maxCoinsToDrop);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            DropCoins();
            Destroy(gameObject);
        }
    }
    private void DropCoins()
    {

        Instantiate(dropParticles, transform.position, Quaternion.identity);
        for(int i = 0; i < coinsToDrop; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.z = 0;
            Instantiate(coinPrefab, transform.position + randomOffset, Quaternion.identity);
        }
    }

}
