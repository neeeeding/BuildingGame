using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public GameObject Player;

    private void Awake()
    {
        Player = gameObject;
    }
}
