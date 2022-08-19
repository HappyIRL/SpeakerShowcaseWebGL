using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
	public class Giveaway : MonoBehaviour
	{
		[Zenject.Inject] private CanvasHandler canvasHandler;

		[SerializeField] private GameObject giveawayUI;
		[SerializeField] private GameObject openGiveawayUI;
		[SerializeField] private string giveawayCode;

		private PageHandler pageHandler;
		private List<int> uniqueInspectedPages = new List<int>();
		private bool wonGiveaway = false;

		private void Awake()
		{
			pageHandler = canvasHandler.PageHandler;
		}

		private void OnEnable()
		{
			if (pageHandler != null)
				pageHandler.PageSelection += UniquePageSelectionCheck;
		}

		private void OnDisable()
		{
			if (pageHandler != null)
				pageHandler.PageSelection -= UniquePageSelectionCheck;
		}

		private void UniquePageSelectionCheck(int openedPage)
		{
			if (wonGiveaway || uniqueInspectedPages.Contains(openedPage))
				return;

			uniqueInspectedPages.Add(openedPage);

			if (uniqueInspectedPages.Count == 4)
			{
				CompleteGiveaway();
				wonGiveaway = true;
			}
		}

		private void CompleteGiveaway()
		{
			giveawayUI.SetActive(true);
			openGiveawayUI.SetActive(true);
		}
	}
}
