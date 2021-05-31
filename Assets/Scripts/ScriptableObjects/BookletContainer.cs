using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(menuName = "ScriptableObject/BookletData")]
	public class BookletContainer : ScriptableObject
	{
		public List<BookletData> BookletDatas = new List<BookletData>();

		public void IncrementDatas()
		{
			BookletDatas.Add(new BookletData());
		}
	}
}