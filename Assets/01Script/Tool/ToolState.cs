using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolState : MonoBehaviour
{
    public ToolCard myRoot;

    public static bool canDelete; //ture : 지워짐, false : 못 지움
    public static bool canBind; //ture : 묶임, false : 못 묶음
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
        if(canBind && Input.GetMouseButtonDown(0))
        {
            BindMe();
        }
    }

    private void BindMe()
    {
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    private void DeleteMe()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //레이로 마우스 위치 확인
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            myRoot.toolList.Add(gameObject);
            gameObject.SetActive(false);
            gameObject.transform.SetParent(myRoot.transform, false);
        }
    }
}
