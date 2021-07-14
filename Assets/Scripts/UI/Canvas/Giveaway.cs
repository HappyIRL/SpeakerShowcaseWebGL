using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
	public class Giveaway : MonoBehaviour
	{
		[Zenject.Inject] private CanvasHandler canvasHandler;

		[SerializeField] private GiveawayData giveawayData;
		[SerializeField] private TMP_Text header;
		[SerializeField] private TMP_Text description;
		[SerializeField] private GameObject giveawayUI;
		[SerializeField] private GameObject openGiveawayUI;

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
				pageHandler.ToggledPage += UniquePageCheck;
		}

		private void OnDisable()
		{
			if (pageHandler != null)
				pageHandler.ToggledPage -= UniquePageCheck;
		}

		public void OnClick_CopyToClipBoard()
		{
			GUIUtility.systemCopyBuffer = giveawayData.GiveawayCode;

		}

		private void UniquePageCheck(int openedPage)
		{
			if (wonGiveaway == true || uniqueInspectedPages.Contains(openedPage))
				return;

			uniqueInspectedPages.Add(openedPage);

			if (uniqueInspectedPages.Count == 3)
			{
				CompleteGiveaway();
				wonGiveaway = true;
			}
		}

		private void CompleteGiveaway()
		{
			giveawayUI.SetActive(true);
			openGiveawayUI.SetActive(true);

			if (giveawayData == null)
			{
				Debug.LogError("Need GiveawayData on the Giveaway component!");
				return;
			}

			header.text = giveawayData.Header;
			description.text = giveawayData.Description;
		}
	}
}
