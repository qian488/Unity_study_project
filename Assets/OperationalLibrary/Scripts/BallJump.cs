using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJump : MonoBehaviour
{
    public float jumpForce;

    private void Update()
    {
        // 0 ×ó¼ü 1 ÓÒ¼ü 2 ÖÐ¼ü
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
        }
    }
}
