using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BluePrint : MonoBehaviour
{
    [SerializeField] private BlueprintSO SO;

    private Image myImage;
    private TextMeshProUGUI warning;

    private void Awake()
    {
        myImage = GetComponentInChildren<Image>();
        warning = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        myImage = SO.image;
        warning.gameObject.SetActive(false);
    }

    public void ClickBtn()
    {
        if (SO.liberation)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Warning();
        }
    }

    private IEnumerator Warning()
    {
        warning.gameObject.SetActive(true);
        warning.text = "����ִ� ���������Դϴ�.\n���� ���������� Ŭ���� ���ּ���.";
        yield return new WaitForSeconds(2f);
        warning.gameObject.SetActive(false);
    }
}
