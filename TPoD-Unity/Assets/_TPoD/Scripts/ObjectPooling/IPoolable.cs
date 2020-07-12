namespace Metamesa.MMUnity.ObjectPooling
{
	public interface IPoolable
	{
		void ActivatePoolable<T>(ObjectPool<T> objectPool) where T : IPoolable;
		void DeactivatePoolable();
	}
}
