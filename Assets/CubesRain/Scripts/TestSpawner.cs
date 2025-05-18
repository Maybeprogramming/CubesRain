using System.Collections;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _rateTime;
    [SerializeField] private Vector3 _position;

    private void Start()
    {
        StartCoroutine(OnSpawning());
    }

    private IEnumerator OnSpawning()
    {
        while (true)
        {
            _position = new Vector3(UnityEngine.Random.Range(-5f, 5f), 20f, UnityEngine.Random.Range(-5f, 5f));
            Instantiate(_prefab, _position, Quaternion.identity);
            yield return new WaitForSeconds(_rateTime);
        }
    }
}