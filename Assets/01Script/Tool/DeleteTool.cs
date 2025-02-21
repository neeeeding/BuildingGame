using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTool : MonoBehaviour
{
    public static bool canDelete; //ture : 지워짐, false : 못 지움
    private void Awake()
    {
        canDelete = false;
    }

    private void Update()
    {
        if (canDelete && Input.GetMouseButtonDown(0))
        {
            DeleteMe();
        }
    }

    public void DeleteMe()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //레이로 마우스 위치 확인
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            Destroy(gameObject);
        }
    }
}
