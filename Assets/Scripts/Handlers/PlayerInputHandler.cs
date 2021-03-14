using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerInputHandler : MonoBehaviour
	{
		public event Action<Vector2> TouchInput;

		public event Action<Vector2, Vector2> DragInput;

		public event Action<Vector2> ScrollInput;

		private Vector2 beganInputPos;

		private Vector2 lastMouseScrollData;

		private void Update()
		{
			UpdateTouchInput();
			UpdateScrollInput();
		}

		private void UpdateScrollInput()
		{
			if (Input.mouseScrollDelta != Vector2.zero)
			{
				ScrollInput?.Invoke(Input.mouseScrollDelta);
			}
		}

		private void UpdateTouchInput()
		{
			if (Input.touches.Length > 0)
			{
				Touch touch = Input.touches[0];
				if (touch.phase == TouchPhase.Began)
				{
					TouchInput?.Invoke(touch.position);
					beganInputPos = touch.position;
				}
				else if (touch.phase == TouchPhase.Moved)
				{
					DragInput?.Invoke(beganInputPos, touch.position);
					beganInputPos = touch.position;
				}
			}
			else if (Input.GetMouseButtonDown(0))
			{
				TouchInput?.Invoke(Input.mousePosition);
				beganInputPos = Input.mousePosition;
			}
			else if(Input.GetMouseButton(0))
			{
				DragInput?.Invoke(beganInputPos, Input.mousePosition);
				beganInputPos = Input.mousePosition;
			}
		}
	}
}