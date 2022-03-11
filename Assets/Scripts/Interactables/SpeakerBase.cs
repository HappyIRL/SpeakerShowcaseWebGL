using TreeEditor;
using UnityEngine;

namespace Assets.Scripts
{
	public enum SpeakerComponents
	{
		None,
		Casing,
		Tweeter,
		MidRangeDriver,
		Woofer,
		Backplate
	}

	public class SpeakerBase : MonoBehaviour, Interactable
	{
		private Vector3 uncasedPosition;
		private Vector3 casedPosition;

		[SerializeField] private SpeakerComponents speakerPartType;
		[SerializeField] private AudioClip moveIn;
		[SerializeField] private AudioClip moveOut;
		[SerializeField] private float moveOutScalar = 1.5f;

		[SerializeField] private Animator animation;

		private int ExplodeAnimationID;
		private int ImplodeAnimationID;

		private AudioClip currentSound;

		private bool inCasing = true;

		private void Awake()
		{
			ExplodeAnimationID = Animator.StringToHash("Explode");
			ImplodeAnimationID = Animator.StringToHash("Implode");
			casedPosition = transform.position;
			uncasedPosition = transform.position + transform.up.normalized * moveOutScalar;
		}

		public Vector3 GetUncasedPosition()
		{
			return uncasedPosition;
		}

		public SpeakerComponents GetSpeakerComponent()
		{
			return speakerPartType;
		}

		protected void MoveOutOfCasing()
		{
			if(animation != null)
				animation.SetTrigger(ExplodeAnimationID);

			StartCoroutine(Utils.LerpToPosition(transform, uncasedPosition, 0.2f, 0));
			inCasing = false;
		}

		protected void MoveIntoCasing()
		{
			if (animation != null)
				animation.SetTrigger(ImplodeAnimationID);

			StartCoroutine(Utils.LerpToPosition(transform, casedPosition, 0.2f, 0));
			inCasing = true;
		}

		public Vector3 GetCurrentPosition()
		{
			return transform.position;
		}

		public AudioClip GetAudioClip()
		{
			return currentSound;
		}

		public void ChangePosition()
		{
			if (speakerPartType == SpeakerComponents.Casing)
				return;

			if (inCasing)
			{
				MoveOutOfCasing();
			}
			else
			{
				MoveIntoCasing();
			}
		}
	}
}
