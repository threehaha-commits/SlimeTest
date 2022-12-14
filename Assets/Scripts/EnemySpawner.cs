using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    private Vector3 _spawnPosition;
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _timeBeetwenSpawn;
    private Transform _player;
    private int _enemyAge = 1;
    private int _countUpgradeStep = 5;
    
    private void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        GameStageController.Instance.AddListener(ChangeAction);
        _spawnPosition = SetSpawnPosition();
    }

    private Vector3 SetSpawnPosition()
    {
        var cam = Camera.main;
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        var rightPlane = planes[1];
        var point = rightPlane.ClosestPointOnPlane(_player.transform.position);
        return point;
    }
    
    private void ChangeAction(GameStage stage)
    {
        switch (stage)
        {
            case GameStage.Fight:
                StartCoroutine(Spawn());
                break;
        }
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            var position = GetRandomSpawnPosition();
            var dir = _player.position - position;
            var rotation = Quaternion.LookRotation(dir);
            var enemy = Instantiate(_enemy, position, rotation);
            enemy.target = _player;
            enemy.SetAge(_enemyAge);
            yield return new WaitForSeconds(_timeBeetwenSpawn);
        }

        _enemyAge++;
        CheckEnemyCountUpgrade();
    }

    private void CheckEnemyCountUpgrade()
    {
        if (_enemyAge % _countUpgradeStep == 0)
            _enemyCount++;
    }
    
    private Vector3 GetRandomSpawnPosition()
    {
        var randomOffset = 5f;
        var spawnPosition = _spawnPosition;
        var positionZ = Random.Range(spawnPosition.z - randomOffset, spawnPosition.z + randomOffset);
        var playerPosY = _player.transform.position.y;
        var position = new Vector3(spawnPosition.x, playerPosY, positionZ);
        return position;
    }
}