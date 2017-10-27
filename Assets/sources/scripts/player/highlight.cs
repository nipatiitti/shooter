using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera/Add highlights")]
public class highlight : MonoBehaviour {

    public float distance;
    public Shader highLight;
    public Shader normal;

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        GameObject target = null;

        Debug.DrawRay(transform.position, transform.forward*distance, Color.red);

        if (Physics.Raycast(ray, out hit, distance))
        {
            Renderer rend;
            target = hit.transform.gameObject;

            if (target.tag == "item")
            {
                if (target.transform.childCount > 0)
                {
                    target = target.transform.GetChild(0).gameObject;
                }

                if (target.GetComponent<Renderer>())
                {
                     rend = target.GetComponent<Renderer>();
                     rend.material.shader = highLight;
                }
            }   
        }
        else
        {
            if (target)
            {
                Renderer rend;
                rend = target.GetComponent<Renderer>();
                rend.material.shader = normal;
            } else
            {
                target = null;
            }
        }
    }
}
