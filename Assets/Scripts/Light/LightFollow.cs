using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

public class LightFollow : PlayerCallbacksMono
{
	[Zenject.Inject] private InteractionHandler interactionsHandler;

	private Interactable speakerCasing;
	private Interactable target;
	private Vector3 currentTarget = Vector3.zero;
	private Coroutine activeCoroutine;

	protected override void OnEnable()
	{
		base.OnEnable();
		interactionsHandler.SpeakerCasingSpawn += OnSpeakerCasingSpawn;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		interactionsHandler.SpeakerCasingSpawn -= OnSpeakerCasingSpawn;
	}

	private void Start()
	{
		transform.LookAt(currentTarget);
	}

	private void Update()
	{
		if (target == null && speakerCasing != null)
		{
			currentTarget = speakerCasing.GetPosition();
			transform.LookAt(currentTarget);
		}
	}

	private void OnSpeakerCasingSpawn(Interactable interactable)
	{
		speakerCasing = interactable;
	}

	protected override void OnSelection(Interactable interactable)
	{
		target = interactable;

		if (activeCoroutine != null)
		{
			StopCoroutine(activeCoroutine);
			activeCoroutine = null;
		}

		activeCoroutine = StartCoroutine(Utils.LerpToPositionLookAt(transform, currentTarget, target.GetPosition(), 0.5f, 1f));

		currentTarget = target.GetPosition();
	}
}