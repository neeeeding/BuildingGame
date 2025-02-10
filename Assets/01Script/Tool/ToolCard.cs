using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolCard : MonoBehaviour
{
    [SerializeField] private GameObject myTool; //도구 (실제)
    [SerializeField] private ToolSO toolType; //도구 종류
    private Image myImage; //도구 이미지 (버튼)
    public Action<ToolSO> toolUseBtnShow; //도구 사용 버튼

    private void Awake()
    {
        myImage = GetComponent<Image>();
        toolType.isUse = false;
        myTool.SetActive(false);
    }

    private void Start()
    {
        // myImage = toolType.toolImage;
        NotUse();
    }

    public void ClickTool()
    {
        toolType.isUse = !toolType.isUse;
        if (toolType.isUse)
        {
            UseTool();
        }
        else
        {
            NotUse();
        }
    }

    public virtual void NotUse()
    {
        myImage.color = Color.white;
    }

    public virtual void UseTool() //사용 버튼 활성화
    {
        myImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);
        toolUseBtnShow?.Invoke(toolType);
    }
}
