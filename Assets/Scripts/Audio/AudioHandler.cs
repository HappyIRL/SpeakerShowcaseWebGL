using UnityEngine;

namespace Assets.Scripts
{
	public class AudioHandler : MonoBehaviour
	{
		[Zenject.Inject] private InteractionHandler interactionHandler;

		private AudioSource audioSource;

		private void Start()
		{
			audioSource = GetComponent<AudioSource>();
		}

		private void OnEnable()
		{
			interactionHandler.SpeakerPartMovement += OnSpeakerPartMovement;
		}

		private void OnDisable()
		{
			interactionHandler.SpeakerPartMovement -= OnSpeakerPartMovement;

		}

		private void OnSpeakerPartMovement(Interactable interaction)
		{
			PlayInteraction(interaction);
		}

		private void PlayInteraction(Interactable interaction)
		{
			var clip = interaction.GetAudioClip();
			if (clip != null)
			{
				audioSource.clip = interaction.GetAudioClip();
				audioSource.Play();
			}
		}
	}
}