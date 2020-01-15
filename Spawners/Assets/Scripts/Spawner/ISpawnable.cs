namespace ObjectManagement.Spawners
{
    public interface ISpawnable
    {
        void OnSpawned();
        void OnDespawned();
    }
}