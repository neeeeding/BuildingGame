using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolCard : MonoBehaviour
{
    [SerializeField] private GameObject realTool; //���� (����)
    [SerializeField] private ToolSO toolType; //���� ����
    private Image _myImage; //���� �̹��� (��ư)
    public static Action<ToolSO> toolBtnUse; //���� ��� ��ư
    public static Action toolBtnNotUse; //���� ��� ��ư

    private static bool isUseTool; //true : ������� ������ ����, false : ��� ������

    private GameObject player;

    private void Awake()
    {
        _myImage = GetComponent<Image>();
        toolType.isUse = false;
        realTool.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        // myImage = toolType.toolImage;
        NotUse();
        isUseTool = false;
    }

    public void ClickTool()
    {
        if (!toolType.isUse && !isUseTool) UseTool();
        else if (toolType.isUse && isUseTool) NotUse();
        else return;

        toolType.isUse = !toolType.isUse;
        isUseTool = !isUseTool;
    }

    public void NotUse()
    {
        _myImage.color = Color.white;

        toolBtnNotUse?.Invoke();

        realTool.SetActive(false);
        realTool.transform.SetParent(transform, false);
    }

    public void UseTool() //��� ��ư Ȱ��ȭ
    {
        _myImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);

        toolBtnUse?.Invoke(toolType);

        realTool.SetActive(true);
        realTool.transform.SetParent(player.transform, false);
    }
}
