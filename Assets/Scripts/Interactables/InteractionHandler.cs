using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class InteractionHandler : PlayerCallbacksMono
	{
		[Zenject.Inject] private SpeakerFactory speakerFactory;
		[Zenject.Inject] private CameraController cameraController;

		public event Action<Interactable> SpeakerCasingSpawn;
		public event Action<Interactable> SpeakerPartMovement;

		private List<Interactable> Interactables = new List<Interactable>();

		protected override void OnEnable()
		{
			base.OnEnable();
			speakerFactory.SpawnedInteractable += OnSpawnedInteractable;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			speakerFactory.SpawnedInteractable -= OnSpawnedInteractable;
		}

		protected override void OnSelection(Interactable interaction)
		{
			ChangeCasing(interaction);
		}

		protected override void OnSameSelection(Interactable interaction)
		{
			if(cameraController.CameraFocus == interaction)
				ChangeCasing(interaction);
		}

		private void ChangeCasing(Interactable interaction)
		{
			interaction.ChangePosition();
			SpeakerPartMovement?.Invoke(interaction);
		}

		private void OnSpawnedInteractable(Interactable interactable)
		{
			Interactables.Add(interactable);

			if (interactable.GetSpeakerComponent() == SpeakerComponents.Casing)
			{
				SpeakerCasingSpawn?.Invoke(interactable);
			}
		}
	}
}
