using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public GameObject Player;

    public ToolSO[] allTool;

    private void Awake()
    {
        Player = gameObject;

        ResetSO();
    }

    private void ResetSO()
    {
        foreach(ToolSO so in allTool)
        {
            so.ResetSO();
        }
    }
}
