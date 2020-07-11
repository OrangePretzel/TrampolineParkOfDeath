using UnityEngine;

namespace TPoD
{
	[CreateAssetMenu(menuName = "TPoD/Create " + nameof(ParkSettings))]
	public class ParkSettings : ScriptableObject
	{
		public GameObject TrampolinePrefab;
		public float TrampolineSize = 1.2f;
		public int ParkSize = 3;
	}
}
