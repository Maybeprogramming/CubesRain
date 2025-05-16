using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Colorer))]
public class Cube : Spawnable
{
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;
    [SerializeField] private bool _isCollisionDetect;
    [SerializeField] private Colorer _colorer;
    
    private WaitForSeconds _waitTime;
    private Coroutine _lifeTimer;

    private void Awake()
    {
        _colorer = GetComponent<Colorer>();
        Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.TryGetComponent<Platform>(out Platform platform);

        if (platform != null && _isCollisionDetect == false)
        {
            _colorer.SetRandomColor();
            _isCollisionDetect = true;
            _lifeTimer = StartCoroutine(LifeCountdown());
        }
    }

    private void OnDisable()
    {
        if (_lifeTimer != null)
            StopCoroutine(_lifeTimer);
    }

    public void Init()
    {
        _waitTime = new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime));
        _colorer.SetDefaultColor();
        _isCollisionDetect = false;
    }

    private IEnumerator LifeCountdown()
    {
        yield return _waitTime;
        gameObject.SetActive(false);
    }
}