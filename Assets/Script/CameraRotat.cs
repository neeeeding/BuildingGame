using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotat : MonoBehaviour
{
    private float moveSpeed = 8f;
    private float mouseX = 0f;
    private float mouseY = 0f;

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * moveSpeed;
        mouseY += Input.GetAxis("Mouse Y") * moveSpeed;

        mouseX = Mathf.Clamp(mouseX, -150f, 150f);
        mouseY = Mathf.Clamp(mouseY, -50f, 30f);

        transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);
    }
}
