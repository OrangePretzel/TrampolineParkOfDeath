using UnityEngine;

namespace TPoD
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private ParkBuilder _parkBuilder = null;

		private void OnEnable()
		{
			SetupPark();
		}

		private void SetupPark()
		{
			if (_parkBuilder == null)
				return; // Nothing to setup

			_parkBuilder.BuildPark();
		}
	}
}
