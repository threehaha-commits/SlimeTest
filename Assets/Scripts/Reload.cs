using System.Threading.Tasks;
using UnityEngine;

public class Reload
{
    private readonly int reloadTimeInMilliseconds;
    public bool isEnd { get; private set; } = true;
    
    public Reload(float reloadTime)
    {
        reloadTimeInMilliseconds = Mathf.RoundToInt(reloadTime * 1000);
    }

    public async void Start()
    {
        isEnd = false;
        await Task.Delay(reloadTimeInMilliseconds);
        isEnd = true;
    }
}