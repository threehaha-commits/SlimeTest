using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Main))]
public class Death : MonoBehaviour, IDamageReceived
{
    private float _currentHealth;
    private Main _main;

    private void Start()
    {
        _main = GetComponent<Main>();
    }
    
    void IDamageReceived.Receive()
    {
        _currentHealth = _main.health;
        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            CheckIfPlayer();
        }
    }

    private void CheckIfPlayer()
    {
        if (gameObject.tag.Equals("Player"))
            GameStageController.Instance.Reset();
    }
}