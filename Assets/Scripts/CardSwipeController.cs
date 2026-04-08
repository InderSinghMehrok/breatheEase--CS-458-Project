using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardSwipeController : MonoBehaviour
{
    [Header("Cards")]
    public RectTransform[] cards;
    public float cardSpacing = 340f;
    public float swipeDuration = 0.35f;

    [Header("Scale")]
    public float activeScale = 1f;
    public float inactiveScale = 0.88f;

    [Header("Dots")]
    public Image[] dots;
    public Color dotActive = new Color(0.47f, 0.78f, 0.63f, 1f);
    public Color dotInactive = new Color(0.47f, 0.78f, 0.63f, 0.25f);

    int _current = 0;
    bool _animating = false;
    Vector2 _dragStart;

    void Start() => RefreshLayout(false);

    public void SwipeLeft()
    {
        if (_animating || _current >= cards.Length - 1) return;
        _current++;
        RefreshLayout(true);
    }

    public void SwipeRight()
    {
        if (_animating || _current <= 0) return;
        _current--;
        RefreshLayout(true);
    }

    void RefreshLayout(bool animate)
    {
        _animating = true;

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] == null) continue;

            float targetX = (i - _current) * cardSpacing;
            float targetScale = (i == _current) ? activeScale : inactiveScale;

            if (animate)
            {
                cards[i].DOLocalMoveX(targetX, swipeDuration)
                    .SetEase(Ease.OutCubic)
                    .OnComplete(() => _animating = false);
                cards[i].DOScale(targetScale, swipeDuration)
                    .SetEase(Ease.OutCubic);
            }
            else
            {
                cards[i].localPosition = new Vector3(targetX, 0, 0);
                cards[i].localScale = Vector3.one * targetScale;
                _animating = false;
            }
        }

        RefreshDots();
    }

    void RefreshDots()
    {
        if (dots == null) return;
        for (int i = 0; i < dots.Length; i++)
        {
            if (dots[i] == null) continue;
            dots[i].color = (i == _current) ? dotActive : dotInactive;
        }
    }

    public void OnBeginDrag(Vector2 pos) => _dragStart = pos;

    public void OnEndDrag(Vector2 pos)
    {
        float delta = pos.x - _dragStart.x;
        if (Mathf.Abs(delta) < 50f) return;
        if (delta < 0) SwipeLeft();
        else SwipeRight();
    }

    public int CurrentIndex => _current;
}