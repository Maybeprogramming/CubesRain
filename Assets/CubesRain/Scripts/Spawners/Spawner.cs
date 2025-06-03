using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour, IInformer where T : Entity
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _EntitiesConteiner;

    private protected ObjectPool<T> Pool;

    public event Action<int, int, int> Informing;

    [field: SerializeField] public int PoolDefaultCapacity { get; private set; }
    [field: SerializeField] public int PoolMaxCapacity { get; private set; }

    public int SpawnedEntities { get; private set; }
    public int CreatedEntities => Pool.CountAll;
    public int ActiveEntities => Pool.CountActive;

    private void Awake()
    {
        SpawnedEntities = 0;
        PoolInit();
    }

    private void Start() =>
        DoInforming(SpawnedEntities, CreatedEntities, ActiveEntities);

    private protected virtual void PoolInit() { }

    private protected T Create()
    {
        T instance = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        instance.transform.parent = _EntitiesConteiner.transform;
        DoInforming(SpawnedEntities, CreatedEntities, ActiveEntities);
        return instance;
    }

    private protected void GetEntity(Entity entity)
    {
        entity.gameObject.SetActive(true);
        SpawnedEntities++;
        DoInforming(SpawnedEntities, CreatedEntities, ActiveEntities);
    }

    private void DoInforming(int spawenedEntities, int createdEntities, int activeEntities) =>
        Informing?.Invoke(spawenedEntities, createdEntities, activeEntities);
}