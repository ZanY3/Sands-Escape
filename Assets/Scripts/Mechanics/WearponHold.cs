using UnityEngine;
using UnityEngine.Rendering;

public class WearponHold : MonoBehaviour
{
    public Transform wearponHoldPoint;
    public GameObject wearpon;

    private Wearpon wearponScript;
    private bool isWithWearpon = true;
    private bool usable = false;
    private bool isCanTake = true;
    private void Update()
    {
        if(usable && Input.GetKeyDown(KeyCode.E) && isCanTake)
        {

            isWithWearpon = true;
            wearpon.transform.parent = transform;
            wearpon.transform.position = wearponHoldPoint.position;
            wearponScript.enabled = true;
            wearpon.GetComponent<Collider2D>().enabled = false;
            wearpon.GetComponent<Rigidbody2D>().isKinematic = true;
            wearpon.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            isCanTake = false;
        }
        if(isWithWearpon && Input.GetKeyDown(KeyCode.G) && wearpon != null && wearponScript != null)
        {
            wearpon.transform.SetParent(null);
            Vector2 newPosition = new Vector2(transform.position.x, transform.position.y);
            wearpon.transform.position = new Vector2(newPosition.x += 1, newPosition.y);
            wearponScript.enabled = false;
            wearpon.GetComponent<Collider2D>().enabled = true;
            wearpon.GetComponent<Rigidbody2D>().isKinematic = false;
            wearpon.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            wearpon = null;
            wearponScript = null;
            isWithWearpon = false;
            isCanTake = true;
            print("dropped");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("Gun"))
        {
            usable = true;
            if (isWithWearpon && wearpon == null && wearpon == null)
            {
                wearpon = collision.gameObject;
                wearponScript = collision.gameObject.GetComponent<Wearpon>();

            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Gun"))
        {
            usable = false;
        }
    }

}
