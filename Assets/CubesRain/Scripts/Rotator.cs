using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";

    [SerializeField] private Transform _rotateObject;
    [SerializeField] private Transform _point;
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _angleSpeed;

    private float axisValueInput;

    private void Update()
    {
        axisValueInput = Input.GetAxis(HorizontalAxis);
        Rotate(axisValueInput);
    }

    private void Rotate(float axisValue) =>
       _rotateObject.RotateAround(_point.position, _axis, axisValue * _angleSpeed * Time.deltaTime);
}