using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ToolSO", menuName = "SO/ToolSO")]
public class ToolSO : ScriptableObject
{
    [SerializeField] private string toolName;

    public ToolType type;

    public bool isUse;

    public Image toolImage;
}

public enum ToolType
{
    car, rotateTool, soil, clcikTool
}
