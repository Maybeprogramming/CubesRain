using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable() =>    
        _cubeSpawner.CubeDead += OnSpawningBomb;        
    
    private void OnDisable() => 
        _cubeSpawner.CubeDead -= OnSpawningBomb;

    private void OnSpawningBomb(Vector3 vector)
    {
        if (Pool.Get() is Bomb bomb)
            Init(bomb, vector);
    }

    private void Init(Bomb bomb, Vector3 vector)
    {
        bomb.transform.position = vector;
        bomb.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        bomb.Exploused += OnExplosed;
    }

    private void OnExplosed(Bomb bomb)
    {
        bomb.Exploused -= OnExplosed;
        Pool.Release(bomb);
    }
}