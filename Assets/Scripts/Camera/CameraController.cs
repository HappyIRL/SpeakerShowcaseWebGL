using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class CameraController : PlayerCallbacksMono
	{
		[SerializeField] private float maxZoom;
		[SerializeField] private float minZoom;

		[Zenject.Inject] private InteractionHandler interactionHandler;

		private Interactable target;
		private Vector3 localRotation = Vector3.zero;
		private Transform parentTransform;
		private Interactable speakerCasing;
		private Coroutine activeCoroutine;

		private void Awake()
		{
			transform.LookAt(transform.parent);
			parentTransform = transform.parent;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			interactionHandler.SpeakerCasingSpawn += OnSpeakerCasingSpawn;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			interactionHandler.SpeakerCasingSpawn -= OnSpeakerCasingSpawn;
		}

		private void Update()
		{
			if (target == null)
			{
				parentTransform.position = speakerCasing.GetPosition();
			}
		}

		private void OnSpeakerCasingSpawn(Interactable interactable)
		{
			speakerCasing = interactable;
		}

		protected override void OnSelection(Interactable interaction)
		{
			target = interaction;

			LerpToNewPosition();
		}

		protected override void OnDragInput(Vector2 start, Vector2 end)
		{
			SetParentRotation(start, end);
		}

		protected override void OnScrollInput(Vector2 scroll)
		{
			SetParentScale(scroll);
		}

		private void LerpToNewPosition()
		{
			if (activeCoroutine != null)
			{
				StopCoroutine(activeCoroutine);
				activeCoroutine = null;
			}

			activeCoroutine = StartCoroutine(Utils.LerpToPosition(transform.parent, target.GetPosition(), 0.5f, 0));
		}

		private void SetParentRotation(Vector2 start, Vector2 end)
		{
			Vector3 dir = (end - start);
			Vector3 orthogonalVector2 = new Vector2(-dir.y, dir.x);

			localRotation += orthogonalVector2;
			localRotation.x = Mathf.Clamp(localRotation.x, -80, 80);
			parentTransform.eulerAngles = localRotation;
		}

		private void SetParentScale(Vector2 scroll)
		{
			Vector3 scaler = new Vector3(scroll.y * 0.1f, scroll.y * 0.1f, scroll.y * 0.1f);
			Vector3 newScale = parentTransform.localScale - scaler;

			if (newScale.x < minZoom || newScale.x > maxZoom)
				return;

			parentTransform.localScale = newScale;
		}
	}
}
