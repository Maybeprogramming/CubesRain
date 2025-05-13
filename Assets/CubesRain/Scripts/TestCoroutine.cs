using System.Collections;
using UnityEngine;

public class TestCoroutine : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown(int delay = 5)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        bool isCoroutine = true;

        if (isCoroutine)
        {
            Debug.Log("Unity");

            yield return wait;
        }
    }

    [ContextMenu(nameof(Print))]
    private void Print()
    {
        GameObject gameObject = FindObjectOfType<GameObject>();
        Debug.Log("Log");
    }
}