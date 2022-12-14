using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMove : MonoBehaviour
{
    private Rigidbody _rb;
    private Transform _target;
    private Enemy _enemyMain;
    private bool _isMove = true;
    private Transform _transform;
    private float _moveSpeed;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
        _enemyMain = GetComponent<Enemy>();
        _moveSpeed = _enemyMain.moveSpeed;
        _target = _enemyMain.target;
    }

    public void EndMove()
    {
        _isMove = false;
        _rb.velocity = Vector3.zero;
    }
    
    private void FixedUpdate()
    {
        if (_isMove == false)
            return;
        
        if (_target == null)
            return;

        var direction = _target.position - _transform.position;
        _rb.velocity = transform.forward + direction * _moveSpeed * Time.fixedDeltaTime;
    }
}