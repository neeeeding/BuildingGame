using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BlueprintSO", menuName = "SO") ]

public class BlueprintSO : ScriptableObject
{
    [Header("Blueprint Setting")]

    [SerializeField] private int number;

    public bool liberation;

    public Image image;
}
