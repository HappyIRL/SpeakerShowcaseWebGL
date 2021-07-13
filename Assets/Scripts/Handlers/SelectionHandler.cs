using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class SelectionHandler : PlayerEventInstallerMono
	{
		[SerializeField] private LayerMask uiLayerMask;

		private Interactable lastInteraction;

		public event Action<Interactable> SelectionChange;

		public event Action<Interactable> SameSelection;

		protected override void OnTouchInput(Vector2 position, HoverState state, GameObject go)
		{
			base.OnTouchInput(position, state, go);

			UpdateSelection(go);
		}

		private void UpdateSelection(GameObject objectHovered)
		{
			if(objectHovered != null)
			{
				if (objectHovered.TryGetComponent(out Interactable interactable))
				{
					if (!SpeakerParent.IsSpeakerInteractable)
						return;

					if (lastInteraction == interactable)
					{
						SameSelection?.Invoke(interactable);
						return;
					}

					lastInteraction = interactable;
					SelectionChange?.Invoke(interactable);
				}
			}
		}
	}
}