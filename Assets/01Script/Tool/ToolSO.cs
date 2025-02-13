using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ToolSO", menuName = "SO/ToolSO")]
public class ToolSO : ScriptableObject
{
    [SerializeField] private string toolName;

    public ToolType type;

    public bool isUse; //true : 사용중임, false : 사용중이 아님

    public Image toolImage;
}

public enum ToolType
{
    car, rotateTool, soil, clcikTool
}
