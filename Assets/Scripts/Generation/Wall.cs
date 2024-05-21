using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject block;
    public bool isSpawned = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block") && !isSpawned)
        {
            Instantiate(block, transform.GetChild(0).position, Quaternion.identity);
            Instantiate(block, transform.GetChild(1).position, Quaternion.identity);
            Destroy(gameObject);
            isSpawned = true;
        }
    }
}
