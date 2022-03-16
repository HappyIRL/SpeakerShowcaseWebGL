using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class PageHandler : PlayerCallbacksMono
	{
		[Zenject.Inject] private BookletContainer bookletContainer;

		[SerializeField] private Page uiPage;

		public int PageCount => booklet.Count;

		public int CurrentPage => currentPage;
		private int currentPage = 0;


		public event Action<int> ToggledPage;

		private Dictionary<SpeakerComponents, int> bookletMapper = new Dictionary<SpeakerComponents, int>();
		private List<BookletData> booklet = new List<BookletData>();
		private bool uiPageActive = false;

		private void Start()
		{
			foreach (BookletData data in bookletContainer.BookletDatas)
			{
				AddPage(data);
			}
		}

		public void AddPage(BookletData data)
		{
			if (data.Image == null)
			{
				Debug.LogError("Image in BookletData might be null!");
				return;
			}

			if (bookletMapper.ContainsKey(data.SpeakerComponent))
			{
				Debug.LogError("Tried adding an additional booklet data into booklet with same speaker component id, this is not allowed!");
				return;
			}

			booklet.Add(data);
			bookletMapper.Add(data.SpeakerComponent, booklet.Count - 1);
		}

		protected override void OnSelection(ISpeakerPart interaction)
		{
			if (interaction.GetCasingState())
				return;

			SpeakerComponents speakerComponent = interaction.GetSpeakerComponentType();

			if (!bookletMapper.ContainsKey(speakerComponent))
			{
				Debug.LogError($"BookletData does not have a page for this component: {speakerComponent}!");
				return;
			}

			currentPage = bookletMapper[speakerComponent];
			
		}

		public void ShowPage()
		{
			BookletData data = booklet[currentPage];

			uiPage.Header.text = data.Header;
			uiPage.Description.text = data.Description;
			uiPage.Image.sprite = data.Image;
		}

		public void TogglePage()
		{
			if (booklet.Count == 0)
				return;

			uiPage.gameObject.SetActive(!uiPageActive);

			if (!uiPageActive)
			{
				ShowPage();
			}

			uiPageActive = !uiPageActive;

			ToggledPage?.Invoke(currentPage);
		}

		public void NextPage()
		{
			if (currentPage >= booklet.Count - 1)
				return;

			currentPage++;

			ShowPage();
		}

		public void PreviousPage()
		{
			if (currentPage <= 0)
				return;

			currentPage--;

			ShowPage();
		}
	}
}
