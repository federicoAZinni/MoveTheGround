using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedJoystick : Joystick,IPointerDownHandler
{
    bool ondrag;

    [SerializeReference] Image border1;
    [SerializeReference] Image handle1;
    private void Update()
    {
        //    transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //      border1.color = new Color(255, 255, 255, 0.1f);
        //      handle1.color = new Color(255, 255, 255, 0.1f);
        //    }
        //    if (Input.GetMouseButton(0))
        //        {
        //            OnDrag(Input.mousePosition);
        //        }

        //        if (Input.GetMouseButtonUp(0))
        //        {
        //            border1.color = new Color(255, 255, 255, 0f);
        //            handle1.color = new Color(255, 255, 255, 0f);
        //            base.Repos();
        //        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                border1.color = new Color(255, 255, 255, 0.1f);
                handle1.color = new Color(255, 255, 255, 0.1f);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                OnDrag(touch.position);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                border1.color = new Color(255, 255, 255, 0f);
                handle1.color = new Color(255, 255, 255, 0f);
                base.Repos();
            }
        }

    }

}