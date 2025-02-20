using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolSO", menuName = "SO/ToolSO")]
public class ToolSO : ScriptableObject
{
    [SerializeField] private string toolName;

    public ToolType type;

    public bool isUse; //true : 사용중임, false : 사용중이 아님

    public Sprite toolImage;
}

public enum ToolType
{
    car, rotateTool, soil, clcikTool, delete
}
