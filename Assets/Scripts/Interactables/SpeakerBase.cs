using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public enum SpeakerPartType
	{
		Casing,
		Part
	}

	public class SpeakerBase : MonoBehaviour, Interactable
	{
		[SerializeField] private Vector3 uncasedPosition;
		[SerializeField] private Vector3 casedPosition;
		[SerializeField] private SpeakerPartType speakerPartType;

		public virtual void Inspect()
		{
			MoveOutOfCasing();
		}

		public virtual void EndInteraction()
		{
			MoveIntoCasing();
		}

		public virtual void Focus()
		{
			
		}

		public Vector3 GetPosition()
		{
			return transform.position;
		}

		public SpeakerPartType GetSpeakerPartType()
		{
			return speakerPartType;
		}

		protected void MoveOutOfCasing()
		{
			StartCoroutine(Utils.LerpToPosition(transform, uncasedPosition, 0.2f, 0));
		}

		protected void MoveIntoCasing()
		{
			StartCoroutine(Utils.LerpToPosition(transform, casedPosition, 0.2f, 0));
		}
	}
}
