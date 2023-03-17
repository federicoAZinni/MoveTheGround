using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedJoystick : Joystick
{
    [SerializeField] Image borderImg;
    [SerializeField] Image handleImg;

    bool ondrag;
    private void Update()
    {
        if(!ondrag)transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        borderImg.color = new Color(255, 255, 255, 0.1f);
        handleImg.color = new Color(255, 255, 255, 0.1f);
        ondrag = true;
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        borderImg.color = new Color(255, 255, 255, 0f);
        handleImg.color = new Color(255, 255, 255, 0f);
        ondrag = false;
        base.OnPointerUp(eventData);
    }
}