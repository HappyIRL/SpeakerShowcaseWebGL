using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class InteractionHandler : PlayerCallbacksMono
	{
		[Zenject.Inject] private SpeakerFactory speakerFactory;

		public event Action<Interactable> SpeakerCasingSpawn;

		private List<Interactable> Interactables = new List<Interactable>();
		private bool isCasingClosed = true;

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
			if (interaction.GetSpeakerPartType() == SpeakerPartType.Casing)
			{
				ChangeCasing();
			}
		}

		private void ChangeCasing()
		{
			if (!SpeakerParent.IsSpeakerInteractable)
				return;

			foreach (var interactable in Interactables)
			{
				if (isCasingClosed)
					interactable.Inspect();
				else
					interactable.EndInteraction();
			}

			isCasingClosed = !isCasingClosed;
		}
		private void OnSpawnedInteractable(Interactable interactable)
		{
			Interactables.Add(interactable);

			if (interactable.GetSpeakerPartType() == SpeakerPartType.Casing)
			{
				SpeakerCasingSpawn?.Invoke(interactable);
			}
		}
	}
}
