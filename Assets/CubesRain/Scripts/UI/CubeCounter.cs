using UnityEngine;

public class CubeCounter : MonoBehaviour
{
    [SerializeField] private EntitiesCounter _cubeCounter;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void Awake() =>    
        _cubeCounter.Init(_cubeSpawner);    
}