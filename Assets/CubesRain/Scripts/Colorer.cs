using System.Collections;
using UnityEngine;

public class Colorer : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _fadeTime;
    [SerializeField] private float _red;
    [SerializeField] private float _green;
    [SerializeField] private float _blue;
    [SerializeField] private float _alpha;

    public void Fade()
    {
        StartCoroutine(OnFading(_fadeTime));
    }

    public void SetRandomColor()
    {
        _renderer.material.color = GetRandomColor();
    }

    private Color GetRandomColor(float alpha = 1f)
    {
        _red = Random.value;
        _green = Random.value;
        _blue = Random.value;
        _alpha = alpha;

        return new Color(_red, _green, _blue, _alpha);
    }

    private IEnumerator OnFading(float fadeTime, float transporent = 0f)
    {
        float elapsedTime = 0f;
        Color color = _renderer.material.color;
        float alpha = color.a;

        while (elapsedTime < fadeTime)
        {
            color.a = Mathf.Lerp(alpha, transporent, elapsedTime/ fadeTime);
            _renderer.material.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}