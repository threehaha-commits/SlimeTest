using System;
using System.Collections;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    private Transform _transform;

    public void SetParams(Transform target, float damage, float time, float height, AnimationCurve curve, AudioSource _audio, AudioClip _clip)
    {
        _transform = transform;
        StartCoroutine(Mover(target, damage, time, height, curve, _audio, _clip));
    }
    
    private IEnumerator Mover(Transform target, float damage, float time, float height, AnimationCurve curve, AudioSource _audio, AudioClip _clip)
    {
        var distCovered = 0f;
        var currentTime = Time.time; 
        var startPosition = _transform.position;
        while (distCovered < 1f)
        {
            if (target.gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(false);
                yield break;
            }
            var targetPos = target.position;
            var y = targetPos.y + curve.Evaluate(distCovered) * height;
            var moveVector = new Vector3(targetPos.x, y, targetPos.z);
            _transform.position = Vector3.Lerp(startPosition,  moveVector, distCovered);
            distCovered = (Time.time - currentTime) / time;
            yield return null;
        }
        DealDamage(target, damage, _audio, _clip);
        gameObject.SetActive(false);
    }

    private void DealDamage(Transform target, float damage, AudioSource _audio, AudioClip _clip)
    {
        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }
        
        var applyDamage = target.GetComponent<IApplyDamage>();
        applyDamage.AppleDamage(damage);
        var targetPos = target.position;
        DamageTextController.Instance.ShowText(Mathf.RoundToInt(damage).ToString(), targetPos);
        _audio.PlayOneShot(_clip);
    }
}