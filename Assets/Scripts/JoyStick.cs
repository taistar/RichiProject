﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class JoyStick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler
{

	private Image bgImg;
	private Image joystickImg;
	private Vector3 inputVector;
	public enum joyStickEnum {onDrag, onPointerDown, onPointerUp};
	public joyStickEnum joystickenum = joyStickEnum.onPointerUp;
	//public Color color;
	private void Start()
	{
		
	//	joyStickEnum myJoyStick;
		bgImg = GetComponent<Image> ();
		joystickImg = transform.GetChild (0).GetComponent<Image> ();
		//color = joystickImg.color;
	
	}


	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)) {
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x * 2, pos.y * 2, 0);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

			joystickImg.rectTransform.anchoredPosition = 
				new Vector3 (inputVector.x * (bgImg.rectTransform.sizeDelta.x / 8)
					, inputVector.y * (bgImg.rectTransform.sizeDelta.y / 8));

			joystickenum = joyStickEnum.onDrag;

			print ("fuckdown");

		}

		}	
	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag (ped);
		print ("shit");
	}
	public virtual void OnPointerUp(PointerEventData ped)
	{
		inputVector = Vector3.zero;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
		joystickenum = joyStickEnum.onPointerUp;

		//joystickImg.color.a = 100;
	}

	public float Horizontal()
	{
		if (inputVector.x != 0)
			return inputVector.x;
		else
			return Input.GetAxis ("Horizontal");
	}

	public float Vertical()
	{
		if (inputVector.y != 0)
			return inputVector.y;
		else
			return Input.GetAxis ("Vertical");
	}

}
