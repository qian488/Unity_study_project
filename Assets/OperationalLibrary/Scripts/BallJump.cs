using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJump : MonoBehaviour
{
    public float jumpForce;

    private void Update()
    {
        // 0 ��� 1 �Ҽ� 2 �м�
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpForce;
        }
    }
}
