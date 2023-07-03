using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    // Æ¢±è±â °ÅÇ° È¿°ú µ¿ÀÛ ½ºÅ©¸³Æ®

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Æ¢±è±â È¿°ú
        ShowBubble();
    }

    /// <summary>
    /// Æ¢±è±â °ÅÇ° ·£´ý È°¼ºÈ­ ¸Þ¼­µå
    /// </summary>
    void ShowBubble()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        Invoke("HideBubble", Random.Range(0, 2f));
    }

    /// <summary>
    /// Æ¢±è±â °ÅÇ° ·£´ý ºñÈ°¼ºÈ­ ¸Þ¼­µå
    /// </summary>
    void HideBubble()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Invoke("ShowBubble", Random.Range(0, 2f));
    }
}
