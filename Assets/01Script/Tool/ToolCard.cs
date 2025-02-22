using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolCard : MonoBehaviour
{
    [SerializeField] private GameObject realTool; //도구 (실제)
    [SerializeField] private ToolSO toolType; //도구 종류
    private Image _myImage; //도구 이미지 (버튼)
    public static Action<ToolSO, GameObject> toolBtnUse; //도구 사용 버튼
    public static Action toolBtnNotUse; //도구 사용 버튼

    private static bool isUseTool; //true : 사용중인 도구가 존재, false : 사용 가능함

    private GameObject player;

    private void Awake()
    {
        _myImage = GetComponent<Image>();
        toolType.isUse = false;

        if(toolType.type != ToolType.delete) //지우기만 아니면
        {
            realTool.SetActive(true);
            player = GameObject.FindGameObjectWithTag("Player");
        }

        CompleteBtn.CurrentStep += ShowToolBtn;
    }

    private void ShowToolBtn(StepType step)
    {
        gameObject.SetActive(Array.Exists(toolType.useStep, i => i.Equals( step)));
        print($"{gameObject.name} : {step} , {Array.Exists(toolType.useStep, i => i.Equals(step))}");
    }

    private void Start()
    {
        if(toolType.type != ToolType.delete)
         _myImage.sprite = toolType.toolImage;
        NotUse();
        isUseTool = false;
    }

    public void ClickTool() //도구(버튼) 누를 때
    {
        if (!toolType.isUse && !isUseTool) UseTool(); //사용
        else if (toolType.isUse && isUseTool) NotUse(); //비활성
        else return; //사용 중인데 다른 도구를 누름

        toolType.isUse = !toolType.isUse;
        isUseTool = !isUseTool;
    }

    public void NotUse() //도구 비활성
    {
        _myImage.color = Color.white;

        toolBtnNotUse?.Invoke();
        
        if(toolType.type == ToolType.car)
        {
            realTool.SetActive(false);
            realTool.transform.SetParent(transform, false);
        }
    }

    public void UseTool() //사용 버튼을 활성화
    {
        _myImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);

        toolBtnUse?.Invoke(toolType, realTool);

        if (toolType.type == ToolType.car) //차 타입이라면
        {
            realTool.SetActive(true);
            realTool.transform.SetParent(player.transform, false);
        }

        CompleteBtn.CurrentStep -= ShowToolBtn;
    }
}
