using UnityEngine;

[RequireComponent(typeof(Colorer), typeof(Rigidbody))]
public class SpawnableEntity : MonoBehaviour
{
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLifeTime;

    private protected Colorer Colorer;

    public float MinLifeTime => _minLifeTime;
    public float MaxLifeTime => _maxLifeTime; 

    private void Awake()
    {
        Colorer = GetComponent<Colorer>();
    }
}