using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixRotator : MonoBehaviour
{
    public float rotationSpeed = 300f; // Unity Editor için
    public float rotationSpeedAndroid = 20f; // Android için
    public float rotationSpeedWebGL = 150f; // WebGL için

    private float rotationInput = 0f;

    private void Update()
    {
        // Update içinde sadece input alýnacak
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            rotationInput = -mouseX * rotationSpeed;
        }

#elif UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float xDeltaPos = Input.GetTouch(0).deltaPosition.x;
            rotationInput = -xDeltaPos * rotationSpeedAndroid;
        }

#elif UNITY_WEBGL
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            rotationInput = -mouseX * rotationSpeedWebGL;
        }
#endif
    }

    private void FixedUpdate()
    {
        // Inputa göre rotasyonu FixedUpdate içinde yap
        transform.Rotate(Vector3.up, rotationInput * Time.fixedDeltaTime);

        // Bir sonraki frame'de ayný dönüþün yapýlmamasý için rotationInput sýfýrlanýr
        rotationInput = 0f;
    }
}
