using UnityEngine;

public class EditorCameraController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float lookSpeed = 2f;
    private float rotX, rotY;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(1))
        {
            rotX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotY += Input.GetAxis("Mouse X") * lookSpeed;
            transform.localEulerAngles = new Vector3(rotX, rotY, 0);
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime);
#endif
    }
}