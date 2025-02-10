using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolCard : MonoBehaviour
{
    [SerializeField] private GameObject realTool; //도구 (실제)
    [SerializeField] private ToolSO toolType; //도구 종류
    private Image _myImage; //도구 이미지 (버튼)
    public static Action<ToolSO> toolBtnUse; //도구 사용 버튼
    public static Action toolBtnNotUse; //도구 사용 버튼

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

    public void NotUse()
    {
        _myImage.color = Color.white;

        toolBtnNotUse?.Invoke();

        realTool.SetActive(false);
        realTool.transform.SetParent(transform, false);
    }

    public void UseTool() //사용 버튼 활성화
    {
        _myImage.color = new Color(95 / 225f, 95 / 225f, 95 / 225f, 1);

        toolBtnUse?.Invoke(toolType);

        realTool.SetActive(true);
        realTool.transform.SetParent(player.transform, false);
    }
}
