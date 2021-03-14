using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerParent : MonoBehaviour
{
	[SerializeField] private Vector3 finalSpeakerPosition;

	private Vector3 startPosition;

	private float lerpTime;

	private void Start()
	{
		startPosition = transform.position;
		StartCoroutine(Utils.LerpToPosition(transform, finalSpeakerPosition, 3f, 1f));
	}
}
