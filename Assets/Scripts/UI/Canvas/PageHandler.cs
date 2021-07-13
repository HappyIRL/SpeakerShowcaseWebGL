using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class PageHandler : PlayerCallbacksMono
	{
		[SerializeField] private Page uiPage;

		private int pageAmount = -1;
		public int PageAmount => pageAmount;

		private int currentPage = 0;
		public int CurrentPage => currentPage;
		public int PageCountCurrentInspectedComponent => booklet[currentInspectedComponent].Count;

		public event Action<SpeakerComponents> ToggledPage;

		private Dictionary<SpeakerComponents, List<BookletData>> booklet = new Dictionary<SpeakerComponents, List<BookletData>>();
		private SpeakerComponents currentInspectedComponent = SpeakerComponents.None;
		private bool uiPageActive = false;

		public void AddPage(BookletData data)
		{
			if (data.Image == null)
			{
				Debug.LogError("Image in BookletData might be null!");
				return;
			}
		
			if(!booklet.ContainsKey(data.SpeakerComponent))
			{
				List<BookletData> pages = new List<BookletData>();
				pages.Add(data);
				booklet.Add(data.SpeakerComponent, pages);
			}
			else
			{
				booklet[data.SpeakerComponent].Add(data);
			}
		}

		protected override void OnSameSelection(Interactable interaction)
		{
			SpeakerComponents speakerComponent = interaction.GetSpeakerComponent();

			if (!booklet.ContainsKey(speakerComponent))
			{
				Debug.LogError($"BookletData does not have a page for this component: {speakerComponent}!");
				return;
			}

			currentInspectedComponent = speakerComponent;
			
		}

		public void ShowPage(int index)
		{
			uiPage.Header.text = booklet[currentInspectedComponent][index].Header;
			uiPage.Description.text = booklet[currentInspectedComponent][index].Description;
			uiPage.Image.sprite = booklet[currentInspectedComponent][index].Image;
		}

		public void TogglePage()
		{
			if (booklet.Count == 0)
				return;

			uiPage.gameObject.SetActive(!uiPageActive);
			currentPage = 0;

			if (!uiPageActive)
			{
				ShowPage(currentPage);
			}

			uiPageActive = !uiPageActive;

			ToggledPage?.Invoke(currentInspectedComponent);
		}

		public void NextPage()
		{
			if (currentPage == booklet[currentInspectedComponent].Count - 1)
				return;

			currentPage++;

			ShowPage(currentPage);
		}

		public void PreviousPage()
		{
			if (currentPage == 0)
				return;

			currentPage--;

			ShowPage(currentPage);
		}
	}
}
