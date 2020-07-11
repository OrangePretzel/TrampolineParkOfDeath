﻿using UnityEngine;

namespace TPoD
{
	public class ParkBuilder : MonoBehaviour
	{
		[SerializeField] private ParkSettings _parkSettings = null;
		[SerializeField] private Transform _parkContainer = null;

		public void BuildPark()
		{
			// Reference: https://www.redblobgames.com/grids/hexagons/

			int parkSize = _parkSettings.ParkSize;
			float parkSizeF = _parkSettings.ParkSizeF;

			float hexSize = _parkSettings.TrampolineSize;
			float hexWidth = Mathf.Sqrt(3) * hexSize;
			float hexHeight = 2f * hexSize;

			for (int i = -parkSize; i <= parkSize; ++i)
				for (int k = -parkSize; k <= parkSize; ++k)
				{
					float x = i + (k & 1) / 2f;
					float y = k;

					if (x * x + y * y > parkSizeF * parkSizeF)
						continue;

					var tramp = SpawnTrampoline();
					tramp.transform.localPosition = new Vector3(
						x * hexWidth,
						0,
						y * hexHeight * 3f / 4f);
#if UNITY_EDITOR
					tramp.name = $"Trampoline ({i}, {k})";
#endif
				}
		}

		private GameObject SpawnTrampoline()
		{
			var trampObj = Instantiate(_parkSettings.TrampolinePrefab, _parkContainer);
			return trampObj;
		}
	}
}
