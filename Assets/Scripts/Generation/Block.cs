using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject wallPrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            print("destroyed block");
            Instantiate(wallPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
