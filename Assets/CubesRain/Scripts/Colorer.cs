using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Colorer : MonoBehaviour
{
    private MeshRenderer _renderer;

    private void Awake() =>
        _renderer = GetComponent<MeshRenderer>();

    public void SetRandomColor() =>
        _renderer.material.color = GetRandomColor();

    public void SetColor(Color color) =>
        _renderer.material.color = color;

    public Material GetMaterial() =>
        _renderer.material;

    private Color GetRandomColor() =>
        Random.ColorHSV();
}