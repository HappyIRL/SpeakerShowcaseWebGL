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

		private bool isOutOfCasing;

		public virtual void Inspect()
		{
			MoveOutOfCasing();
			isOutOfCasing = true;
		}

		public virtual void EndInteraction()
		{
			MoveIntoCasing();
			isOutOfCasing = false;
		}

		public virtual void Focus()
		{
			
		}

		public Vector3 GetFuturePosition()
		{
			if (isOutOfCasing)
				return uncasedPosition;
			else
				return casedPosition;
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

		public bool IsOutOfCasing()
		{
			return isOutOfCasing;
		}

		public Vector3 GetCurrentPosition()
		{
			return transform.position;
		}
	}
}
