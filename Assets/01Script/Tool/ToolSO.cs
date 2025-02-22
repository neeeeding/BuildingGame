using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolSO", menuName = "SO/ToolSO")]
public class ToolSO : ScriptableObject
{
    [SerializeField] private string toolName; //도구 이름(구분 용)

    public StepType[] useStep; //도구 사용 가능한 단계들

    public ToolType type; //도구 종류

    public bool isUse; //true : 사용중임, false : 사용중이 아님

    public Sprite toolImage; //도구 사진
}

public enum ToolType
{
    car, rotateTool, soil, clcikTool, delete
}
