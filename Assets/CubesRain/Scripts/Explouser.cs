using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Explouser : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private LayerMask _layerMask;

    public void Explose()
    {
        Collider[] entities = Physics.OverlapSphere(transform.position, _explosionRadius, _layerMask);

        foreach (Collider entity in entities)
        {
            if (entity.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _layerMask);
        }
    }
}
