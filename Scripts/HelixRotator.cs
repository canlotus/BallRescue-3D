using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixRotator : MonoBehaviour
{
    public float rotationSpeed = 300f; // Unity Editor i�in
    public float rotationSpeedAndroid = 20f; // Android i�in
    public float rotationSpeedWebGL = 150f; // WebGL i�in

    private float rotationInput = 0f;

    private void Update()
    {
        // Update i�inde sadece input al�nacak
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
        // Inputa g�re rotasyonu FixedUpdate i�inde yap
        transform.Rotate(Vector3.up, rotationInput * Time.fixedDeltaTime);

        // Bir sonraki frame'de ayn� d�n���n yap�lmamas� i�in rotationInput s�f�rlan�r
        rotationInput = 0f;
    }
}
