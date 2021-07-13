using Assets.Scripts;
using UnityEngine;

public class LightFollow : PlayerCallbacksMono
{
	[Zenject.Inject] private CameraController cameraController;
	[Zenject.Inject] private InteractionHandler interactionHandler;

	private Interactable speakerCasing;
	private Interactable target;
	private Vector3 currentTarget = Vector3.zero;
	private Coroutine activeCoroutine;

	protected override void OnEnable()
	{
		base.OnEnable();
		cameraController.CameraMovement += OnCameraMovement;
		interactionHandler.SpeakerCasingSpawn += OnSpeakerCasingSpawn;
	}
	protected override void OnDisable()
	{
		base.OnDisable();
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