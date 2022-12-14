using System;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    #region Множители цены
    private readonly float _healthMultiplier = 1.35f;
    private readonly float _damageMultiplier = 1.35f;
    private readonly float _attackSpeedMultiplier = 1.5f;
    #endregion
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private UpgradeType _type = UpgradeType.Health;
    [SerializeField] private float _costValue;
    private Player _player;
    private Gold _gold;
    
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _gold = FindObjectOfType<Gold>();
        StartVisual();
    }

    private void StartVisual()
    {
        switch (_type)
        {
            case UpgradeType.Health:
                _value.text = $"{Mathf.Round(_player.health)}";
                break;
            case UpgradeType.Damage:
                _value.text = $"{Mathf.Round(_player.GetDamage())}";
                break;
            case UpgradeType.AttackSpeed:
                _value.text = $"{Math.Round(_player.GetReloadTime(), 2)}";
                break;
        }

        _costText.text = $"{Mathf.Round(_costValue)}G";
    }

    private void UpgradeVisual()
    {
        switch (_type)
        {
            case UpgradeType.Health:
                _value.text = $"{Mathf.RoundToInt(_player.health)}";
                _costValue *= _healthMultiplier;
                break;
            case UpgradeType.Damage:
                _value.text = $"{Mathf.RoundToInt(_player.GetDamage())}";
                _costValue *= _damageMultiplier;
                break;
            case UpgradeType.AttackSpeed:
                _value.text = $"{Math.Round(_player.GetReloadTime(), 2)}";
                _costValue *= _attackSpeedMultiplier;
                break;
        }

        _costValue = Mathf.RoundToInt(_costValue);
        _costText.text = $"{_costValue}G";
    }

    public void Upgrade()
    {
        if(_gold.current >= _costValue)
        {
            _gold.Change(-Mathf.RoundToInt(_costValue));
            _player.Upgrade(_type);
            UpgradeVisual();
        }
    }
}