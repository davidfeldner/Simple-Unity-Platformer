using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float range = 100f;
    public float damage = 10f;

    public float fireRate = 15f;
    public float impactForce = 30f;
    public Camera playerCam;
    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;


    private float delayFire = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= delayFire) {
            delayFire = Time.time + 1f/fireRate;
            shoot();
        }
    }

    void shoot() {
        muzzleFlash.Play();
        RaycastHit hit;
        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range)) {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null) {
                target.takeDamage(damage); 
            } 

        }

        if(hit.rigidbody != null) {
            hit.rigidbody.AddForce(-hit.normal*impactForce);
        }

        GameObject impactObject = Instantiate(impactEffect, hit.point,Quaternion.LookRotation(hit.normal));
        Destroy(impactObject, 2f);
    }
}
