using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BlueprintSO", menuName = "SO") ]

public class BlueprintSO : ScriptableObject
{
    [Header("Blueprint Setting")]

    public int number;

    public bool isLock; //true = ����ִ�. false = �����ִ�.

    public Image image;
}
