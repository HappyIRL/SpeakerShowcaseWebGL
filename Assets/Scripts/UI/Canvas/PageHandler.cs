using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageHandler : MonoBehaviour
{
	[SerializeField] private Page uiPage;

	[SerializeField] private List<BookletData> pages = new List<BookletData>();

	private bool uiPageActive = false;

	private int pageAmount = -1;
	private int currentPage = 0;

	public int CurrentPage => currentPage;
	public int PageAmount => pageAmount;

	public void AddPage(BookletData data)
	{
		if(data.Image == null)
		{
			Debug.LogError("Missing Image in BookletData");
			return;
		}

		pages.Add(data);
		pageAmount++;
	}

	public void ShowPage()
	{
		if (pageAmount < 0)
			return;

		uiPage.Header.text = pages[currentPage].Header;
		uiPage.Description.text = pages[currentPage].Description;
		uiPage.Image.sprite = pages[currentPage].Image;
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
		if (currentPage == PageAmount)
			return;

		currentPage++;

		ShowPage();
	}

	public void PreviousPage()
	{
		if (currentPage == 0)
			return;

		currentPage--;

		ShowPage();
	}
}
