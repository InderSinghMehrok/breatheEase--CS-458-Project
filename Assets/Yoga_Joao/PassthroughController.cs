using UnityEngine;

public class PassthroughController : MonoBehaviour
{
    public OVRPassthroughLayer passthroughLayer;

    // Opacity: 0 = fully virtual, 1 = fully real world
    public void ShowRealWorld()
    {
        passthroughLayer.gameObject.SetActive(true);
        SetOpacity(1f);
    }

    public void HideRealWorld()
    {
        // Don't deactivate instantly — fade out first (see FadeOut below)
        SetOpacity(0f);
        passthroughLayer.gameObject.SetActive(false);
    }

    public void SetOpacity(float opacity)
    {
        passthroughLayer.textureOpacity = opacity;
    }

    // Smooth fade in/out
    public void FadeIn(float duration)
    {
        StartCoroutine(FadeRoutine(0f, 1f, duration));
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(FadeRoutine(1f, 0f, duration));
    }

    System.Collections.IEnumerator FadeRoutine(float from, float to, float duration)
    {
        passthroughLayer.gameObject.SetActive(true);
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            passthroughLayer.textureOpacity = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        passthroughLayer.textureOpacity = to;

        if (to == 0f)
            passthroughLayer.gameObject.SetActive(false);
    }
}