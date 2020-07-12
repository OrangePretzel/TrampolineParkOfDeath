using UnityEngine;

namespace TPoD
{
	public class PlayerGun : MonoBehaviour
	{
		[Header("Visuals")]
		[SerializeField] private Animation _shootAnimation = null;
		[SerializeField] private ParticleSystem _shootParticles = null;

		[Header("Shooting Mechanic")]
		[SerializeField] private float _shootDistance = 1000f;
		[SerializeField] private float _gunDamage = 1;
		[SerializeField] private float _bulletRadius = 0.5f;
		[SerializeField] private bool _weakSpotOnly;
		[SerializeField] private Transform _gunShootStartLocation;

		private int _shootMask;
		private int _weakPointLayer;

		private void Awake()
		{
			
			_weakPointLayer = LayerMask.NameToLayer(TrampolineConstants.LayerConstants.ENEMY_WEAK_SPOT);
			int weakPointMask = 1 << _weakPointLayer;
			int waspColliderMask = 1 << LayerMask.NameToLayer(TrampolineConstants.LayerConstants.ENEMY_COLLIDER);
			_shootMask = weakPointMask | waspColliderMask;
		}

		private void Update()
		{
			if (Input.GetButtonDown(TrampolineConstants.InputConstants.SHOOT))
				StartShootAnimation();
		}

		public void StartShootAnimation()
		{
			_shootAnimation.Play();
		}

		public void FireActualShot()
		{
			_shootParticles.Play();

			RaycastHit raycastHit;
			Debug.DrawRay(_gunShootStartLocation.position, _gunShootStartLocation.forward * _shootDistance, Color.blue, 3f);

			if (Physics.SphereCast(_gunShootStartLocation.position, _bulletRadius, _gunShootStartLocation.forward, out raycastHit, _shootDistance, _shootMask, QueryTriggerInteraction.Collide) )
			{
				if (!_weakSpotOnly || raycastHit.collider.gameObject.layer == _weakPointLayer)
				{
					Health health = raycastHit.collider.gameObject.GetComponentInParent<Health>();
					health.DealDamage(_gunDamage);
				}
			}
			
		}
	}
}
