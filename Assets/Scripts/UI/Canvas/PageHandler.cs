using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
	public class PageHandler : PlayerCallbacksMono
	{
		[SerializeField] private Page uiPage;

		[SerializeField] private Dictionary<SpeakerComponents,BookletData> Booklet = new Dictionary<SpeakerComponents, BookletData>();

		private bool uiPageActive = false;

		private int pageAmount = -1;
		public int PageAmount => pageAmount;

		private SpeakerComponents currentInspectedComponent = SpeakerComponents.None;
		public SpeakerComponents CurrentInspectedComponent => currentInspectedComponent;

		private SpeakerComponents lastPage = SpeakerComponents.None;
		public SpeakerComponents LastPage => lastPage;

		

		public void AddPage(BookletData data)
		{
			if (data.Image == null)
			{
				Debug.LogError("Image in BookletData might be null!");
				return;
			}

			if(Booklet.ContainsKey(data.SpeakerComponent))
			{
				Debug.LogError($"There can currently only be one page of {data.SpeakerComponent} in BookletData ");
				return;
			}

			Booklet.Add(data.SpeakerComponent, data);

			lastPage = data.SpeakerComponent;
		}

		protected override void OnSameSelection(Interactable interaction)
		{
			SpeakerComponents speakerComponent = interaction.GetSpeakerComponent();

			if (!Booklet.ContainsKey(speakerComponent))
			{
				Debug.LogError($"BookletData does not have a page for {speakerComponent}!");
				return;
			}

			currentInspectedComponent = speakerComponent;
			Debug.Log(currentInspectedComponent);
		}

		public void ShowPage()
		{
			if (Booklet.Count == 0)
				return;

			uiPage.Header.text = Booklet[currentInspectedComponent].Header;
			uiPage.Description.text = Booklet[currentInspectedComponent].Description;
			uiPage.Image.sprite = Booklet[currentInspectedComponent].Image;
		}

		public void TogglePage()
		{
			uiPage.gameObject.SetActive(!uiPageActive);

			if (!uiPageActive)
			{
				ShowPage();
			}

			uiPageActive = !uiPageActive;
		}

		public void NextPage()
		{
			currentInspectedComponent++;

			ShowPage();
		}

		public void PreviousPage()
		{
			if (currentInspectedComponent == 0)
				return;

			currentInspectedComponent--;

			ShowPage();
		}
	}
}
