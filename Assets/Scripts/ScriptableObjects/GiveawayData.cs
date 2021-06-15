using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(menuName = "ScriptableObject/GiveawayData")]
	public class GiveawayData : ScriptableObject
	{
		public string Header;
		public string Description;
	}
}

