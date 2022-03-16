using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class InteractionHandler : PlayerCallbacksMono
	{
		[Zenject.Inject] private SpeakerFactory speakerFactory;

		public event Action<ISpeakerPart> SpeakerCasingSpawn;
		public event Action<ISpeakerPart> ChangeCasingState;

		private List<ISpeakerPart> Interactables = new List<ISpeakerPart>();

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

		protected override void OnSelection(ISpeakerPart interaction)
		{
			ChangeCasing(interaction);
		}

		private void ChangeCasing(ISpeakerPart interaction)
		{
			interaction.ChangeCasingState();
			ChangeCasingState?.Invoke(interaction);
		}

		private void OnSpawnedInteractable(ISpeakerPart interactable)
		{
			Interactables.Add(interactable);

			if (interactable.GetSpeakerComponentType() == SpeakerComponents.Casing)
			{
				SpeakerCasingSpawn?.Invoke(interactable);
			}
		}
	}
}
