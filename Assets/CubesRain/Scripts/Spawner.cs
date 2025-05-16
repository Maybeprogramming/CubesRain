using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : Spawnable
{
    [SerializeField] private T _prefab;
    [SerializeField] private float _timeBeetwenSpawn = 1f;
    [SerializeField] private bool isWork = true;
    [SerializeField] private Transform _conteiner;

    private ObjectPool<T> _pool;
    private WaitForSeconds _waitTime;

    public int ActiveObjects => _pool.CountActive;
    public int CreatedOjbjects {  get; private set; }
    public int SpawnedObjects { get; private set; }

    private void Awake()
    {
        _pool = new ObjectPool<T>( createFunc: () => Create(),
                                   actionOnGet: (entity) => ActionOnGet(entity),
                                   actionOnRelease: (entity) => entity.gameObject.SetActive(false),
                                   actionOnDestroy: (entity) => Destroy(entity),
                                   collectionCheck: true,
                                   defaultCapacity: 10,
                                   maxSize: 20);                                  
    }

    private void Start()
    {
        StartCoroutine(OnSpawning());
    }

    private T Create()
    {        
        return Instantiate(_prefab, Vector3.zero, Quaternion.identity);
    }

    private void ActionOnGet(Spawnable spawnableObject)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 20f, Random.Range(-5f, 5f));
        spawnableObject.transform.position = spawnPosition;
        spawnableObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        spawnableObject.gameObject.SetActive(true);
    }

    private IEnumerator OnSpawning()
    {
        while (isWork)
        {
            Spawnable spawnableObjecxt = _pool.Get();
            yield return _waitTime = new WaitForSeconds(_timeBeetwenSpawn);
        }
    }
}