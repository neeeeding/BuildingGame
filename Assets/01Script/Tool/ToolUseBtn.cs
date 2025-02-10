using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolUseBtn : MonoBehaviour
{
    private Image btnImage;

    private void Awake()
    {
        btnImage = GetComponentInChildren<Image>();
        ToolCard.toolBtnUse += ActiveBtn;
        ToolCard.toolBtnNotUse += NotActiveBtn;
    }

    public void ClcikBtn()
    {

    }

    private void ActiveBtn(ToolSO so) //버튼 활성화 될 때
    {
        btnImage.gameObject.SetActive(true);
        //btnImage = so.toolImage;
    }

    private void NotActiveBtn()
    {
        btnImage.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ToolCard.toolBtnUse -= ActiveBtn;
        ToolCard.toolBtnNotUse -= NotActiveBtn;
    }
}
