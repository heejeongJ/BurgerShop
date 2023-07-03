using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class MouseLook : MonoBehaviour
    {

        public float mouseXSensitivity = 100f;

        public Transform playerBody;

        float xRotation = 0f;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseXSensitivity * Time.deltaTime;

            // 40 x 20 ~ 40
            // 180 y 140 ~ -180 

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            Debug.Log(transform.eulerAngles.x);
            if (transform.eulerAngles.x > 20 && transform.eulerAngles.x < 40)
            {
                transform.localRotation = Quaternion.Euler(xRotation, 180f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }
    }
}