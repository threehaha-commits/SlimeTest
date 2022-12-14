using System;
using System.Threading.Tasks;
using UnityEngine;

public class TextMover : MonoBehaviour
{
    public float lifeTime
    {
        set => _lifeTime = value;
    }
    private float _lifeTime;
    public float moveSpeed
    {
        set => _moveSpeed = value;
    }
    private float _moveSpeed;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    public void StartFade()
    {
       Invoke("Fade", _lifeTime);
    }

    private void Update()
    {
        _transform.position = transform.position + _transform.up * _moveSpeed * Time.deltaTime;
    }

    private void Fade()
    {
        gameObject.SetActive(false);
    }
}