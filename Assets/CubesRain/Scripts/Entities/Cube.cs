using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Colorer))]
public class Cube : Entity
{
    private bool _isCollisionDetect;
    private Colorer _colorer;
    private WaitForSeconds _waitTime;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    public event Action<Cube> Dead;

    [field: SerializeField] public float MinLifeTime { get; private set; }
    [field: SerializeField] public float MaxLifeTime { get; private set; }

    private void Awake()
    {
        _colorer = GetComponent<Colorer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable() =>    
        Init();    

    private void OnDisable() =>
        Reset();

    public void Reset()
    {
        ResetRigidbody();
        ResetTransform();

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void Init()
    {
        _waitTime = new WaitForSeconds(UnityEngine.Random.Range(MinLifeTime, MaxLifeTime));
        _isCollisionDetect = false;
        _colorer.SetColor(Color.white);
    }

    private void ResetTransform() =>     
        transform.rotation = Quaternion.identity;    

    private void ResetRigidbody()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.TryGetComponent<Platform>(out Platform platform);

        if (platform != null && _isCollisionDetect == false)
        {
            _colorer.SetRandomColor();
            _isCollisionDetect = true;
            _coroutine = StartCoroutine(LifeCountdown());
        }
    }



    private IEnumerator LifeCountdown()
    {
        yield return _waitTime;
        Dead?.Invoke(this);
    }
}