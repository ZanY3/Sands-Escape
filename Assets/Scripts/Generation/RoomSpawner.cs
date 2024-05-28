using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction; // направление данной комнаты
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    } //вариации нарпавлений комнат
    public bool spawned = false;
    public bool canBeDestroyed = false;

    private RoomVariants variants;
    private int rand;
    private float waitTime = 3f;
    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }
    void Spawn()
    {
        if (!spawned)
        {
            switch (direction)
            {
                case Direction.Top:
                    rand = Random.Range(0, variants.topRooms.Length);
                    Instantiate(variants.topRooms[rand], transform.position, Quaternion.identity);
                    break;
                case Direction.Bottom:
                    rand = Random.Range(0, variants.bottomRooms.Length);
                    Instantiate(variants.bottomRooms[rand], transform.position, Quaternion.identity);
                    break;
                case Direction.Left:
                    rand = Random.Range(0, variants.leftRooms.Length);
                    Instantiate(variants.leftRooms[rand], transform.position, Quaternion.identity);
                    break;
                case Direction.Right:
                    rand = Random.Range(0, variants.rightRooms.Length);
                    Instantiate(variants.rightRooms[rand], transform.position, Quaternion.identity);
                    break;
            }
            spawned = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().spawned) //что бы предатвратить спавн комнаты на комнате
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
        {
            if (canBeDestroyed)
            {
                Debug.Log("RoomPoint collision detected and conditions met. Destroying the object.");
                Destroy(collision.gameObject);
            }
            else
            {
                var roomSpawner = collision.gameObject.GetComponent<RoomSpawner>();
                roomSpawner.canBeDestroyed = true;
            }
        }
    }
}
