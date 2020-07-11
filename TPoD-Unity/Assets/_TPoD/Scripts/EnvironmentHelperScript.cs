using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace TPoD
{
	[ExecuteInEditMode]
	public class EnvironmentHelperScript : MonoBehaviour
	{
		[SerializeField] private GameObject _trampolinePrefab = null;
		[SerializeField] private Transform _environmentContainer = null;

		[Button("Empty Container")]
		private void EmptyContainer()
		{
			var childCount = _environmentContainer.childCount;
			for (int i = childCount - 1; i >= 0; --i)
			{
				var child = _environmentContainer.GetChild(i);
				DestroyImmediate(child.gameObject);
			}
		}

		[SerializeField] float _hexagonSize = 2.0f;
		[Button("Generate Hexagons")]
		private void GenerateTrampolineHexagon()
		{
			// Reference: https://www.redblobgames.com/grids/hexagons/
			int parkSize = 3;

			float hexWidth = Mathf.Sqrt(3) * _hexagonSize;
			float hexHeight = 2f * _hexagonSize;

			for (int i = -parkSize; i <= parkSize; ++i)
				for (int k = -parkSize; k <= parkSize; ++k)
				{
					float x = i + (k & 1) / 2f;
					float y = k;

					var trampObj = SpawnTrampoline(new Vector3(
						x * hexWidth,
						0,
						y * hexHeight * 3f / 4f
					), Quaternion.identity);
				}
		}

		[SerializeField] private int _gridSize = 10;
		[SerializeField] private int _gridScale = 2;
		[Button("Generate Grid")]
		private void GenerateTrampolineGrid()
		{
			for (int x = 0; x < _gridSize; ++x)
				for (int y = 0; y < _gridSize; ++y)
				{
					SpawnTrampoline(new Vector3(x * _gridScale, 0, y * _gridScale), Quaternion.identity);
				}
		}

		private GameObject SpawnTrampoline(Vector3 position, Quaternion rotation)
		{
			var x = PrefabUtility.InstantiatePrefab(_trampolinePrefab);
			var x2 = x as GameObject;
			var trampObj = Instantiate(_trampolinePrefab, position, rotation, _environmentContainer);
			return trampObj;
		}
	}
}
