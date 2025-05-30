using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Colorer))]
public class Bomb : Entity
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _layerMask;

    private Colorer _colorer;
    private float _fadeTime;
    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    public event Action<Bomb> Exploused;

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

    private void Reset()
    {
        transform.position = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void Init()
    {
        _colorer.SetColor(Color.black);
        _fadeTime = UnityEngine.Random.Range(MinLifeTime, MaxLifeTime);
        _coroutine = StartCoroutine(OnFading(_fadeTime));
    }

    private void Explose()
    {
        Exploused?.Invoke(this);
        Collider[] entities = Physics.OverlapSphere(transform.position, _explosionRadius, _layerMask);

        foreach (Collider entity in entities)
        {
            if (entity.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))            
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _layerMask);
            
        }
    }

    private IEnumerator OnFading(float fadeTime)
    {
        float elapsedTime = 0f;
        Color color = _colorer.GetMaterial().color;
        float alpha = 1f;
        float transporent = 0f;

        while (elapsedTime <= fadeTime)
        {
            color.a = Mathf.Lerp(alpha, transporent, elapsedTime / fadeTime);
            _colorer.SetColor(color);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Explose();
    }
}