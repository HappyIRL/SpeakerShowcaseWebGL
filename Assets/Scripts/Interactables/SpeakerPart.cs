using UnityEngine;

namespace Assets.Scripts
{
	public class SpeakerPart : SpeakerBase
	{
		[SerializeField] private Vector3 spawnPosition;
		public Vector3 SpawnPosition => spawnPosition;
	}
}
