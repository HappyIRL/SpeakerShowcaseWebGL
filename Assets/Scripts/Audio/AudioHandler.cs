using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class AudioHandler : MonoBehaviour
	{
		private AudioSource audioSource;

		private void Start()
		{
			audioSource = GetComponent<AudioSource>();
		}
	}
}