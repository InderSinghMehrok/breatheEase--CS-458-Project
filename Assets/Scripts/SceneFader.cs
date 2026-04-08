using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    [Header("Fade Settings")]
    public float fadeDuration = 2f;

    MeshRenderer _quad;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);
        CreateFadeQuad();
    }

    void CreateFadeQuad()
    {
        var quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.name = "FadeQuad";
        quad.transform.SetParent(transform);
        quad.transform.localPosition = new Vector3(0, 0, 0.3f);
        quad.transform.localScale = new Vector3(2f, 2f, 1f);
        Destroy(quad.GetComponent<MeshCollider>());

        var mat = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        mat.color = Color.black;
        mat.renderQueue = 4000;

        var renderer = quad.GetComponent<MeshRenderer>();
        renderer.material = mat;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        _quad = renderer;
        SetAlpha(0f);
    }

    void Start() => FadeIn();

    void SetAlpha(float alpha)
    {
        if (_quad == null) return;
        var col = _quad.material.color;
        col.a = alpha;
        _quad.material.color = col;
    }

    public void FadeIn(float duration = -1f)
    {
        if (duration < 0) duration = fadeDuration;
        SetAlpha(1f);
        DOTween.To(() => _quad.material.color.a,
            x => SetAlpha(x), 0f, duration)
            .SetEase(Ease.OutQuad);
    }

    public void FadeToScene(string sceneName, float duration = -1f)
    {
        if (duration < 0) duration = fadeDuration;
        DOTween.To(() => _quad.material.color.a,
            x => SetAlpha(x), 1f, duration)
            .SetEase(Ease.InQuad)
            .OnComplete(() => SceneManager.LoadScene(sceneName));
    }
}