using UnityEngine;

namespace TPoD.UI
{
	public class HUD : MonoBehaviour
	{
		[SerializeField] private HUDTimer _hudTimer = null;

		private void Update()
		{
			var gameState = GameManager.GameState;
			if (gameState == null)
				return;

			if (!gameState.IsPlaying)
				return;

			_hudTimer.UpdateTimerText(gameState.Time);
		}
	}
}
