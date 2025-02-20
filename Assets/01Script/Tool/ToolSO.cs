using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolSO", menuName = "SO/ToolSO")]
public class ToolSO : ScriptableObject
{
    [SerializeField] private string toolName;

    public ToolType type;

    public bool isUse; //true : �������, false : ������� �ƴ�

    public Sprite toolImage;
}

public enum ToolType
{
    car, rotateTool, soil, clcikTool, delete
}
