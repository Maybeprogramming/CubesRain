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
    private Rigidbody _rigidbody;
    private float _fadeTime;

    public event Action<Bomb> Exploused;

    [field: SerializeField] public float MinLifeTime { get; private set; }
    [field: SerializeField] public float MaxLifeTime { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorer = GetComponent<Colorer>();
    }

    private void OnEnable()
    {
        _colorer.SetColor(Color.black);
        _fadeTime = UnityEngine.Random.Range(MinLifeTime, MaxLifeTime);
        StartCoroutine(OnFading(_fadeTime));
    }

    private void Explose()
    {
        Collider[] entities = Physics.OverlapSphere(transform.position, _explosionRadius, _layerMask);

        foreach (Collider entity in entities) 
        {
            if (entity.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _layerMask);
                Exploused?.Invoke(this);
            }
        }
    }

    private IEnumerator OnFading(float fadeTime, float transporent = 0f)
    {
        float elapsedTime = 0f;
        Color color = _colorer.GetMaterial().color;
        float alpha = color.a;

        while (elapsedTime < fadeTime)
        {
            color.a = Mathf.Lerp(alpha, transporent, elapsedTime / fadeTime);
            _colorer.SetColor(color);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Explose();
    }
}