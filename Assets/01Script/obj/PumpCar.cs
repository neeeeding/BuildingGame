using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpCar : MonoBehaviour
{
    [SerializeField] private GameObject toolKeep;
    [Space(10f)]
    [SerializeField] private GameObject cement; //Ω√∏‡∆Æ
    [SerializeField] private Transform position;

    private void OnEnable()
    {
        ToolUseBtn.OnUseTool += Cement;
        cement.SetActive(false);
    }

    private void Cement(ToolSO obj)
    {
        GameObject cementDrop = Instantiate(cement);
        cementDrop.transform.position = position.position;
        cementDrop.GetComponent<Cement>().ToolKeep(toolKeep);
        cementDrop.SetActive(true);
    }

    private void OnDisable()
    {
        ToolUseBtn.OnUseTool -= Cement;
    }
}
