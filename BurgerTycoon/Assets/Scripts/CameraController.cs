using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 카메라 좌우 동작 기능 스크립트
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        // 좌우 이동
        SimpleMove();
    }

    /// <summary>
    /// 연속적인 카메라 움직임 동작 메서드
    /// </summary>
    void SmoothMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x < 1.61f)
                transform.Translate(Vector3.left * 0.5f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x > -6.3f)
                transform.Translate(Vector3.right * 0.5f);
        }
    }

    /// <summary>
    /// 끊기는 카메라 움직임 동작 메서드
    /// </summary>
    void SimpleMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (transform.position.x < 0)
                transform.Translate(Vector3.left * 3.1f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (transform.position.x > -5)
                transform.Translate(Vector3.right * 3.1f);
        }
    }
}
