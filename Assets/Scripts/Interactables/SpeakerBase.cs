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

	public class SpeakerBase : MonoBehaviour, ISpeakerPart
	{
		[SerializeField] private SpeakerComponents speakerPartType;
		[SerializeField] private AudioClip moveIn;
		[SerializeField] private AudioClip moveOut;
		[SerializeField] private Animator animation;

		private int ExplodeAnimationID;
		private int ImplodeAnimationID;
		private bool inCasing = true;

		private AudioClip currentSound;

		private void Awake()
		{
			ExplodeAnimationID = Animator.StringToHash("Explode");
			ImplodeAnimationID = Animator.StringToHash("Implode");
		}

		public bool GetCasingState()
		{
			return inCasing;
		}

		public SpeakerComponents GetSpeakerComponentType()
		{
			return speakerPartType;
		}

		public void MoveOutOfCasing()
		{
			if (animation == null)
				return;

			animation.SetTrigger(ExplodeAnimationID);
		}

		public void MoveIntoCasing()
		{
			if (animation == null)
				return;

			animation.SetTrigger(ImplodeAnimationID);
		}

		public void ChangeCasingState()
		{
			if (inCasing)
				MoveOutOfCasing();
			else
				MoveIntoCasing();

			inCasing = !inCasing;
		}

		public Vector3 GetCurrentPosition()
		{
			return transform.position;
		}

		public AudioClip GetAudioClip()
		{
			return currentSound;
		}
	}
}
