using System;
using System.Collections;
using UnityEngine;

public class Cube : SpawnableEntity
{
    [SerializeField] private bool _isCollisionDetect;

    private WaitForSeconds _waitTime;

    public event Action<SpawnableEntity> Released;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.TryGetComponent<Platform>(out Platform platform);

        if (platform != null && _isCollisionDetect == false)
        {
            Colorer.SetRandomColor();
            _isCollisionDetect = true;
            StartCoroutine(LifeCountdown());
        }
    }

    public void Init()
    {
        _waitTime = new WaitForSeconds(UnityEngine.Random.Range(MinLifeTime, MaxLifeTime));
        _isCollisionDetect = false;
    }
    private IEnumerator LifeCountdown()
    {
        yield return _waitTime;
        Released?.Invoke(this);
    }
}