using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintSelect : MonoBehaviour
{
    [SerializeField] private int allStage;
    [SerializeField] private int currentStage; //1 ~ allStage

    public Action<int> ChangePage;


    public void NextBtn()
    {
        currentStage = allStage <= currentStage ? 1 : ++currentStage;
        ChangePage?.Invoke(currentStage - 1);
    }

    public void BeforeBtn()
    {
        currentStage = 1 >= currentStage ? allStage : --currentStage;
        ChangePage?.Invoke(currentStage - 1);
    }
}
