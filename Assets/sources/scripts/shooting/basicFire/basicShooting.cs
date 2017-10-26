using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Shooting/Basic shooting")]
public class basicShooting : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public GameObject shellPrefab;
    public Transform shellSpawn;

    public float bulletVelocity;
    public float shellVelocity;

    public float TTLBullet;
    public float TTLShell;

    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Create the shell from the Bullet Prefab
        var shell = (GameObject)Instantiate(
            shellPrefab,
            shellSpawn.position,
            shellSpawn.rotation);

        // Add forces to the bullet and shell
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletVelocity);
        shell.GetComponent<Rigidbody>().AddForce(shellSpawn.transform.right * shellVelocity);
        shell.GetComponent<Rigidbody>().AddForce(shellSpawn.transform.up * shellVelocity);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, TTLBullet);
        Destroy(shell, TTLShell);
    }
}
