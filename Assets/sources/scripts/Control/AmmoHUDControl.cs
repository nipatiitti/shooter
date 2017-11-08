using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[AddComponentMenu("Control/Ammo")]
public class AmmoHUDControl : MonoBehaviour {

    public GameObject ammoText;
    Text Teksti;
    GameObject GunControl;

    void Awake()
    {
        GunControl = GameObject.Find("gunController");
        Teksti = ammoText.GetComponent<Text>();
    }

 

	void Update () {
        if (GunControl.transform.childCount > 0 && GunControl.transform.GetChild(0).GetComponent<enablePicking>().canShoot)
        {
            int[] bullets = GunControl.transform.GetChild(0).GetComponent<basicShooting>().getBullets();
            Teksti.text = bullets[1].ToString() + "/" + bullets[0].ToString();
        }
        else
        {
            Teksti.text = "";
        }
	}
}
