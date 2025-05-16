using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Colorer : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = Color.white;
        _renderer.material.color = _defaultColor;
    }

    public void SetRandomColor()
    {
        _renderer.material.color = GetRandomColor();
    }

    public void SetDefaultColor()
    {
        _renderer.material.color = _defaultColor;
    }

    private Color GetRandomColor(float alpha = 1f)
    {
        float _red = Random.value;
        float _green = Random.value;
        float _blue = Random.value;
        float _alpha = alpha;

        return new Color(_red, _green, _blue, _alpha);
    }

    //public void Fade()
    //{
    //    StartCoroutine(OnFading(_fadeTime));
    //}

    //private IEnumerator OnFading(float fadeTime, float transporent = 0f)
    //{
    //    float elapsedTime = 0f;
    //    Color color = _renderer.material.color;
    //    float alpha = color.a;

    //    while (elapsedTime < fadeTime)
    //    {
    //        color.a = Mathf.Lerp(alpha, transporent, elapsedTime/ fadeTime);
    //        _renderer.material.color = color;
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //}
}