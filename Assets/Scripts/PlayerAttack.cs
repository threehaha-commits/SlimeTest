using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAttack : MonoBehaviour
{
    private bool _isAttack;
    private BulletMover _bullet;
    private float _bulletMoveTime;
    private float _pathWayHeight;
    private AudioSource _audio;
    private AudioClip _clip;
    private Transform _target
    {
        get => _player.target;
        set => _player.target = value;
    }
    private AnimationCurve _bulletPathway;
    private float _reloadTime;
    private float _damage => _player.GetDamage();
    private Reload _reload;
    private Player _player;
    
    private void Start()
    {
        _player = GetComponent<Player>();
        _bullet = _player.GetBullet();
        _bulletPathway = _player.GetCurve();
        _reloadTime = _player.GetReloadTime();
        _pathWayHeight = _player.GetPathWayHeight();
        _bulletMoveTime = _player.GetBulletMoveTime();
        _audio = _player.audio;
        _clip = _player.clip;
        _reload = new Reload(_reloadTime);
        GameStageController.Instance.AddListener(ChangeAction);
    }

    private void ChangeAction(GameStage stage)
    {
        switch (stage)
        {
            case GameStage.Fight:
                _isAttack = true;
                _target = FindTarget();
                break;
            case GameStage.Move:
                _isAttack = false;
                break;
        }
    }

    public void UpgradeAttackSpeed()
    {
        _reloadTime = _player.GetReloadTime();
        _reload = new Reload(_reloadTime);
    }
    
    private void Update()
    {
        if (_isAttack == false)  return;
        if (_reload.isEnd == false) return;
        if (_target == null)
        {
            _target = FindTarget();
            return;
        }
        else
        {
            if(_target.gameObject.activeInHierarchy == false)
            {
                _target = null;
                return;
            }
        }
        if(TargetIsVisible() == false)
            return;
        var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        bullet.SetParams(_target, _damage, _bulletMoveTime, _pathWayHeight, _bulletPathway, _audio, _clip);
        _reload.Start();
    }

    private bool TargetIsVisible()
    {
        var cam = Camera.main;
        var planes = GeometryUtility.CalculateFrustumPlanes(cam);
        var collider = _target.GetComponent<Collider>();
        return GeometryUtility.TestPlanesAABB(planes, collider.bounds);
    }
    
    private Transform FindTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var maxDistance = Mathf.Infinity;
        Transform returnedEnemy = null;
        foreach (var enemy in enemies)
        {
            var dist = (enemy.transform.position - transform.position).sqrMagnitude;
            if (dist < maxDistance)
            {
                maxDistance = dist;
                returnedEnemy = enemy.transform;
            }
        }
        return returnedEnemy;
    }
}