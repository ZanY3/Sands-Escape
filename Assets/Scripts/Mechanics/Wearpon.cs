using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wearpon : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    public float shootCD;
    public float offset;
    public bool usable = true;
    [Space]
    public bool isUseMana = false;
    public float manaForOneShot;
    [Space]
    public GameObject shootParticles;
    public Color shootParticlesColor;
    public AudioClip shootSound;

    private Mana mana;
    private float startShootCD;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        mana = GameObject.FindGameObjectWithTag("Player").GetComponent<Mana>();
        startShootCD = shootCD;
    }
    private void Update()
    {
        shootCD -= Time.deltaTime;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (Input.GetMouseButton(0))
        {
            if(usable == true && shootCD <= 0 && mana.mana >= manaForOneShot)
            {
                if(isUseMana)
                {
                    source.PlayOneShot(shootSound);
                    mana.mana -= manaForOneShot;
                }
                else
                {
                    source.PlayOneShot(shootSound);
                }
                Instantiate(bullet, shootPoint.position, Quaternion.identity);

                GameObject particles = Instantiate(shootParticles, transform.position, Quaternion.identity);
                ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    var mainModule = particleSystem.main;
                    mainModule.startColor = shootParticlesColor;
                    Color colorWithMaxAlpha = new Color(shootParticlesColor.r, shootParticlesColor.g, shootParticlesColor.b, 1f);
                    mainModule.startColor = new ParticleSystem.MinMaxGradient(colorWithMaxAlpha);
                    shootCD = startShootCD;
                }
            }
        }
    }
}
