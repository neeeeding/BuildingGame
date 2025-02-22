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
    public static Action<ToolSO, GameObject> toolBtnUse; //���� ��� ��ư
    public static Action toolBtnNotUse; //���� ��� ��ư

    private static bool isUseTool; //true : ������� ������ ����, false : ��� ������

    private GameObject player;

    private void Awake()
    {
        _myImage = GetComponent<Image>();
        toolType.isUse = false;

        if(toolType.type != ToolType.delete) //����⸸ �ƴϸ�
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

    public void ClickTool() //����(��ư) ���� ��
    {
        if (!toolType.isUse && !isUseTool) UseTool(); //���
        else if (toolType.isUse && isUseTool) NotUse(); //��Ȱ��
        else return; //��� ���ε� �ٸ� ������ ����

        toolType.isUse = !toolType.isUse;
        isUseTool = !isUseTool;
    }

    public void NotUse() //���� ��Ȱ��
    {
        _myImage.color = Color.white;

        toolBtnNotUse?.Invoke();
        
        if(toolType.type == ToolType.car)
        {
            realTool.SetActive(false);
            realTool.transform.SetParent(transform, false);
        }
    }

    public void UseTool() //��� ��ư�� Ȱ��ȭ
    {
        _myImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);

        toolBtnUse?.Invoke(toolType, realTool);

        if (toolType.type == ToolType.car) //�� Ÿ���̶��
        {
            realTool.SetActive(true);
            realTool.transform.SetParent(player.transform, false);
        }

        CompleteBtn.CurrentStep -= ShowToolBtn;
    }
}
