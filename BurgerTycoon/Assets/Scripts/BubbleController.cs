using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    // Ƣ��� ��ǰ ȿ�� ���� ��ũ��Ʈ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Ƣ��� ȿ��
        ShowBubble();
    }

    /// <summary>
    /// Ƣ��� ��ǰ ���� Ȱ��ȭ �޼���
    /// </summary>
    void ShowBubble()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        Invoke("HideBubble", Random.Range(0, 2f));
    }

    /// <summary>
    /// Ƣ��� ��ǰ ���� ��Ȱ��ȭ �޼���
    /// </summary>
    void HideBubble()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Invoke("ShowBubble", Random.Range(0, 2f));
    }
}
