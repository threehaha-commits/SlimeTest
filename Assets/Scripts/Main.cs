using UnityEngine;

public class Main : MonoBehaviour
{
    public AudioClip clip => _attackSound;
    [SerializeField] private AudioClip _attackSound;
    public new AudioSource audio => _audio;
    private AudioSource _audio;
    private LineRenderer _hpBar;
    [SerializeField] protected float _health;
    private float _maxHealth;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _reloadTime;
    private Transform _target;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _hpBar = GetComponentInChildren<LineRenderer>();
    }

    public LineRenderer HpBar => _hpBar;
    
    public float health
    {
        get => _health;
        set => _health = value;
    }

    public Transform target
    {
        get => _target;
        set => _target = value;
    }

    public float maxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    public float GetDamage() => _damage;
    public float GetReloadTime() => _reloadTime;
}