using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable() =>    
        _cubeSpawner.CubeLifeTimeEnded += OnSpawningBomb;        
    
    private void OnDisable() => 
        _cubeSpawner.CubeLifeTimeEnded -= OnSpawningBomb;

    private void OnSpawningBomb(Vector3 spawnPosition)
    {
        if (Pool.Get() is Bomb bomb)
            Init(bomb, spawnPosition);
    }

    private void Init(Bomb bomb, Vector3 spawnPosition)
    {
        bomb.Exploused += OnExplosed;
        bomb.transform.position = spawnPosition;
    }

    private void OnExplosed(Bomb bomb)
    {
        bomb.Exploused -= OnExplosed;
        Pool.Release(bomb);
    }
}