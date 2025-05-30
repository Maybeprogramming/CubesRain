using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : Entity
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _EntitiesConteiner;
    [SerializeField] private int _poolDefaultCapacity;
    [SerializeField] private int _poolMaxCapacity;

    private protected ObjectPool<Entity> Pool;

    public event Action<int, int, int> Informing;

    public int SpawnedEntities { get; private set; }
    public int CreatedEntities => Pool.CountAll;
    public int ActiveEntities => Pool.CountActive;

    private void Awake()
    {
        SpawnedEntities = 0;

        Pool = new ObjectPool<Entity>(() => Create(),
                                  (entity) => ActionOnGet(entity),
                                  (entity) => entity.gameObject.SetActive(false),
                                  (entity) => Destroy(entity),
                                   true,
                                   _poolDefaultCapacity,
                                   _poolMaxCapacity);

        DoInforming(SpawnedEntities, CreatedEntities, ActiveEntities);
    }

    private T Create()
    {
        T instance = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        instance.transform.parent = _EntitiesConteiner.transform;
        DoInforming(SpawnedEntities, CreatedEntities, ActiveEntities);
        return instance;
    }

    private void ActionOnGet(Entity entity)
    {
        entity.gameObject.SetActive(true);
        SpawnedEntities++;
        DoInforming(SpawnedEntities, CreatedEntities, ActiveEntities);
    }

    private void OnReleased(Entity entity)
    {
        Pool.Release(entity);
    }

    private void DoInforming(int spawenedEntities, int createdEntities, int activeEntities)
    {
        Informing?.Invoke(spawenedEntities, createdEntities, activeEntities);
    }
}