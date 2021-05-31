using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

public class LightFollow : PlayerCallbacksMono
{
	[Zenject.Inject] private CameraController cameraController;
	[Zenject.Inject] private InteractionHandler interactionHandler;

	private Interactable speakerCasing;
	private Interactable target;
	private Vector3 currentTarget = Vector3.zero;
	private Coroutine activeCoroutine;

	private void OnEnable()
	{
		cameraController.CameraMovement += OnCameraMovement;
		interactionHandler.SpeakerCasingSpawn += OnSpeakerCasingSpawn;
	}

	private void OnDisable()
	{
		cameraController.CameraMovement -= OnCameraMovement;
		interactionHandler.SpeakerCasingSpawn -= OnSpeakerCasingSpawn;

	}

	private void Start()
	{
		transform.LookAt(currentTarget);
	}

	private void Update()
	{
		if (target == null && speakerCasing != null)
		{
			currentTarget = speakerCasing.GetCurrentPosition();
			transform.LookAt(currentTarget);
		}
	}

	private void OnSpeakerCasingSpawn(Interactable interactable)
	{
		speakerCasing = interactable;
	}

	private void OnCameraMovement(Interactable interaction)
	{
		MoveLights(interaction);
	}

	private void MoveLights(Interactable interaction)
	{
		target = interaction;

		if (activeCoroutine != null)
		{
			StopCoroutine(activeCoroutine);
			activeCoroutine = null;
		}

		activeCoroutine = StartCoroutine(Utils.LerpToPositionLookAt(transform, currentTarget, target.GetFuturePosition(), 0.5f, 1f));

		currentTarget = target.GetFuturePosition();
	}
}