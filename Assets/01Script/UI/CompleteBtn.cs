using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.ComponentModel;

public class CompleteBtn : MonoBehaviour
{
    private List<StepType> Steps;
    private int CurrentNumber; //�������� ���� �ܰ� (��ȣ)
    private TextMeshProUGUI btnText;

    public static Action<StepType> CurrentStep; //���� �ܰ踦 �˷���

    private void Start()
    {
        AddStep();
        CurrentNumber = 0;
        btnText = GetComponentInChildren<TextMeshProUGUI>();
        CurrentStep?.Invoke(Steps[CurrentNumber]);
    }
    public void ClcikComplete() //��ư Ŭ���� ��
    {
        CurrentNumber += CurrentNumber < Steps.Count - 1? 1 : 0;

        if(Steps[CurrentNumber] == StepType.None)
        {
            //���� ��
            print("END");
        }
        else
        {
            ScoreCount();
            btnText.text = StepName(Steps[CurrentNumber]);
            CurrentStep?.Invoke(Steps[CurrentNumber]);
        }
    }

    private void AddStep() //��� �� �ֱ�
    {
        Steps = Enum.GetValues(typeof(StepType)).Cast<StepType>().ToList();
    }

    private string StepName(StepType step) //�̸��� �˷���
    {
        var field = step.GetType().GetField(step.ToString());
        var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attr?.Description ?? step.ToString();
    }

    private void ScoreCount()
    {
        //���� ���
    }
}

public enum StepType
{
    [Description("ö�� �Ϸ�")] Demolition,
    [Description("���ı� �Ϸ�")] Digging,
    [Description("�ٴڱ��� �Ϸ�")] FloorBasic,
    [Description("���� ���� �Ϸ�")] FrameWork,
    [Description("���� ���� �Ϸ�")] FinishingWork,
    [Description("�Ϸ�")] None
}
