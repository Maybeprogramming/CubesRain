using TMPro;
using UnityEngine;

public class ObjectsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _spawnedCube;
    [SerializeField] private TextMeshProUGUI _createdCube;
    [SerializeField] private TextMeshProUGUI _activeCube;
    [SerializeField] private TextMeshProUGUI _spawnedBomb;
    [SerializeField] private TextMeshProUGUI _createdBomb;
    [SerializeField] private TextMeshProUGUI _activeBomb;

    private void Awake()
    {
        _spawnedCube.text = "0";
        _createdCube.text = "0";
        _activeCube.text = "0";

        _spawnedBomb.text = "0";
        _activeBomb.text = "0";
        _createdBomb.text = "0";
    }
}