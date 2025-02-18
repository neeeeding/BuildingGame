using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI : MonoBehaviour
{
    private bool isHide; //true = ¼û´Ù, false = º¸ÀÌ´Ù
    [SerializeField] private Transform basePosition;
    [SerializeField] private Transform movePosition;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        isHide = false;
    }

    public void ClickBtn()
    {
        isHide = !isHide;
    }

    private void Update()
    {
        if(isHide ? gameObject.transform.position != movePosition.position : gameObject.transform.position != basePosition.position)
        {
            Move();
        }
    }

    private void Move()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, isHide? movePosition.position : basePosition.position, moveSpeed);
    }
}
