using Metamesa.MMUnity.Helpers;
using Metamesa.MMUnity.ObjectPooling;
using System;
using UnityEngine;

namespace TPoD
{
	public class WaspEnemyAnimator : MonoBehaviour
	{
		private const string ShootAnimationName = "Wasp - Shoot";

		[SerializeField] private Animator _animator = null;
		[SerializeField] private ParticleSystem _gunParticles = null;

		[Header("Model Parts")]
		[SerializeField] private MeshRenderer _headMeshRenderer = null;
		[SerializeField] private MeshRenderer _bodyMeshRenderer = null;
		[SerializeField] private MeshRenderer _leftJetpackMeshRenderer = null;
		[SerializeField] private MeshRenderer _rightJetpackMeshRenderer = null;
		[SerializeField] private MeshRenderer _gunMeshRenderer = null;
		[SerializeField] private MeshRenderer _armLeftMeshRenderer = null;
		[SerializeField] private MeshRenderer _armRightMeshRenderer = null;

		private void Update()
		{
			AimHeadAtTarget(GameManager.Instance.PlayerTransform.position);
			//if (Input.GetKeyDown(KeyCode.B))
			//	Shoot();
		}

		public void PerformActualShoot()
		{
			_gunParticles.Play();
		}

		#region Animation Functions

		public void Shoot()
		{
			_animator.Play(ShootAnimationName);
		}

		public void AimTowards(Vector3 targetPosition)
		{
			throw new NotImplementedException();
		}

		public void AimHeadAtTarget(Vector3 targetPosition)
		{
			var deltaPos = transform.position - targetPosition;
			float angle = -UnityVectorHelper.Vector2ToDegrees(new Vector2(deltaPos.x, deltaPos.z)) + 90;
			_headMeshRenderer.transform.localRotation = Quaternion.Euler(
				_headMeshRenderer.transform.localEulerAngles.x,
				_headMeshRenderer.transform.localEulerAngles.y,
				angle
			);
		}

		#endregion
	}
}
