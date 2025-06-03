using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable() =>
        _cubeSpawner.CubeLifeTimeEnded += OnSpawningBomb;

    private void OnDisable() =>
        _cubeSpawner.CubeLifeTimeEnded -= OnSpawningBomb;

    private protected override void PoolInit()
    {
        Pool = new ObjectPool<Bomb>(() => Create(),
                                    (bomb) => PutEntity(bomb),
                                    (bomb) => bomb.gameObject.SetActive(false),
                                    (bomb) => Destroy(bomb),
                                    true,
                                    PoolDefaultCapacity,
                                    PoolMaxCapacity);
    }

    private void Init(Bomb bomb, Vector3 spawnPosition)
    {
        bomb.Exploused += OnExplosed;
        bomb.transform.position = spawnPosition;
    }

    private void OnSpawningBomb(Vector3 spawnPosition) =>    
        Init(Pool.Get(), spawnPosition);    

    private void OnExplosed(Bomb bomb)
    {
        bomb.Exploused -= OnExplosed;
        Pool.Release(bomb);
    }
}