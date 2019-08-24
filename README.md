# Spawners
Spawners and Object Pooling for Unity.

## Object Pooling
Object pooling is a technique, which reduces heap memory allocations, by recycling objects. Pool is made to be lightweight and efficient, it's also responsible for:
- Tracking pooled objects
- Retrieving and returning them
- Resetting their state
- Expanding when necessarry

### How does it work?
When object is retrieved:
1. If Pool is empty, then it's expanded
2. Pooled object is selected and marked as used
3. It's state is being restored to default

When object is returned
1. Returned object is disabled
2. The object is marked as pooled

Pooled object has to be a prefab with component that inherits from Poolable\<T\>. It's a generic class used by pools, which holds a reference to the actual object.

#### Pool Preparer and Multi Pool Preparer
This classes are responsible for creating and initializing instances of the Pool class. Every needed parameters are set up more or less directly through the Unity Inspector.

### Scripting API
- Retrieve an object
```csharp
Poolable<T> poolable = pool.Retrieve();
```
- Retrieve multiple objects
```csharp
Poolable<T>[] poolables = new Poolable<T>[10];
pool.RetrieveMany(poolables);
pool.RetrieveMany(poolables, count: 5);
pool.RetrieveMany(poolables, 11); // This will throw an AssertionException, since the 'count' parameter is greater than 'poolables' length
pool.RetrieveMany(poolables, 0); // This will throw too.
```
- Return an object
```csharp
pool.Return(poolable); // 'poolable' cannot be null
```

### Multi Pool
Multi Pool is a container for multiple Pools and other Multi Pools. It's interface is extended by following methods, which allow retrieving object from selected Pool:
```csharp
int poolIndex = 0;
Poolable<T> poolable = pool.RetrieveFrom(poolIndex);
pool.RetrieveManyFrom(poolables, poolIndex);
pool.RetrieveManyFrom(poolables, poolIndex, count: 5);
```
### Example
It's time for some examples. Let's set up a simple Pool for Transform components.

#### 1. Create concrete class that inherits from Poolable\<Transform\> in this case.
```csharp
class TransformPoolable : Poolable<Transform> { }
```
Yes, that's it. This is necessary, since Unity can't serialize generic classes. Following steps are going to be similar.

#### 2. Create sublass of the PoolPreparer\<Transform\> 
```csharp
class TransformPoolPreparer : PoolPreparer<Transform>
{
    [SerializeField]
    private TransformPoolable prefab;
    protected override Poolable<Transform> Prefab => prefab;
}
```
This allows to select a prefab, which is going to be pooled. PoolPreparer class can be further customized with two optional overrides.

#### 2a. (Optional) Manage pooled object's state when it's retrieved and returned
By default, pooled object's GameObject is activated when retrieved, and deactivated when returned. This behaviour can be extended or completly modified. To change it, provide an implementation of IPoolableStateRestorer\<Transform\> interface.
```csharp
class TransformPoolableStateRestorer : IPoolableStateRestorer<Transform>
{
    public void OnRetrieve(Poolable<Transform> poolable)
    {
        poolable.gameObject.SetActive(true);
        poolable.Target.eulerAngles = new Vector3(30, 0, 90);
    }

    public void OnReturn(Poolable<Transform> poolable)
    {
        poolable.gameObject.SetActive(false);
    }
}
```
Then, use it in Pool Preparer in the following way:
```csharp
protected override IPoolableStateRestorer<Transform> StateRestorer => restorer;
```

#### 2b. (Optional) Change the way objects are being created
Pooled objects are instantiated on the scene when:
- Pool instance is created
- Pool is being expanded

By default, pooled objects are instantiated as Pool Preparer's children. Note, that for every created object, OnRetrieve method of provided IPoolableStateRestorer implementation is invoked.
To extend this behaviour, either provide an implementation of IPoolableFactory\<Transform\> interface or extend PoolableFactory\<Transform\> class. The latter is generally more preferable.
An example use case is to provide dependencies, that can't be set up directly in the object's prefab.
```csharp
class TransformFactory : PoolableFactory<Transform>
{
    private Vector3 scale;

    public TransformFactory(Poolable<Transform> prefab, Transform parent, IPoolableStateRestorer<Transform> stateResotrer, Vector3 scale) : base(prefab, parent, stateResotrer)
    {
        this.scale = scale;
    }

    public override void OnCreated(Poolable<Transform> created)
    {
        created.Target.localScale = scale;
    }
}
```
To use it, override PoolableFactory property:
```csharp
protected override IPoolableFactory<Transform> PoolableFactory => new TransformFactory(prefab, transform, StateRestorer, scale);
```


Finally, Pool Preparer should look like this:
```csharp
class TransformPoolPreparer : PoolPreparer<Transform>
{
    [SerializeField]
    private TransformPoolable prefab;
    protected override Poolable<Transform> Prefab => prefab;

    protected override IPoolableStateRestorer<Transform> StateRestorer => new TransformPoolableStateRestorer();

    [SerializeField]
    private Vector3 scale;
    protected override IPoolableFactory<Transform> PoolableFactory => new TransformFactory(prefab, transform, StateRestorer, scale);
}
```

## Spawners
Coming soon...
### Example
Coming soon...

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
