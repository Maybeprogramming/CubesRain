using UnityEngine;

public class BombCounter : MonoBehaviour
{
    [SerializeField] private EntitiesCounter _bombCounter;
    [SerializeField] private BombSpawner _bombSpawner;

    private void Awake() =>    
        _bombCounter.Init(_bombSpawner);    
}