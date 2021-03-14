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
		[SerializeField] private Vector3 speakerPartSpawn;

		public event Action<Interactable> SpawnedInteractable;

		private void Start()
		{
			CreateSpeaker();
		}

		private void CreateSpeaker()
		{
			GameObject parent = Instantiate(speakerParent);
			foreach (GameObject speakerPart in speakerParts)
			{
				GameObject speakerPartInstance = Instantiate(speakerPart, speakerPartSpawn, speakerPart.transform.rotation);
				speakerPartInstance.transform.parent = parent.transform;
				SpawnedInteractable?.Invoke(speakerPartInstance.GetComponent<Interactable>());
			}
		}
	}
}
