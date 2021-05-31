using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class SelectionHandler : PlayerEventInstallerMono
	{
		[Zenject.Inject] private CameraController cameraController;

		private Camera sceneCamera;

		private Interactable currentInteractable;

		public event Action<Interactable> SelectionChange;

		public event Action<Interactable> SameSelection;


		private void Start()
		{
			sceneCamera = cameraController.GetComponent<Camera>();
		}

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
					if(currentInteractable == interactable)
					{
						SameSelection?.Invoke(interactable);
						return;
					}

					currentInteractable = interactable;
					SelectionChange?.Invoke(interactable);
				}
			}
		}
	}
}