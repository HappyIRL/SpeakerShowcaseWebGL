using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class SelectionHandler : PlayerEventInstallerMono
	{
		public event Action<ISpeakerPart> SelectionChange;

		protected override void OnTouchInput(Vector2 position, HoverState state, GameObject go)
		{
			base.OnTouchInput(position, state, go);

			ChangeSelection(go);
		}

		private void ChangeSelection(GameObject objectHovered)
		{
			if(objectHovered != null)
			{
				ISpeakerPart interactable = objectHovered.GetComponentInParent<ISpeakerPart>();

				if (interactable != null)
				{
					SelectionChange?.Invoke(interactable);
				}
			}
		}
	}
}