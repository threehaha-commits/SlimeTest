using System;
using TMPro;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] private int _current;
    private TMP_Text _goldText;
    public int current => _current;

    private void Start()
    {
        _goldText = GetComponent<TMP_Text>();
    }

    public void Change(int value)
    {
        _current += value;
        _goldText.text = $"Gold: {_current}";
    }
}