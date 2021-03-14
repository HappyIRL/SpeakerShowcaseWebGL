using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerEventInstallerMono : MonoBehaviour
	{
		[Zenject.Inject] private PlayerInputHandler _playerInputHandler;

		protected virtual void OnEnable()
		{
			_playerInputHandler.TouchInput += OnTouchInput;
		}

		protected virtual void OnDisable()
		{
			_playerInputHandler.TouchInput -= OnTouchInput;
		}

		protected virtual void OnTouchInput(Vector2 position)
		{
		}

	}
}
