using UnityEngine;

namespace Assets.Scripts
{ 
	public interface ISpeakerPart
	{
		public void ChangeCasingState();

		public Vector3 GetCurrentPosition();

		public bool GetCasingState();

		public SpeakerComponents GetSpeakerComponentType();

		public AudioClip GetAudioClip();
	}
}
