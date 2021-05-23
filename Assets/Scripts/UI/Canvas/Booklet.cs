using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booklet : MonoBehaviour
{
	[SerializeField] private BookletContainer bookletContent;
	[SerializeField] private PageHandler pageHandler;
	[SerializeField] private GameObject NextButton;
	[SerializeField] private GameObject PreviousButton;

	private void Start()
	{
		foreach(BookletData data in bookletContent.BookletDatas)
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
		if (pageHandler.CurrentPage == pageHandler.PageAmount)
			NextButton.SetActive(false);
		else
			NextButton.SetActive(true);

		if (pageHandler.CurrentPage == 0)
			PreviousButton.SetActive(false);
		else
			PreviousButton.SetActive(true);

		Debug.Log(pageHandler.CurrentPage);
	}
}
