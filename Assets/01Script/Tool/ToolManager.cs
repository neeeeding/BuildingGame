using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    private StepType currentStep;
    private void OnEnable()
    {
        CompleteBtn.CurrentStep += StepHide;
        ToolUseBtn.OnMark += MarkCard;
    }

    private void MarkCard(bool AllMark)
    {
        ChildCardShow(AllMark);
    }

    private void StepHide(StepType step)
    {
        currentStep = step;
        ChildCardShow(false);
    }

    private void ChildCardShow(bool mark)
    {
        foreach (Transform toolCard in gameObject.transform)
        {
            ToolCard toolCardSc = toolCard.GetComponent<ToolCard>();

            toolCardSc.NotUse();
            toolCard.gameObject.SetActive((toolCardSc.ChildToolType.useStep & currentStep) != 0 &&( toolCardSc.ChildToolType.isMark || mark == toolCardSc.ChildToolType.isMark));
            //현재 단계가 맞거나 mark와 so의 isMark가 같거나 그냥 isMark가 true일 때
        }
    }

    private void OnDisable()
    {
        CompleteBtn.CurrentStep -= StepHide;
    }
}
