using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapSize : MonoBehaviour
{
    [SerializeField] private GameObject _blueprinnt; //설계도
    [SerializeField] private GameObject _map; //미니 맵
    [SerializeField] private GameObject _btn; // 사이즈 조절 버튼

    private RectTransform _size; //전체 사이즈
    private RectTransform _blueprinntSize; //설계도 사이즈
    private RectTransform _mapSize; //미니맵 사이즈

    private float _XsizeMax = 1860; //X 최대 사이즈
    private float _YsizeMax = 1020; //Y 최대 사이즈
    private float imageSize = 50; //미니맵과 설계도의 사이즈 = _size - imageSize
    private float _speed = 10; //변하는 속도

    private bool onBtn; // true : 버튼이 클릭중, false : 버튼에서 땠음.

    private void Awake()
    {
        _size = GetComponent<RectTransform>();
        _blueprinntSize = _blueprinnt.GetComponent<RectTransform>();
        _mapSize = _map.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (onBtn)
        {
            Size();
        }
    }

    private void Size()
    {
        Vector2 mousePos = new Vector2(Mathf.Clamp(Input.mousePosition.x,110,_XsizeMax), Mathf.Clamp(_YsizeMax + 60/*게임 크기*/ - Input.mousePosition.y, 110, _YsizeMax)); //마우스 위치 구하기

        Vector2 currentEntireSize = _size.sizeDelta; //현재 전체 사이즈
        Vector2 currentSize = _blueprinntSize.sizeDelta; //현재 설계도/미니맵 사이즈
        float targetEntireSize = Mathf.Min(Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y) ? mousePos.x : mousePos.y, _YsizeMax - 40); //바꿀 전체 사이즈
        float targetSize = Mathf.Min(targetEntireSize - imageSize, _YsizeMax - 90/*얘가 이것보다 커지면 곤란함*/); // 바꿀 설계도/미니맵 사이즈

        _size.sizeDelta = Vector2.Lerp(currentEntireSize, new Vector2(targetEntireSize, Mathf.Min(targetEntireSize + 40, _YsizeMax)/*아래가 터진 친구라*/), Time.deltaTime * _speed); //전체 사이즈 바꾸기

        _blueprinntSize.sizeDelta = Vector2.Lerp(currentSize, Vector2.one * targetSize, Time.deltaTime * _speed); //설계도 사이즈 바꾸기
        _mapSize.sizeDelta = Vector2.Lerp(currentSize, Vector2.one * targetSize, Time.deltaTime * _speed); //미니맵 사이즈 바꾸기
    }

    public void BtnDown()
    {
        onBtn = true;
    }

    public void BtnUp()
    {
        onBtn = false;
    }
}
