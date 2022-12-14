using TMPro;
using UnityEngine;

[RequireComponent(typeof(Main))]
public class HpVisualChanger : MonoBehaviour, IDamageReceived, IReset
{
    private TMP_Text _healthText;
    private float _maxHealth;
    private LineRenderer _bar;
    private Main _main;
    private float _maxLength;
    private IDamageReceived _damageReceived;
    
    private void Start()
    {
        _damageReceived = this;
        _main = GetComponent<Main>();
        _bar = _main.HpBar;
        _healthText = GetComponentInChildren<TMP_Text>();
        _healthText.text = _main.health.ToString();
        _maxHealth = _main.health;
        _maxLength = _bar.GetPosition(1).z;
    }

    public void HealthUpgrade()
    {
        _maxHealth = _main.maxHealth;
        _damageReceived.Receive();
    }
    
    void IDamageReceived.Receive()
    {
        var currentHealth = _main.health;
        if (currentHealth <= 0)
            currentHealth = 0;

        ChangeText(currentHealth);
        ChangeBar(currentHealth);
    }

    private void ChangeText(float currentHealth)
    {
        _healthText.text = Mathf.RoundToInt(currentHealth).ToString();
    }

    void IReset.Reset()
    {
        ChangeText(_main.maxHealth);
        ChangeBar(_main.maxHealth);
    }
    
    private void ChangeBar(float currentHealth)
    {
        var value = currentHealth * _maxLength / _maxHealth;
        var newLength = new Vector3(0, 0, value);
        _bar.SetPosition(1, newLength);
    }
}