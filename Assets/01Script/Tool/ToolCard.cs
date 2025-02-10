using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolCard : MonoBehaviour
{
    [SerializeField] private GameObject myTool; //���� (����)
    [SerializeField] private ToolSO toolType; //���� ����
    private Image myImage; //���� �̹��� (��ư)
    public Action<ToolSO> toolUseBtnShow; //���� ��� ��ư

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

    public virtual void UseTool() //��� ��ư Ȱ��ȭ
    {
        myImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);
        toolUseBtnShow?.Invoke(toolType);
    }
}
