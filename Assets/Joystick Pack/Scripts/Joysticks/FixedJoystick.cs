using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedJoystick : Joystick,IPointerDownHandler
{
    bool ondrag;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                UIManager.INS.border1.color = new Color(255, 255, 255, 0.1f);
                UIManager.INS.handle1.color = new Color(255, 255, 255, 0.1f);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                OnDrag(touch.position);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                UIManager.INS.border1.color = new Color(255, 255, 255, 0f);
                UIManager.INS.handle1.color = new Color(255, 255, 255, 0f);
                base.Repos();
            }
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        
    }
   
}