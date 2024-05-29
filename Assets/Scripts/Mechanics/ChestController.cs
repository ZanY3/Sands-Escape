using UnityEngine;

public class ChestController : MonoBehaviour
{
    public GameObject openParticles;
    public AudioClip openSound;

    private ChestLoot loot;
    private bool usable = false;
    private GameObject chestWearpon;

    private AudioSource source;
    private Collider2D colider;
    private Animator animator;

    private bool canDrop = true;

    private void Start()
    {
        loot = FindAnyObjectByType<ChestLoot>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        colider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && usable && canDrop)
        {
            source.PlayOneShot(openSound);
            Instantiate(openParticles, transform.position, Quaternion.identity);
            chestWearpon = loot.GetRandomWearpon();
            Instantiate(chestWearpon, transform.position, Quaternion.identity);
            canDrop = false;
            colider.isTrigger = true;
            animator.SetTrigger("Destroy");
            Destroy(gameObject, 1f);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            usable = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            usable = false;
        }
    }
}
