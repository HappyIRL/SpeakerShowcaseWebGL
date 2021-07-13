using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
	public enum HoverState
	{
		Nothing,
		OverGameObject,
		OverUIObject
	}

	public class PlayerInputHandler : MonoBehaviour
	{
		[Zenject.Inject] private CameraController cameraController;
		[Zenject.Inject] private EventSystem eventSystem;

		public event Action<Vector2, HoverState, GameObject> TouchInput;

		public event Action<Vector2, Vector2> DragInput;

		public event Action<Vector2> ScrollInput;

		private Vector2 beganInputPos;

		private Camera sceneCamera;

		private void Start()
		{
			sceneCamera = cameraController.GetComponent<Camera>();
		}

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
					HoverState result = HoveringOverObject(touch.position, out GameObject go);
					TouchInput?.Invoke(touch.position, result, go);
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
				HoverState result = HoveringOverObject(Input.mousePosition, out GameObject go);
				TouchInput?.Invoke(Input.mousePosition, result, go);
				beganInputPos = Input.mousePosition;
			}
			else if(Input.GetMouseButton(0))
			{
				DragInput?.Invoke(beganInputPos, Input.mousePosition);
				beganInputPos = Input.mousePosition;
			}
		}

		private HoverState HoveringOverObject(Vector2 mousePosition, out GameObject go)
		{
			go = null;

			if (eventSystem.IsPointerOverGameObject())
			{
				return HoverState.OverUIObject;
			}

			if(Physics.Raycast(sceneCamera.ScreenPointToRay(mousePosition), out RaycastHit hitInfo))
			{
				go = hitInfo.transform.gameObject;
				return HoverState.OverGameObject;
			}

			return HoverState.Nothing;
		}
	}
}