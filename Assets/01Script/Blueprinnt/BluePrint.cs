using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePrint : MonoBehaviour
{
    [SerializeField] private BlueprintSO SO;

    private Image myImage;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    private void Start()
    {
        myImage = SO.image;
    }

    public void ClickBtn()
    {
        if (SO.liberation)
        {

        }
    }
}
