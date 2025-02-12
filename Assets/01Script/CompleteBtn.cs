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
    [SerializeField] private int CurrentNumber; //실질적인 현재 단계 (번호)
    private TextMeshProUGUI btnText;

    public static Action<StepType> CurrentStep;

    private void Start()
    {
        AddStep();
        CurrentNumber = 0;
        btnText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void ClcikComplete()
    {
        CurrentNumber += CurrentNumber < Steps.Count - 1? 1 : 0;

        if(Steps[CurrentNumber] == StepType.None)
        {
            //게임 끝
            print("END");
        }
        else
        {
            ScoreCount();
            btnText.text = StepName(Steps[CurrentNumber]);
        }
    }

    private void AddStep()
    {
        Steps = Enum.GetValues(typeof(StepType)).Cast<StepType>().ToList();
    }

    private string StepName(StepType step)
    {
        var field = step.GetType().GetField(step.ToString());
        var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attr?.Description ?? step.ToString();
    }

    private void ScoreCount()
    {
        //점수 계산
    }
}

public enum StepType
{
    [Description("철거 완료")] Demolition,
    [Description("터파기 완료")] Digging,
    [Description("바닥기초 완료")] FloorBasic,
    [Description("골조 공사 완료")] FrameWork,
    [Description("마감 공사 완료")] FinishingWork,
    [Description("완료")] None
}
