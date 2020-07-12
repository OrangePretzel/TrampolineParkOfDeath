using UnityEngine;

namespace TPoD
{
	[CreateAssetMenu(menuName = "TPoD/Create " + nameof(ParkSettings))]
	public class ParkSettings : ScriptableObject
	{
		[Header("Park Visual Settings")]
		public GameObject TrampolinePrefab;
		public float TrampolineSize = 1.2f;
		public int ParkSize = 3;
		public float ParkSizeF = 3.5f;

		[Header("Boosert Randomization Settings")]
		public float ProbabilityOfDirectional;		
	}
}
