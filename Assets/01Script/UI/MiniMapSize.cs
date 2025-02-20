using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapSize : MonoBehaviour
{
    [SerializeField] private GameObject _blueprinnt; //���赵
    [SerializeField] private GameObject _map; //�̴� ��
    [SerializeField] private GameObject _btn; // ������ ���� ��ư

    private RectTransform _size; //��ü ������
    private RectTransform _blueprinntSize; //���赵 ������
    private RectTransform _mapSize; //�̴ϸ� ������

    private float _XsizeMax = 1860; //X �ִ� ������
    private float _YsizeMax = 1020; //Y �ִ� ������
    private float imageSize = 50; //�̴ϸʰ� ���赵�� ������ = _size - imageSize
    private float _speed = 10; //���ϴ� �ӵ�

    private bool onBtn; // true : ��ư�� Ŭ����, false : ��ư���� ����.

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
        Vector2 mousePos = new Vector2(Mathf.Clamp(Input.mousePosition.x,110,_XsizeMax), Mathf.Clamp(_YsizeMax + 60/*���� ũ��*/ - Input.mousePosition.y, 110, _YsizeMax)); //���콺 ��ġ ���ϱ�

        Vector2 currentEntireSize = _size.sizeDelta; //���� ��ü ������
        Vector2 currentSize = _blueprinntSize.sizeDelta; //���� ���赵/�̴ϸ� ������
        float targetEntireSize = Mathf.Min(Mathf.Abs(mousePos.x) > Mathf.Abs(mousePos.y) ? mousePos.x : mousePos.y, _YsizeMax - 40); //�ٲ� ��ü ������
        float targetSize = Mathf.Min(targetEntireSize - imageSize, _YsizeMax - 90/*�갡 �̰ͺ��� Ŀ���� �����*/); // �ٲ� ���赵/�̴ϸ� ������

        _size.sizeDelta = Vector2.Lerp(currentEntireSize, new Vector2(targetEntireSize, Mathf.Min(targetEntireSize + 40, _YsizeMax)/*�Ʒ��� ���� ģ����*/), Time.deltaTime * _speed); //��ü ������ �ٲٱ�

        _blueprinntSize.sizeDelta = Vector2.Lerp(currentSize, Vector2.one * targetSize, Time.deltaTime * _speed); //���赵 ������ �ٲٱ�
        _mapSize.sizeDelta = Vector2.Lerp(currentSize, Vector2.one * targetSize, Time.deltaTime * _speed); //�̴ϸ� ������ �ٲٱ�
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
