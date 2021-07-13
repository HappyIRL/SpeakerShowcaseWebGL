using UnityEngine;

namespace Assets.Scripts
{ 
	public interface Interactable
	{
		public void ChangePosition();

		public Vector3 GetFuturePosition();

		public Vector3 GetCurrentPosition();

		public SpeakerComponents GetSpeakerComponent();

		public AudioClip GetAudioClip();
	}
}
