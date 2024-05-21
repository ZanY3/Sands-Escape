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
    private RoomVariants variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 3f;
    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }
    void Spawn()
    {
        if(!spawned)
        {
            switch(direction)
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
        }
        spawned = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().spawned) //что бы предатвратить спавн комнаты на комнате
        {
            Destroy(gameObject);
        }
    }
}
