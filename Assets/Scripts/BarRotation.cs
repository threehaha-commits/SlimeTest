using UnityEngine;

public class BarRotation : MonoBehaviour
{
    private Transform _transform;
    private Transform _camera;
    
    private void Start()
    {
        _camera = Camera.main.transform;
        _transform = transform;
    }

    private void Update()
    {
        var dir = _transform.position - _camera.position;
        _transform.rotation = Quaternion.LookRotation(dir, Vector3.right);
    }
}