using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameStageController : MonoBehaviour
{
    public static GameStageController Instance;
    [SerializeField] private float _moveStageTime;
    [SerializeField] private GameStage _gameStage = GameStage.Move;
    private UnityAction<GameStage> _stageAction;
    private float _currentEnemyOnField;
    private Player _player;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        Invoke(GameStage.Move);
    }

    public void AddListener(UnityAction<GameStage> listener)
    {
        _stageAction += listener;
    }

    public void RemoveListener(UnityAction<GameStage> listener)
    {
        _stageAction -= listener;
    }

    public void AddEnemy()
    {
        _currentEnemyOnField++;
    }

    public void RemoveEnemy()
    {
        _currentEnemyOnField--;
        if (_currentEnemyOnField == 0)
            Invoke(GameStage.Move);
    }

    public void Reset()
    {
        //Убираем всех противников с поля
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
            enemy.SetActive(false);
        //Сбрасываем данные игрока
        var resetInterfaces = _player.GetComponents<IReset>();
        foreach (var reset in resetInterfaces)
            reset.Reset();
        //Убираем весь плавающий текст
        var resetFromText = DamageTextController.Instance as IReset;
        resetFromText.Reset();
        Invoke(GameStage.Reset);
    }
    
    public void Invoke(GameStage stage)
    {
        _gameStage = stage;
        _stageAction.Invoke(_gameStage);
        Debug.Log(stage);
        switch (stage)
        {
            case GameStage.Move:
                StartCoroutine(WaitMoveStage());
                break;
        }
    }

    private IEnumerator WaitMoveStage()
    {
        yield return new WaitForSeconds(_moveStageTime);
        if(_gameStage != GameStage.Reset)
            Invoke(GameStage.Fight);
    }
}