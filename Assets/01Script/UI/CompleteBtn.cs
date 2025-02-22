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
    private int CurrentNumber; //실질적인 현재 단계 (번호)
    private TextMeshProUGUI btnText;

    public static Action<StepType> CurrentStep; //현재 단계를 알려줌

    private void Start()
    {
        AddStep();
        CurrentNumber = 0;
        btnText = GetComponentInChildren<TextMeshProUGUI>();
        CurrentStep?.Invoke(Steps[CurrentNumber]);
    }
    public void ClcikComplete() //버튼 클릭할 때
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
            CurrentStep?.Invoke(Steps[CurrentNumber]);
        }
    }

    private void AddStep() //모든 거 넣기
    {
        Steps = Enum.GetValues(typeof(StepType)).Cast<StepType>().ToList();
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

public enum StepType
{
    [Description("철거 완료")] Demolition,
    [Description("터파기 완료")] Digging,
    [Description("바닥기초 완료")] FloorBasic,
    [Description("골조 공사 완료")] FrameWork,
    [Description("마감 공사 완료")] FinishingWork,
    [Description("완료")] None
}
