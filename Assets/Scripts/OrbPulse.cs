using UnityEngine;
using DG.Tweening;

public class OrbPulse : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(1.12f, 2f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}