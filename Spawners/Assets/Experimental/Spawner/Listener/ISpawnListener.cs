namespace Experimental.Spawners.Listener
{
    public interface ISpawnListener<in T>
    {
        void OnSpawned(T spawned);
        void OnDespawned(T despawned);
    }
}