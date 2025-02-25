using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private float moveSpeed = 8f;
    private float mouseX = 0f;
    private float mouseY = 0f;
    private Vector2 mousePoint;

    private bool canRatate; //true : 돌기 가능, false : 돌기 불가

    [SerializeField]private bool IsCamera;

    private void OnEnable()
    {
        PlayerInput.Instance.OnRotate += can => canRatate = can;
        canRatate = true;
    }


    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * moveSpeed;
        mouseY += Input.GetAxis("Mouse Y") * moveSpeed;

        //mouseX = Mathf.Clamp(mouseX, -170f, 170f);
        mouseY = Mathf.Clamp(mouseY, -50f, 30f);

        mousePoint = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, 1920), Mathf.Clamp(Input.mousePosition.y, 0, 1080));

        if (canRatate && mousePoint.y >= 250 /*&& mousePoint.y <= 1050*/)
        {
            transform.localEulerAngles = new Vector3(IsCamera ? -mouseY : 0, IsCamera ? 0 : mouseX, 0);
        }
    }
}
