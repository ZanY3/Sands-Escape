using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject[] walls;
    public GameObject enemies;
    public int enemiesLeft;
    public bool isOpened = true;
    public bool closeRoom = true;

    private void Start()
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(!isOpened);
        }
    }
    private void Update()
    {
        if(closeRoom)
        {
            foreach (GameObject wall in walls)
            {
                wall.SetActive(!isOpened);
            }
        }
        if(enemiesLeft <= 0)
        {
            isOpened = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("level started");
            if(closeRoom)
                enemies.SetActive(true);
            isOpened = false;

        }
    }
}
