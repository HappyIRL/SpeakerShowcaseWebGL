using UnityEngine;

namespace Assets.Scripts
{
	public class CanvasHandler : MonoBehaviour
	{
		[SerializeField] private PageHandler pageHandler;

		public PageHandler PageHandler => pageHandler;
	}
}

