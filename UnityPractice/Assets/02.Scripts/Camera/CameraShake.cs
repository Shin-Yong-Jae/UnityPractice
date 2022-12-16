using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraShake : MonoBehaviour
{
    //카메라 흔들기
    public float ShakeAmount;
    float ShakeTime;
    Vector3 initialPosition;
    public void VibrateForTime(float time)
    {
        ShakeTime = time;
    }

    private void Update()
    {
        initialPosition = GameObject.FindWithTag("MainCamera").transform.position;//카메라 흔들릴 위치값
        if (ShakeTime > 0)
        {
            transform.position = UnityEngine.Random.insideUnitSphere * ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            transform.position = initialPosition;
        }
    }

    // DoTween Version.
    [Serializable]
    public class ShakeOption
    {
        public float Duration;
        public Vector3 Strength;
        public int Vibrato;
        public float Randomness;
        public bool FadeOut;
        public AnimationCurve EaseCurve;
    }

    private readonly Dictionary<(string, Camera), Sequence> _sequences = new Dictionary<(string, Camera), Sequence>();
    [Header("화면 shake 옵션")]
    private Dictionary<string, ShakeOption> _shakeOptions;
    public bool TryGetShakeSequence(string shakeOptionName, Camera targetCamera, out Sequence sequence)
    {
        // 수정사항 즉시 반영하기위해 캐싱 해놓은 것 제거
#if UNITY_EDITOR
        _sequences.Remove((shakeOptionName, targetCamera));
#endif
        if (_sequences.TryGetValue((shakeOptionName, targetCamera), out sequence))
        {
            return true;
        }

        if (!_shakeOptions.TryGetValue(shakeOptionName, out var option))
        {
            Debug.LogError($"Invalid shake option name {shakeOptionName}");
            return false;
        }

        sequence = DOTween.Sequence();
        sequence.SetAutoKill(false);
        sequence.Append(targetCamera.DOShakePosition(option.Duration, option.Strength, option.Vibrato, option.Randomness, option.FadeOut).SetEase(option.EaseCurve));
        sequence.Pause();

        _sequences.Add((shakeOptionName, targetCamera), sequence);

        return true;
    }
}