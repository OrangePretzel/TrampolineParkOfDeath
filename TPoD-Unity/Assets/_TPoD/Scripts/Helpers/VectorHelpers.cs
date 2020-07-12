using UnityEngine;

namespace Metamesa.MMUnity.Helpers
{
	public static class UnityVectorExtensions
	{
		public static Vector2 SetX(this Vector2 vec, float x)
		{
			vec.x = x;
			return vec;
		}

		public static Vector2 SetY(this Vector2 vec, float y)
		{
			vec.y = y;
			return vec;
		}

		public static Vector2Int SetX(this Vector2Int vec, int x)
		{
			vec.x = x;
			return vec;
		}

		public static Vector2Int SetY(this Vector2Int vec, int y)
		{
			vec.y = y;
			return vec;
		}

		public static Vector3 SetX(this Vector3 vec, float x)
		{
			vec.x = x;
			return vec;
		}

		public static Vector3 SetY(this Vector3 vec, float y)
		{
			vec.y = y;
			return vec;
		}

		public static Vector3 SetZ(this Vector3 vec, float z)
		{
			vec.z = z;
			return vec;
		}

		public static Vector3Int SetX(this Vector3Int vec, int x)
		{
			vec.x = x;
			return vec;
		}

		public static Vector3Int SetY(this Vector3Int vec, int y)
		{
			vec.y = y;
			return vec;
		}

		public static Vector3Int SetZ(this Vector3Int vec, int z)
		{
			vec.z = z;
			return vec;
		}
	}

	public static class UnityRectExtensions
	{
		public static Vector2 ClampInsideRect(this Rect rect, Vector2 vec)
		{
			Vector2 clampedVec;
			clampedVec.x = Mathf.Clamp(vec.x, rect.xMin, rect.xMax);
			clampedVec.y = Mathf.Clamp(vec.y, rect.yMin, rect.yMax);
			return clampedVec;
		}

		public static Rect ScaleRect(this Rect rect, float xScale, float yScale)
		{
			return new Rect(rect.x * xScale, rect.y * yScale, rect.width * xScale, rect.height * yScale);
		}
	}

	public static class NumericsVectorExtensions
	{
		public static Vector2 ToUnityVector2(this System.Numerics.Vector2 v)
			=> new Vector2(v.X, v.Y);
		public static Vector3 ToUnityVector3(this System.Numerics.Vector2 v)
			=> new Vector3(v.X, v.Y, 0);

		public static Vector2 ToUnityVector2(this System.Numerics.Vector3 v)
			=> new Vector2(v.X, v.Y);
		public static Vector3 ToUnityVector3(this System.Numerics.Vector3 v)
			=> new Vector3(v.X, v.Y, v.Z);
	}

	public static class UnityVectorHelper
	{
		public static Vector2 MultiplyVectorComponents(Vector2 v1, Vector2 v2)
			=> new Vector2(v1.x * v2.x, v1.y * v2.y);
		public static Vector3 MultiplyVectorComponents(Vector3 v1, Vector3 v2)
			=> new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);

		public static Vector2 DivideVectorComponents(Vector2 v1, Vector2 v2)
			=> new Vector2(v1.x / v2.x, v1.y / v2.y);
		public static Vector3 DivideVectorComponents(Vector3 v1, Vector3 v2)
			=> new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);

		public static Vector2 DegreesToVector2(float angleInDegrees) => new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
		public static Vector2 RadiansToVector2(float angleInRadians) => new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

		public static float Vector2ToDegrees(Vector2 vector, bool normalizeUnityCoords = true) => Mathf.Rad2Deg * Vector2ToRadians(vector, normalizeUnityCoords);
		public static float Vector2ToRadians(Vector2 vector, bool normalizeUnityCoords = true)
		{
			var rad = Mathf.Atan2(vector.y, vector.x);
			if (normalizeUnityCoords && rad < 0) rad += Mathf.PI * 2; // Normalize Unity returning negative angles
			return rad;
		}

		public static Vector3 DegreesToVector3(float angleInDegrees) => new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
		public static Vector3 RadiansToVector3(float angleInRadians) => new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0);

		public static Vector2 RotateVector2ByDegrees(Vector2 vector, float degrees) => RotateVector2ByRadians(vector, degrees * Mathf.Deg2Rad);
		public static Vector2 RotateVector2ByRadians(Vector2 vector, float radians)
		{
			float sinAngle = Mathf.Sin(radians);
			float cosAngle = Mathf.Cos(radians);

			float x = vector.x;
			float y = vector.y;
			vector.x = (cosAngle * x) - (sinAngle * y);
			vector.y = (sinAngle * x) + (cosAngle * y);

			return vector;
		}
	}
}
