using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolCard : MonoBehaviour
{
    [SerializeField] private ToolSO toolType;
    private Image myImage;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    private void Start()
    {
       // myImage = toolType.toolImage;
    }
}
