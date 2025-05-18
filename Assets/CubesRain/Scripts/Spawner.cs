using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : SpawnableEntity
{
    [SerializeField] private T _prefab;
    [SerializeField] private float _timeBeetwenSpawn = 1f;
    [SerializeField] private bool isWork = true;
    [SerializeField] private Transform _conteiner;

    private ObjectPool<SpawnableEntity> _pool;
    private WaitForSeconds _waitTime;

    public int ActiveEntities => _pool.CountActive;
    public int CreatedEntities => _pool.CountAll;
    public int SpawnedEntities { get; private set; }

    private void Awake()
    {
        _waitTime = new WaitForSeconds(_timeBeetwenSpawn);

        _pool = new ObjectPool<SpawnableEntity>(() => Create(),
                                  (entity) => ActionOnGet(entity),
                                  (entity) => entity.gameObject.SetActive(false),
                                  (entity) => Destroy(entity),
                                   true,
                                   10,
                                   20);
    }

    private void Start()
    {
        StartCoroutine(OnSpawning());
    }

    private T Create()
    {
        T instance = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        instance.transform.parent = _conteiner.transform;
        return instance;
    }

    private void ActionOnGet(SpawnableEntity entity)
    {
        entity.transform.position = GetRandomSpawnPosition();
        entity.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        //entity.Released += OnReleased;
        entity.gameObject.SetActive(true);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(UnityEngine.Random.Range(-5f, 5f), 20f, UnityEngine.Random.Range(-5f, 5f));
    }

    private void OnReleased(SpawnableEntity entity)
    {
        //entity.Released -= OnReleased;
        _pool.Release(entity);
    }

    private IEnumerator OnSpawning()
    {
        while (isWork)
        {
            SpawnableEntity entity = _pool.Get();
            yield return _waitTime;
        }
    }
}