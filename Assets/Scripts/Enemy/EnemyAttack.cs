using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool _isAttack;
    private float _damage;
    private Transform _player;
    private Enemy _enemyMain;
    private Reload _reload;
    private float _reloadTime;
    private IApplyDamage _applyDamage;
    private AudioSource _audio;
    private AudioClip _clip;
    
    private void Start()
    {
        _enemyMain = GetComponent<Enemy>();
        _damage = _enemyMain.GetDamage();
        _reloadTime = _enemyMain.GetReloadTime();
        _player = _enemyMain.target;
        _reload = new Reload(_reloadTime);
        _audio = _enemyMain.audio;
        _clip = _enemyMain.clip;
        _applyDamage = _player.GetComponent<IApplyDamage>();
    }

    public void StartAttack()
    {
        _isAttack = true;
    }

    private void Update()
    {
        if (_isAttack == false)
            return;
        
        if (_reload.isEnd == false)
            return;

        _applyDamage.AppleDamage(_damage);
        var playerPos = _player.transform.position;
        DamageTextController.Instance.ShowText(Mathf.RoundToInt(_damage).ToString(), playerPos);
        _audio.PlayOneShot(_clip);
        _reload.Start();
    }
}