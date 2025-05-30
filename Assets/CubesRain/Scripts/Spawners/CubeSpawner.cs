using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _timeBeetwenSpawn = 1f;
    [SerializeField] private bool isWork = true;

    private WaitForSeconds _waitTime;

    public event Action<Vector3> CubeLifeTimeEnded;

    private void Start()
    {
        _waitTime = new WaitForSeconds(_timeBeetwenSpawn);
        StartCoroutine(OnSpawning());
    }

    private void Init(Cube cube)
    {
        cube.Dead += OnDead;
        cube.transform.position = GetRandomSpawnPosition();
        cube.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }

    private void OnDead(Cube cube)
    {
        cube.Dead -= OnDead;
        CubeLifeTimeEnded?.Invoke(cube.transform.position);
        Pool.Release(cube);
    }

    private Vector3 GetRandomSpawnPosition() =>    
        new Vector3(UnityEngine.Random.Range(-5f, 5f), 20f, UnityEngine.Random.Range(-5f, 5f));

    private IEnumerator OnSpawning()
    {
        while (isWork)
        {
            if (Pool.Get() is Cube cube)            
                Init(cube);            

            yield return _waitTime;
        }
    }
}