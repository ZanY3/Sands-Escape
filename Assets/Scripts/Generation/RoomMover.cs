using UnityEngine;

public class RoomMover : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;
    private void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("ss");
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position += playerChangePos;
            cam.transform.position = cameraChangePos;
        }
    }
}
