using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerCallbacksMono : MonoBehaviour
	{
		[Zenject.Inject] private SelectionHandler selectionHandler;
		[Zenject.Inject] private PlayerInputHandler playerInputHandler;

		protected virtual void OnEnable()
		{
			selectionHandler.SelectionChange += OnSelection;
			playerInputHandler.DragInput += OnDragInput;
			playerInputHandler.ScrollInput += OnScrollInput;
			playerInputHandler.TouchInput += OnTouchInput;

		}

		protected virtual void OnDisable()
		{
			selectionHandler.SelectionChange -= OnSelection;
			playerInputHandler.DragInput -= OnDragInput;
			playerInputHandler.ScrollInput -= OnScrollInput;
			playerInputHandler.TouchInput -= OnTouchInput;
		}

		protected virtual void OnSelection(ISpeakerPart interaction)
		{
		}

		protected virtual void OnTouchInput(Vector2 vector, HoverState state, GameObject go)
		{
		}

		protected virtual void OnDragInput(Vector2 start, Vector2 end)
		{
		}

		protected virtual void OnScrollInput(Vector2 scroll)
		{
		}
	}
}
