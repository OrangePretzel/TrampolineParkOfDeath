using Metamesa.MMUnity.Helpers;
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

		[SerializeField] private bool _isDyingWaspType = false;
		[SerializeField] private float _dyingSpinSpeed = 300f;

		private void Update()
		{
			AimHeadAtTarget(GameManager.Instance.PlayerTransform.position);
			//if (Input.GetKeyDown(KeyCode.B))
			//	Shoot();

			if (_isDyingWaspType)
			{
				transform.localRotation = Quaternion.Euler(
					transform.localEulerAngles.x,
					Time.time * _dyingSpinSpeed,
					transform.localEulerAngles.z
					);
			}
		}

		public void PerformActualShoot()
		{
			_gunParticles.Play();
		}

		public void HandleDamageTaken(float damageAmount, float newHealth)
		{
			// TODO Alosh (Alex + Josh)
			if (newHealth <= 0)
				PerformDeath();
		}

		#region Animation Functions

		public void TriggerShootAnimation()
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

		public void PerformDeath()
		{
			if (!_isDyingWaspType)
			{
				// Spawn dying type
				var dyingWasp = GameManager.Instance.DyingWaspPool.GetObjectFromPool();
				dyingWasp.transform.position = transform.position;

				// Destroy self
			}
			else
			{
				// Stay gold pony boy
			}
		}

		#endregion
	}
}
