using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintSelect : MonoBehaviour
{
    [SerializeField] private int allStage;
    [SerializeField] private int currentStage;


    public void NextBtn()
    {
        currentStage = allStage <= currentStage ? 0 : ++currentStage;
    }

    public void BeforeBtn()
    {
        currentStage = 0 >= currentStage ? allStage : --currentStage;
    }
}
