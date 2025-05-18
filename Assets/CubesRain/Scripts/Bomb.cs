using System;
using System.Collections;
using UnityEngine;


public class Bomb : SpawnableEntity
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _layerMask;
    
    private Rigidbody _rigidbody;
    float _fadeTime;

    public event Action<SpawnableEntity> Exploused;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Colorer = GetComponent<Colorer>();
    }

    private void OnEnable()
    {
        Colorer.SetColor(Color.black);
        _fadeTime = UnityEngine.Random.Range(MinLifeTime, MaxLifeTime);
        StartCoroutine(OnFading(_fadeTime));
    }

    private void Explose()
    {
        _rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _layerMask);
        Exploused?.Invoke(this);
    }

    private IEnumerator OnFading(float fadeTime, float transporent = 0f)
    {
        float elapsedTime = 0f;
        Color color = Colorer.GetMaterial().color;
        float alpha = color.a;

        while (elapsedTime < fadeTime)
        {
            color.a = Mathf.Lerp(alpha, transporent, elapsedTime / fadeTime);
            Colorer.SetColor(color);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Explose();
    }
}