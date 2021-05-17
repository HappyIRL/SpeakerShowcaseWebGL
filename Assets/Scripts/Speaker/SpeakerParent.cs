using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerParent : MonoBehaviour
{
	[SerializeField] private Vector3 finalSpeakerPosition;

	public static bool IsSpeakerInteractable = false;

	private Vector3 startPosition;

	private float lerpTime = 3f;
	private float delay = 1f;

	private void Start()
	{
		startPosition = transform.position;
		StartCoroutine(Utils.LerpToPosition(transform, finalSpeakerPosition, lerpTime, delay));
		StartCoroutine(OnFinalPosition());

	}

	private IEnumerator OnFinalPosition()
	{
		yield return new WaitForSeconds(lerpTime + delay);

		IsSpeakerInteractable = true;

		yield return null;
	}
}
