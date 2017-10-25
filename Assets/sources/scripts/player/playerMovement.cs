using UnityEngine;

public class playerMovement : MonoBehaviour {
    public int vSpeed;
    public int hSpeed;

    void Update() {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * hSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * vSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}
