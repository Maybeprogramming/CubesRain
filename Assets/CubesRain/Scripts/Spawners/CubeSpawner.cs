using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _timeBeetwenSpawn = 1f;
    [SerializeField] private bool _isWork = true;
    [SerializeField] private float _minXAxis;
    [SerializeField] private float _maxXAxis;
    [SerializeField] private float _minZAxis;
    [SerializeField] private float _maxZAxis;
    [SerializeField] private float _height;

    private WaitForSeconds _waitTime;

    public event Action<Vector3> CubeLifeTimeEnded;

    private void Start()
    {
        _waitTime = new WaitForSeconds(_timeBeetwenSpawn);
        StartCoroutine(Spawning());
    }

    private void Init(Cube cube)
    {
        cube.Reset();
        cube.Dead += OnDead;
        cube.transform.position = GetRandomSpawnPosition();
    }

    private void OnDead(Cube cube)
    {
        cube.Dead -= OnDead;
        CubeLifeTimeEnded?.Invoke(cube.transform.position);
        Pool.Release(cube);
    }

    private Vector3 GetRandomSpawnPosition() =>    
        new Vector3(UnityEngine.Random.Range(_minXAxis, _maxXAxis), _height, UnityEngine.Random.Range(_minZAxis, _maxZAxis));

    private IEnumerator Spawning()
    {
        while (_isWork)
        {
            if (Pool.Get() is Cube cube)            
                Init(cube);            

            yield return _waitTime;
        }
    }
}