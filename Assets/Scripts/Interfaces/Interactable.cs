using UnityEngine;

namespace Assets.Scripts
{ 
	public interface Interactable
	{
		public void Inspect();
		public void EndInteraction();
		public void Focus();
		public Vector3 GetPosition();
		public SpeakerPartType GetSpeakerPartType();
	}
}
