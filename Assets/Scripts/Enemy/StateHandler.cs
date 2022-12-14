using UnityEngine;

public class StateHandler : MonoBehaviour
{
        private float _distForAttack;
        private Enemy _enemyMain;
        private Transform _player;
        private Transform _transform;
        private EnemyAttack _attack;
        private EnemyMove _move;
        
        private void Start()
        {
                _attack = GetComponent<EnemyAttack>();
                _move = GetComponent<EnemyMove>();
                _transform = transform;
                _enemyMain = GetComponent<Enemy>();
                _player = _enemyMain.target;
                _distForAttack = _enemyMain.DistanceForAttack;
        }

        private void FixedUpdate()
        {
                var dist = (_player.position - _transform.position).sqrMagnitude;
                if (dist <= _distForAttack)
                {
                        _attack.StartAttack();
                        _move.EndMove();
                        Destroy(this);//Решил использовать подобный подход, тк состояние противника больше меняться не будет
                }
        }
}