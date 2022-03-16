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
			interactionHandler.ChangeCasingState += OnChangeCasingState;
		}

		private void OnDisable()
		{
			interactionHandler.ChangeCasingState -= OnChangeCasingState;

		}

		private void OnChangeCasingState(ISpeakerPart interaction)
		{
			PlayInteraction(interaction);
		}

		private void PlayInteraction(ISpeakerPart interaction)
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