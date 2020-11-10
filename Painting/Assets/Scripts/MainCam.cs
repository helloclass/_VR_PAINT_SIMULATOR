using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    public float speed = 1.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        float ud = speed;
        if (Input.GetKey(KeyCode.Q))
            ud *= 1.0f;
        else if (Input.GetKey(KeyCode.E))
            ud *= -1.0f;
        else
            ud = 0.0f;

        transform.localPosition += new Vector3(0.0f, ud, 0.0f) * Time.fixedDeltaTime;
        transform.localPosition += speed * Input.GetAxisRaw("Vertical") * transform.forward * Time.fixedDeltaTime;
        transform.localPosition += speed * Input.GetAxisRaw("Horizontal") * transform.right * Time.fixedDeltaTime;

        if (Input.GetMouseButton(1))
        {
            // Quaternion q = Quaternion.Euler(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
            transform.Rotate(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }
    }
}
