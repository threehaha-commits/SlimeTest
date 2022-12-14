using UnityEngine;

public class PlaneMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private bool _isMove;
    private Material _material;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        GameStageController.Instance.AddListener(ChangeAction);
    }

    private void ChangeAction(GameStage stage)
    {
        switch (stage)
        {
            case GameStage.Fight:
                _isMove = false;
                break;
            case GameStage.Move:
                _isMove = true;
                break;
            case GameStage.Reset:
                _isMove = false;
                break;
        }
    }
    
    private void Update()
    {
        if (_isMove == false)
            return;

        var offsetX = _material.mainTextureOffset.x;
        var x = offsetX - _moveSpeed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(x, 0);
    }
}