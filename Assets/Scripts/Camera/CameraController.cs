using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class CameraController : PlayerCallbacksMono
	{
		[Zenject.Inject] private InteractionHandler interactionHandler;

		public event Action<ISpeakerPart> CameraMovement;

		[SerializeField] private float maxZoom;
		[SerializeField] private float minZoom;

		private Vector3 localRotation = Vector3.zero;
		private Transform parentTransform;
		private Coroutine activeCoroutine;
		private bool isInititalHoverStateNothing = false;
		private Vector3 defaultPosition;

		private void Awake()
		{
			transform.LookAt(transform.parent);
			parentTransform = transform.parent;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			interactionHandler.SpeakerCasingSpawn += OnSpeakerCasingSpawn;
			interactionHandler.ChangeCasingState += OnChangeCasingState;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			interactionHandler.SpeakerCasingSpawn -= OnSpeakerCasingSpawn;
		}

		private void OnSpeakerCasingSpawn(ISpeakerPart interactable)
		{
			Vector3 pos = interactable.GetCurrentPosition();

			parentTransform.position = pos;
			defaultPosition = pos;
		}

		private void OnChangeCasingState(ISpeakerPart interaction)
		{
			if (!interaction.GetCasingState())
				LerpToNewPosition(interaction.GetCurrentPosition());
			else
			{
				LerpToNewPosition(defaultPosition);
			}

			CameraMovement?.Invoke(interaction);
		}

		protected override void OnTouchInput(Vector2 start, HoverState state, GameObject go)
		{
			if (state == HoverState.Nothing)
				isInititalHoverStateNothing = true;
			else
				isInititalHoverStateNothing = false;

		}
		protected override void OnDragInput(Vector2 start, Vector2 end)
		{
			if(isInititalHoverStateNothing)
				SetParentRotation(start, end);
		}

		protected override void OnScrollInput(Vector2 scroll)
		{
			SetParentScale(scroll);
		}

		private void LerpToNewPosition(Vector3 pos)
		{
			if (activeCoroutine != null)
			{
				StopCoroutine(activeCoroutine);
				activeCoroutine = null;
			}

			activeCoroutine = StartCoroutine(Utils.LerpToPosition(transform.parent, pos, 1f, 0));
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
