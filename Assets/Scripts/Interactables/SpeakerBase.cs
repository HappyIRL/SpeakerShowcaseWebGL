using UnityEngine;

namespace Assets.Scripts
{
	public enum SpeakerComponents
	{
		None,
		Casing,
		HPSWaveGuide,
		Tweeter,
		HPSScrews,
		MidRangeDriver,
		BigWoofer,
		BackplateSectionOne,
		BackplateSectionTwo,
		BackplateSectionThree,
		CasingScrewHollows
	}

	public class SpeakerBase : MonoBehaviour, Interactable
	{
		[SerializeField] private Vector3 uncasedPosition;
		[SerializeField] private Vector3 casedPosition;
		[SerializeField] private SpeakerComponents speakerPartType;
		[SerializeField] private AudioClip moveIn;
		[SerializeField] private AudioClip moveOut;

		private AudioClip currentSound;
		private Vector3 futurePosition;

		private bool inCasing = true;

		public Vector3 GetFuturePosition()
		{
			return futurePosition;
		}

		public SpeakerComponents GetSpeakerComponent()
		{
			return speakerPartType;
		}

		protected void MoveOutOfCasing()
		{
			inCasing = false;
			futurePosition = uncasedPosition;
			currentSound = moveOut;
			StartCoroutine(Utils.LerpToPosition(transform, uncasedPosition, 0.2f, 0));
		}

		protected void MoveIntoCasing()
		{
			inCasing = true;
			futurePosition = casedPosition;
			currentSound = moveIn;
			StartCoroutine(Utils.LerpToPosition(transform, casedPosition, 0.2f, 0));
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
