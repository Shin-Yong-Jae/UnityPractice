using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorUtil
{
    public static void CallRunAnimationCoroutine(GameObject _coroutineObj, Animator _anim, string _animationName, System.Action _callback = null, float _targetNormalizedTime = 1.0f, float _startNormalizedTime = 0.0f, int _layer = 0)
    {
        _coroutineObj.GetComponent<MonoBehaviour>().StopAllCoroutines();
        _coroutineObj.GetComponent<MonoBehaviour>().StartCoroutine(GenerateRunAnimationCoroutine(_anim, _animationName, _callback, _targetNormalizedTime, _startNormalizedTime, _layer));
    }

    public static IEnumerator GenerateRunAnimationCoroutine(Animator _anim, string _animationName, System.Action _callback = null, float _targetNormalizedTime = 1.0f, float _startNormalizedTime = 0.0f, int _layer = 0)
    {
        _anim.StopPlayback();
        _anim.Play(_animationName, _layer, _startNormalizedTime);

        yield return null;

        while (true)
        {
            var info = _anim.GetCurrentAnimatorStateInfo(_layer);

            if (info.normalizedTime >= _targetNormalizedTime)
                break;

            yield return null;
        }

        if (_callback != null)
            _callback();
    }
}
