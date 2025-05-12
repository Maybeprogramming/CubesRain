using UnityEngine;

public class TestColorer : MonoBehaviour
{
    [SerializeField] private Colorer _colorer;
    [SerializeField] private bool isSetColor = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isSetColor)
        {
            _colorer.SetRandomColor();
            _colorer.Fade();
            isSetColor = true;
        }
    }
}