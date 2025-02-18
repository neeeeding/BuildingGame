using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickActive : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerInput.Instance.OnClickMouse += ClickMouse;
    }

    private void ClickMouse()
    {
        transform.position = Input.mousePosition;
    }

    private void OnDisable()
    {
        //PlayerInput.Instance.OnClickMouse -= ClickMouse;
    }
}
