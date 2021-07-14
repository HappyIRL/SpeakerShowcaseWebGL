using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(menuName = "ScriptableObject/BookletData")]
	public class BookletContainer : ScriptableObject
	{
		public List<BookletData> BookletDatas => bookletDatas;

		[SerializeField] private List<BookletData> bookletDatas = new List<BookletData>();

		public void IncrementDatas()
		{
			BookletDatas.Add(new BookletData());
		}
	}
}