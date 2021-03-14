using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class SelectionHandler : PlayerEventInstallerMono
	{
		[Zenject.Inject] private Camera sceneCamera;

		private Interactable currentInteractable;

		public event Action<Interactable> SelectionChange;

		protected override void OnTouchInput(Vector2 position)
		{
			base.OnTouchInput(position);

			UpdateSelection(position);
		}

		private void UpdateSelection(Vector2 position)
		{
			if (Physics.Raycast(sceneCamera.ScreenPointToRay(position), out RaycastHit hitInfo))
			{
				if (hitInfo.transform.TryGetComponent(out Interactable interactable))
				{
					currentInteractable = interactable;
					SelectionChange?.Invoke(interactable);
				}
			}
		}
	}
}