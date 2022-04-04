using UnityEngine;

namespace Assets.Scripts
{
	public enum SpeakerComponents
	{
		None,
		Mounting,
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
		[SerializeField] private float moveOutScalar = 0;

		private Vector3 moveOutPosition;
		private Vector3 casedPosition;
		private int explodeAnimationID;
		private int implodeAnimationID;
		private bool inCasing = true;
		private Coroutine activeCoroutine;

		private AudioClip currentSound;

		private void Awake()
		{
			explodeAnimationID = Animator.StringToHash("Explode");
			implodeAnimationID = Animator.StringToHash("Implode");
		}

		private void Start()
		{
			casedPosition = transform.position;
			moveOutPosition = casedPosition + transform.up * moveOutScalar;
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

			animation.SetTrigger(explodeAnimationID);

			if (moveOutScalar != 0)
			{
				LerpToNewPosition(moveOutPosition);
			}
		}

		public void MoveIntoCasing()
		{
			if (animation == null)
				return;

			animation.SetTrigger(implodeAnimationID);

			if(moveOutScalar != 0)
				LerpToNewPosition(casedPosition);

			
		}

		public void ChangeCasingState()
		{
			if (inCasing)
				MoveOutOfCasing();
			else
				MoveIntoCasing();

			inCasing = !inCasing;
		}

		private void LerpToNewPosition(Vector3 pos)
		{
			if (activeCoroutine != null)
			{
				StopCoroutine(activeCoroutine);
				activeCoroutine = null;
			}

			activeCoroutine = StartCoroutine(Utils.LerpToPosition(transform, pos, 0.4f, 0));
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
