using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
	public class SpeakerFactory : MonoBehaviour
	{
		[Zenject.Inject] private PrefabFactory prefabFactory;

		[SerializeField] private GameObject speakerParent;
		[SerializeField] private List<GameObject> speakerParts;

		public event Action<ISpeakerPart> SpawnedInteractable;

		private void Start()
		{
			CreateSpeaker();
		}

		private void CreateSpeaker()
		{
			Transform parent = prefabFactory.Create(speakerParent);
			foreach (GameObject speaker in speakerParts)
			{
				Transform speakerPartInstance = prefabFactory.Create(speaker);
				speakerPartInstance.transform.parent = parent;
				SpawnedInteractable?.Invoke(speakerPartInstance.GetComponent<ISpeakerPart>());
			}
		}
	}
}
