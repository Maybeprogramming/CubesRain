using TMPro;
using UnityEngine;

public class EntitiesCounter : MonoBehaviour
{
    [SerializeField] private Iinformer _spawner;
    [SerializeField] private TextMeshProUGUI _spawnedEntities;
    [SerializeField] private TextMeshProUGUI _createdEntities;
    [SerializeField] private TextMeshProUGUI _activeEntities;

    private void OnDisable() =>
        _spawner.Informing -= OnCounterRefreshing;

    public void Init(Iinformer spawner)
    {
        _spawner = spawner;
        _spawner.Informing += OnCounterRefreshing;
    }

    private void OnCounterRefreshing(int spawnedEntities, int createdEntities, int activeEntities)
    {
        _spawnedEntities.text = GetText("Spawned:", spawnedEntities);
        _createdEntities.text = GetText("Created:", createdEntities);
        _activeEntities.text = GetText("Active:", activeEntities);
    }

    private string GetText(string text, int count = 0) =>
        $"{text} {count}";
}