using System.Threading.Tasks;
using UnityEngine;

public class Player : Main, IReset
{
    #region Данные для апгрейда
    private readonly float _healthStandartValue = 2f;
    private readonly float _healthDivider = 15f;
    private readonly float _damageStandartValue = 3f;
    private readonly float _damageDivider = 12f;
    private readonly float _reloadStandartValue = 0.015f;
    private readonly float _reloadDivider = 30f;
    #endregion

    [SerializeField] private float _respawnTime;
    private int respawnTimeInMillisecond => Mathf.RoundToInt(_respawnTime * 1000);
    [Space]
    [Header("Bullet settings")]
    [Space]
    [SerializeField] private AnimationCurve _bulletPathway;
    [SerializeField] private float _pathWayHeight;
    [SerializeField] private BulletMover _bullet;
    [SerializeField] private float _bulletMoveTime;
    private PlayerAttack _attack;
    private HpVisualChanger _hpVisualChanger;
    
    private void Start()
    {
        _attack = GetComponent<PlayerAttack>();
        _hpVisualChanger = GetComponent<HpVisualChanger>();
        maxHealth = health;
    }
    
    public AnimationCurve GetCurve() => _bulletPathway;
    public float GetPathWayHeight() => _pathWayHeight;
    public BulletMover GetBullet() => _bullet;
    public float GetBulletMoveTime() => _bulletMoveTime;

    async void IReset.Reset()
    {
        health = maxHealth;
        await Task.Delay(respawnTimeInMillisecond);
        gameObject.SetActive(true);
        GameStageController.Instance.Invoke(GameStage.Move);
    }
    
    public void Upgrade(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Health:
                var valueHealth = _healthStandartValue + health / _healthDivider;
                health += valueHealth;
                health = Mathf.RoundToInt(health);
                maxHealth += valueHealth;
                maxHealth = Mathf.RoundToInt(maxHealth);
                _hpVisualChanger.HealthUpgrade();
                break;
            case UpgradeType.Damage:
                var valueDamage = _damageStandartValue + _damage / _damageDivider;
                _damage += valueDamage;
                break;
            case UpgradeType.AttackSpeed:
                var valueAttackSpeed = _reloadStandartValue + _reloadTime / _reloadDivider;
                _reloadTime -= valueAttackSpeed;
                _attack.UpgradeAttackSpeed();
                break;

        }
    }
}