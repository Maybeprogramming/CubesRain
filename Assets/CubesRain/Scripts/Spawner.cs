using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _timeBeetwenSpawn = 1f;
    [SerializeField] private bool isWork = true;

    private WaitForSeconds _waitTime;

    private void Start()
    {
        StartCoroutine(OnSpawning());
    }

    private void Spawn()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 20f, Random.Range(-5f, 5f));
        var cube = Instantiate(_prefab, spawnPosition, Quaternion.identity);
    }

    private IEnumerator OnSpawning()
    {
        while (isWork)
        {
            Spawn();
            yield return _waitTime = new WaitForSeconds(_timeBeetwenSpawn);
        }
    }
}