using UnityEngine;

public class ControllerRay : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float maxRayLength = 2f;

    void Update()
    {
        if (lineRenderer == null) return;

        Vector3 start = transform.position;
        Vector3 direction = transform.forward;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, start + direction * maxRayLength);
    }
}