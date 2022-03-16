using Assets.Scripts;
using UnityEngine;

public class LightFollow : PlayerCallbacksMono
{
	[Zenject.Inject] private CameraController cameraController;

	private ISpeakerPart target;
	private Vector3 currentTarget = Vector3.zero;
	private Coroutine activeCoroutine;

	protected override void OnEnable()
	{
		base.OnEnable();
		cameraController.CameraMovement += OnCameraMovement;
	}
	protected override void OnDisable()
	{
		base.OnDisable();
		cameraController.CameraMovement -= OnCameraMovement;
	}

	private void Start()
	{
		transform.LookAt(currentTarget);
	}

	private void OnCameraMovement(ISpeakerPart interaction)
	{
		MoveLights(interaction);
	}

	private void MoveLights(ISpeakerPart interaction)
	{
		target = interaction;

		if (activeCoroutine != null)
		{
			StopCoroutine(activeCoroutine);
			activeCoroutine = null;
		}

		activeCoroutine = StartCoroutine(Utils.LerpToPositionLookAt(transform, currentTarget, target.GetCurrentPosition(), 0.5f, 0f));

		currentTarget = target.GetCurrentPosition();
	}
}