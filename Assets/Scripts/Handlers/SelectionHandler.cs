using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class SelectionHandler : PlayerEventInstallerMono
	{
		public event Action<ISpeakerPart> SelectionChange;

		private Outline currentOutline;

		protected override void OnTouchInput(Vector2 position, HoverState state, GameObject go)
		{
			base.OnTouchInput(position, state, go);

			ChangeSelection(go);
		}

		protected override void OnHoverState(HoverState state, GameObject go)
		{
			if (state == HoverState.OverGameObject)
			{
				Outline outline = go.GetComponentInParent<Outline>();

				if (currentOutline != null && outline != currentOutline)
				{
					currentOutline.enabled = false;
				}

				currentOutline = outline;
				currentOutline.enabled = true;
			}
			else
			{
				if (currentOutline != null)
				{
					currentOutline.enabled = false;
					currentOutline = null;
				}
			}
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