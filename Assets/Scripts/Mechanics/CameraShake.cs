using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.5f;

    public void ShakeCamera()
    {
        transform.DOShakePosition(shakeDuration, shakeAmount);
    }
}
