using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTool : MonoBehaviour
{
    public static bool canDelete; //ture : ������, false : �� ����
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���̷� ���콺 ��ġ Ȯ��
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            Destroy(gameObject);
        }
    }
}
