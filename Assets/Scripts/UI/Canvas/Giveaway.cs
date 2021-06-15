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
		private SpeakerComponents lastInspectedComponent = SpeakerComponents.None;
		private SpeakerComponents lastlastInspectedComponent = SpeakerComponents.None;
		private int UniquePagesToggled = 0;
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

		private void UniquePageCheck(SpeakerComponents currentInspectedComponent)
		{
			if (wonGiveaway == true)
				return;

			if (currentInspectedComponent != lastInspectedComponent && currentInspectedComponent != lastlastInspectedComponent)
			{
				lastlastInspectedComponent = lastInspectedComponent;
				lastInspectedComponent = currentInspectedComponent;
				UniquePagesToggled++;
			}

			if (UniquePagesToggled == 3)
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