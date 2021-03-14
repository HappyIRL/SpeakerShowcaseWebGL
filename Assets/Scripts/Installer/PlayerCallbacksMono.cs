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
		}

		protected virtual void OnDisable()
		{
			selectionHandler.SelectionChange -= OnSelection;
			playerInputHandler.DragInput -= OnDragInput;
			playerInputHandler.ScrollInput -= OnScrollInput;
		}

		protected virtual void OnSelection(Interactable interactable)
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
