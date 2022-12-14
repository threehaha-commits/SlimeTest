using UnityEngine;

public class Enemy : Main
{
    [SerializeField] private int _goldForDeath;
    private Gold _gold;
    public float moveSpeed => _moveSpeed;
    [SerializeField] private float _moveSpeed;
    public float DistanceForAttack => _distForAttack;
    [SerializeField] private float _distForAttack;
    private int _age;

    public void SetAge(int age)
    {
        _age = age * 2;
        _goldForDeath += age;
        var speedDivided = 150f;
        _moveSpeed += age / speedDivided;
        _damage += age;
        health += _age;
    }
    
    private void Start()
    {
        GameStageController.Instance.AddEnemy();
        _gold = FindObjectOfType<Gold>();
    }

    private void OnDisable()
    {
        if(GameStageController.Instance != null)
            GameStageController.Instance.RemoveEnemy();
        _gold.Change(_goldForDeath);
    }
}