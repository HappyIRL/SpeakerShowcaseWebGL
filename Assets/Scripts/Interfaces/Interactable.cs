using UnityEngine;

namespace Assets.Scripts
{ 
	public interface Interactable
	{
		public void Inspect();
		public void EndInteraction();
		public bool IsOutOfCasing();
		public void Focus();
		public Vector3 GetFuturePosition();
		public Vector3 GetCurrentPosition();
		public SpeakerPartType GetSpeakerPartType();
	}
}
