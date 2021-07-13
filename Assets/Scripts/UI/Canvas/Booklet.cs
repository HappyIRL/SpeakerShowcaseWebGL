using UnityEngine;

namespace Assets.Scripts
{
	public class Booklet : MonoBehaviour
	{
		// references should be given through the constructor and it shouldnt be a mono behaviour
		[SerializeField] private BookletContainer bookletContent;
		[SerializeField] private PageHandler pageHandler;
		[SerializeField] private GameObject NextButton;
		[SerializeField] private GameObject PreviousButton;

		private void Start()
		{
			foreach (BookletData data in bookletContent.BookletDatas)
			{
				pageHandler.AddPage(data);
			}
		}

		public void OnClick_OpenBooklet()
		{
			pageHandler.TogglePage();
			EnableButtons();
		}

		public void OnClick_NextPage()
		{
			pageHandler.NextPage();
			EnableButtons();
		}

		public void OnClick_PreviousPage()
		{
			pageHandler.PreviousPage();
			EnableButtons();
		}

		private void EnableButtons()
		{
			if (pageHandler.CurrentPage == pageHandler.PageCountCurrentInspectedComponent - 1)
				NextButton.SetActive(false);
			else
				NextButton.SetActive(true);

			if (pageHandler.CurrentPage == 0)
				PreviousButton.SetActive(false);
			else
				PreviousButton.SetActive(true);
		}
	}
}
