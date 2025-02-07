using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BlueprintSO", menuName = "SO") ]

public class BlueprintSO : ScriptableObject
{
    [Header("Blueprint Setting")]

    public int number;

    public bool isLock; //true = 잠겨있다. false = 열려있다.

    public Image image;
}
