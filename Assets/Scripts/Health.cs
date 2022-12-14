using UnityEngine;

[RequireComponent(typeof(Main))]
public class Health : MonoBehaviour, IApplyDamage
{
    private float currentHealth
    {
        get => _main.health;
        set => _main.health = value;
    }
    private Main _main;
    private IDamageReceived[] _damageReceiveds;
    
    private void Start()
    {
        _damageReceiveds = GetComponents<IDamageReceived>();
        _main = GetComponent<Main>();
    }

    void IApplyDamage.AppleDamage(float damageValue)
    {
        currentHealth -= damageValue;
        
        foreach (var receiver in _damageReceiveds)
            receiver.Receive();
    }
}