using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	[Serializable]
	public class BookletData
	{
		public SpeakerComponents SpeakerComponent;
		public string Header;
		[TextArea(10,50)]
		public string Description;
		public Sprite Image;
	}
}
