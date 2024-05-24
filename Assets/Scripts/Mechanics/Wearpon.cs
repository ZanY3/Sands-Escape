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

    private float startShootCD;
    private void Start()
    {
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
            if(usable == true && shootCD <= 0)
            {
                Instantiate(bullet, shootPoint.position, Quaternion.identity);
                shootCD = startShootCD;
            }
        }
    }
}
