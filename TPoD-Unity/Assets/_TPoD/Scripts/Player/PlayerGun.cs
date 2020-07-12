using UnityEngine;

namespace TPoD
{
	public class PlayerGun : MonoBehaviour
	{
		[SerializeField] private Animation _shootAnimation = null;
		[SerializeField] private ParticleSystem _shootParticles = null;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
				StartShootAnimation();
		}

		public void StartShootAnimation()
		{
			_shootAnimation.Play();
		}

		public void FireActualShot()
		{
			Debug.Log("Bang");
			_shootParticles.Play();
		}
	}
}
