using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolUseBtn : MonoBehaviour
{
    private Image btnImage;

    [SerializeField] private GameObject justUseBtn;
    [SerializeField] private GameObject yBtn;
    [SerializeField] private GameObject xBtn;

    private void Awake()
    {
        btnImage = justUseBtn.GetComponent<Image>();
        ToolCard.toolBtnUse += ActiveBtn;
        ToolCard.toolBtnNotUse += NotActiveBtn;
    }

    public void JustBtnClick()
    {

    }

    public void XBtnClick()
    {

    }

    public void YBtnClcik()
    {

    }

    private void ActiveBtn(ToolSO so) //버튼 활성화 될 때
    {
        if(so.type == ToolType.car)
        {
            btnImage.gameObject.SetActive(true);
            btnImage.sprite = so.toolImage;
        }
        else if(so.type == ToolType.rotateTool)
        {
            yBtn.SetActive(true);
            xBtn.SetActive(true);
        }
    }

    private void NotActiveBtn()
    {
        justUseBtn.SetActive(false);
        yBtn.SetActive(false);
        xBtn.SetActive(false);
    }

    private void OnDisable()
    {
        ToolCard.toolBtnUse -= ActiveBtn;
        ToolCard.toolBtnNotUse -= NotActiveBtn;
    }
}
