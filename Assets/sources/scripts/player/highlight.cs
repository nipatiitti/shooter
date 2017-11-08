using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Pick ups/Player")]
public class highlight : MonoBehaviour {

    public float distance;
    public float throwSpeed;
    
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        GameObject target = null;

        Debug.DrawRay(transform.position, transform.forward*distance, Color.red);

        if (Physics.Raycast(ray, out hit, distance))
        {
            target = hit.transform.gameObject;

            if (target.tag == "item" && target.GetComponent<enablePicking>())
            {
                Debug.Log("Pick up this item with e");
            }   
        }

        if(Input.GetButtonDown("Fire2"))
        {
            GameObject gunController = GameObject.Find("gunController");

            if (gunController.transform.childCount > 0)
            {
                GameObject child = gunController.transform.GetChild(0).gameObject;

                if (!child.GetComponent<Rigidbody>())
                    child.AddComponent<Rigidbody>();

                child.GetComponent<Rigidbody>().AddForce(child.transform.forward * throwSpeed);

                child.transform.parent = null;
            }
        }

        if(Input.GetButtonDown("Pick"))
        {
            if (Physics.Raycast(ray, out hit, distance))
            {
                target = hit.transform.gameObject;

                if (target.tag == "item" && target.GetComponent<enablePicking>())
                {
                    GameObject gunController = GameObject.Find("gunController");

                    if(gunController.transform.childCount > 0)
                    {
                        GameObject child = gunController.transform.GetChild(0).gameObject;
                        GameObject newTarget = Instantiate(child, target.transform.position, Quaternion.identity);
                        if (!newTarget.GetComponent<Rigidbody>())
                            newTarget.AddComponent<Rigidbody>();
                        GameObject newChild = Instantiate(target, gunController.transform.position, gunController.transform.rotation);
                        if (newChild.GetComponent<Rigidbody>())
                            Destroy(newChild.GetComponent<Rigidbody>());
                        newChild.transform.parent = gunController.transform;

                        if (child.GetComponent<enablePicking>().canShoot)
                        {
                            int[] bullets = child.GetComponent<basicShooting>().getBullets();
                            StartCoroutine(setBullets(newTarget, bullets[0], bullets[1]));
                        }

                        if (target.GetComponent<enablePicking>().canShoot)
                        {
                            int[] bullets = target.GetComponent<basicShooting>().getBullets();
                            StartCoroutine(setBullets(newChild, bullets[0], bullets[1]));
                        }

                        Destroy(child);
                        Destroy(target);
                    } else
                    {
                        GameObject newChild = Instantiate(target, gunController.transform.position, gunController.transform.rotation);
                        if (newChild.GetComponent<Rigidbody>())
                            Destroy(newChild.GetComponent<Rigidbody>());
                        newChild.transform.parent = gunController.transform;

                        if (target.GetComponent<enablePicking>().canShoot)
                        {
                            int[] bullets = target.GetComponent<basicShooting>().getBullets();
                            StartCoroutine(setBullets(newChild, bullets[0], bullets[1]));
                        }

                        Destroy(target);
                    }
                }
            }
        }
    }

    IEnumerator setBullets(GameObject gun, int bullets, int bulletsOnGun)
    {
        yield return 0;
        gun.GetComponent<basicShooting>().setBullets(bullets, bulletsOnGun);
    }

}

