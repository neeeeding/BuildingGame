using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BluePrint : MonoBehaviour
{
    [SerializeField] private BlueprintSO[] BlueprintSOs;
    [SerializeField] private BlueprintSO currentBlueprint; //���� ������

    [SerializeField] private Image _myImage;
    [SerializeField] private GameObject _lockImage;
    private TextMeshProUGUI _warning;

    private BlueprintSelect _selectScript;

    private void Awake()
    {
        _warning = GetComponentInChildren<TextMeshProUGUI>();
        _selectScript = GetComponentInParent<BlueprintSelect>();
        _selectScript.ChangePage += ChangeBlueprint;
    }

    private void Start()
    {
        ChangeBlueprint(0);
    }

    public void ClickBtn()
    {
        if (currentBlueprint.isLock)
        {
            StartCoroutine(Warning());
        }
        else
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            SceneManager.LoadScene(currentBlueprint.number + 1);
        }
    }

    private void ChangeBlueprint(int currentPage) //������ �ٲܶ� ����
    {
        currentBlueprint = BlueprintSOs[currentPage];
        _myImage = currentBlueprint.image;
        _warning.gameObject.SetActive(false);
        _lockImage.SetActive(currentBlueprint.isLock);
    }

    private IEnumerator Warning() //���� �߰��ϱ�
    {
        _warning.gameObject.SetActive(true);
        _warning.text = "����ִ� ���������Դϴ�.\n���� ���������� Ŭ���� ���ּ���.";
        yield return new WaitForSeconds(1.2f);
        _warning.gameObject.SetActive(false);
    }
}
