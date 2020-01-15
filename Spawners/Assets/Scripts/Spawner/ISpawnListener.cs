namespace ObjectManagement.Spawners
{
    public interface ISpawnListener<in T>
    {
        void OnSpawned(T spawned);
        void OnDespawned(T despawned);
    }
}