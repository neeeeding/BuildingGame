using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotat : MonoBehaviour
{
    private float moveSpeed = 8f;
    private float mouseX = 0f;
    private float mouseY = 0f;
    private Vector2 mousePoint;

    [SerializeField]private bool IsCamera;

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * moveSpeed;
        mouseY += Input.GetAxis("Mouse Y") * moveSpeed;

        //mouseX = Mathf.Clamp(mouseX, -170f, 170f);
        mouseY = Mathf.Clamp(mouseY, -50f, 30f);

        mousePoint = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, 1920), Mathf.Clamp(Input.mousePosition.y, 0, 1080));

        if (mousePoint.y >= 250 /*&& mousePoint.y <= 1050*/)
        {
            transform.localEulerAngles = new Vector3(IsCamera ? -mouseY : 0, IsCamera ? 0 : mouseX, 0);
        }
    }
}
