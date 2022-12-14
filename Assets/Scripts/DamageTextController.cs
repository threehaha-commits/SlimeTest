using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextController : MonoBehaviour, IReset
{
    public static DamageTextController Instance;
    [SerializeField] private float _lifeTimeText;
    [SerializeField] private float _moveSpeedText;
    [SerializeField] private TMP_Text _textTemplate;
    private List<TMP_Text> _damageText = new();

    private void Awake()
    {
        Instance = this;
    }

    public void ShowText(string message, Vector3 position)
    {
        var text = GetFreeText();
        text.text = message;
        text.transform.position = position;
        text.GetComponent<TextMover>().StartFade();
    }

    private TMP_Text GetFreeText()
    {
        foreach (var text in _damageText)
        {
            if (text.gameObject.activeInHierarchy == false)
            {
                text.gameObject.SetActive(true);
                return text;
            }
        }

        var newText = CreateNewText();
        _damageText.Add(newText);
        return newText;
    }

    void IReset.Reset()
    {
        foreach (var text in _damageText)
            text.gameObject.SetActive(false);
    }
    
    private TMP_Text CreateNewText()
    {
        var text = Instantiate(_textTemplate);
        var textMover = text.GetComponent<TextMover>();
        textMover.lifeTime = _lifeTimeText;
        textMover.moveSpeed = _moveSpeedText;
        return text;
    }
}