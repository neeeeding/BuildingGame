using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BluePrint : MonoBehaviour
{
    [SerializeField] private BlueprintSO[] BlueprintSOs;
    [SerializeField] private BlueprintSO currentBlueprint; //현재 페이지

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

    private void ChangeBlueprint(int currentPage) //페이지 바꿀때 마다
    {
        currentBlueprint = BlueprintSOs[currentPage];
        _myImage = currentBlueprint.image;
        _warning.gameObject.SetActive(false);
        _lockImage.SetActive(currentBlueprint.isLock);
    }

    private IEnumerator Warning() //사운드 추가하기
    {
        _warning.gameObject.SetActive(true);
        _warning.text = "잠겨있는 스테이지입니다.\n이전 스테이지를 클리어 해주세요.";
        yield return new WaitForSeconds(1.2f);
        _warning.gameObject.SetActive(false);
    }
}
