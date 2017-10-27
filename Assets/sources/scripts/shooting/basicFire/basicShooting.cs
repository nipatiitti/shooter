﻿using System.Collections;
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

    public int bullets;
    public int perClip;
    public int bulletsOnGun;

    public float reloadTime;

    public float timeBetweenShots;
    private float readyToShoot;

    void Start()
    {
        if (bullets-perClip >= 0)
        {
            bulletsOnGun = perClip;
            bullets = bullets - perClip + bulletsOnGun;
        } else
        {
            bulletsOnGun = bullets;
        }
        
        
    }

    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            if(bulletsOnGun > 0 && Time.time >= readyToShoot)
            {
                Fire();
                bulletsOnGun--;
                readyToShoot = Time.time + timeBetweenShots;
            }
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (bullets - perClip >= 0)
            {
                bullets = bullets - perClip + bulletsOnGun;
                bulletsOnGun = perClip;
            }
            else
            {
                bulletsOnGun = bullets;
                bullets = 0;
            }
            readyToShoot = Time.time + reloadTime;
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
