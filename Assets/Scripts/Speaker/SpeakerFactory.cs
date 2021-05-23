using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
	public class SpeakerFactory : MonoBehaviour
	{

		[SerializeField] private GameObject speakerParent;
		[SerializeField] private List<GameObject> speakerParts;

		public event Action<Interactable> SpawnedInteractable;

		private void Start()
		{
			CreateSpeaker();
		}

		private void CreateSpeaker()
		{
			GameObject parent = Instantiate(speakerParent);
			foreach (GameObject speaker in speakerParts)
			{
				GameObject speakerPartInstance = Instantiate(speaker);
				speakerPartInstance.TryGetComponent(out SpeakerPart speakerPart);
				speaker.transform.position = speakerPart.SpawnPosition;
				speakerPartInstance.transform.parent = parent.transform;
				SpawnedInteractable?.Invoke(speakerPartInstance.GetComponent<Interactable>());
			}
		}
	}
}
