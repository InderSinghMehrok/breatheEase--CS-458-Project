using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RoundedCorners : MonoBehaviour
{
    [Range(0f, 0.5f)]
    public float radius = 0.15f;

    void Start() => Apply();
    void OnValidate() => Apply();

    void Apply()
    {
        var img = GetComponent<Image>();
        int size = 256;
        float r = radius * size;

        var tex = new Texture2D(size, size);
        tex.filterMode = FilterMode.Bilinear;

        for (int x = 0; x < size; x++)
        for (int y = 0; y < size; y++)
        {
            float cx = size * 0.5f;
            float cy = size * 0.5f;
            float dx = Mathf.Max(Mathf.Abs(x - cx) - (cx - r), 0f);
            float dy = Mathf.Max(Mathf.Abs(y - cy) - (cy - r), 0f);
            float dist = Mathf.Sqrt(dx * dx + dy * dy);
            float alpha = Mathf.Clamp01(r - dist + 0.5f);
            tex.SetPixel(x, y, new Color(1f, 1f, 1f, alpha));
        }

        tex.Apply();

        var sprite = Sprite.Create(
            tex,
            new Rect(0, 0, size, size),
            new Vector2(0.5f, 0.5f),
            100f, 0,
            SpriteMeshType.FullRect,
            new Vector4(size * 0.25f, size * 0.25f, size * 0.25f, size * 0.25f)
        );

        img.sprite = sprite;
        img.type =Image.Type.Sliced;
    }
}