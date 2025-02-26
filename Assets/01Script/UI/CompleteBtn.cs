using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.ComponentModel;

public class CompleteBtn : MonoBehaviour
{
    private StepType Steps;
    private int CurrentNumber; //실질적인 현재 단계 (번호)
    private TextMeshProUGUI btnText;

    public static Action<StepType> CurrentStep; //현재 단계를 알려줌

    private void Start()
    {
        Steps = StepType.all;
        AddStep();
        CurrentNumber = 0;
        btnText = GetComponentInChildren<TextMeshProUGUI>();
        CurrentStep?.Invoke((StepType)(1 << CurrentNumber));
    }
    public void ClcikComplete() //버튼 클릭할 때
    {
        if((StepType)(1 << CurrentNumber) == StepType.all)
        {
            //게임 끝
            print("END");
        }
        else
        {
            CurrentNumber++;
            ScoreCount();
            btnText.text = StepName((StepType)(1<<CurrentNumber));
            CurrentStep?.Invoke((StepType)(1 << CurrentNumber));
        }
    }

    private void AddStep() //모든 거 넣기
    {
        //Steps = Enum.GetValues(typeof(StepType)).Cast<StepType>().ToList();
    }

    private string StepName(StepType step) //이름을 알려줌
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

[Flags]
public enum StepType
{
    [Description("철거 완료")] Demolition = 1<<0,
    [Description("터파기 완료")] Digging = 1<<1,
    [Description("바닥기초 완료")] FloorBasic = 1<<2,
    [Description("골조 공사 완료")] FrameWork = 1<<3,
    [Description("마감 공사 완료")] FinishingWork = 1<<4,
    [Description("완료")] None = 0,
    [Description("완료")] all = ~0
}
