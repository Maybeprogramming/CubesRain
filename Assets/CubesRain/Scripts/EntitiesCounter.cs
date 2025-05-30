using TMPro;
using UnityEngine;

public class EntitiesCounter<T> : MonoBehaviour, where T : Spawner<Entity>
{
    [SerializeField] private T _spawner;
    [SerializeField] private TextMeshProUGUI _spawnedEntities;
    [SerializeField] private TextMeshProUGUI _createdEntities;
    [SerializeField] private TextMeshProUGUI _activeEntities;

    private void OnEnable() =>
        _spawner.Informing += OnCounterRefreshing;

    private void OnDisable() =>
        _spawner.Informing -= OnCounterRefreshing;

    private void OnCounterRefreshing(int spawnedEntities, int createdEntities, int activeEntities)
    {
        _spawnedEntities.text = GetText("Spawned:", spawnedEntities);
        _createdEntities.text = GetText("Created:", createdEntities);
        _activeEntities.text = GetText("Active:", activeEntities);
    }

    private string GetText(string text, int count = 0) =>
        $"{text} {count}";
}