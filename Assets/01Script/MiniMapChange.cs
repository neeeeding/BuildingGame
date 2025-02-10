using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapChange : MonoBehaviour
{
    [SerializeField] private BlueprintSO blueprintImage;

    [SerializeField] private Image blueprint;
    [SerializeField] private GameObject miniMap;

    private bool isMap; //true = map, false = blueprint

    private void Start()
    {
        isMap = false;

        //blueprint = blueprintImage.image;

        miniMap.SetActive(isMap);
        blueprint.gameObject.SetActive(!isMap);
    }

    public void ClcikBtn()
    {
        isMap = !isMap;

        miniMap.SetActive(isMap);
        blueprint.gameObject.SetActive(!isMap);
    }
}
