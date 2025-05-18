using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Colorer : MonoBehaviour
{
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void SetRandomColor()
    {
        _renderer.material.color = GetRandomColor();
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    private Color GetRandomColor(float alpha = 1f)
    {
        float _red = Random.value;
        float _green = Random.value;
        float _blue = Random.value;
        float _alpha = alpha;

        return new Color(_red, _green, _blue, _alpha);
    }

    public Material GetMaterial() 
    { 
        return _renderer.material;
    }
}