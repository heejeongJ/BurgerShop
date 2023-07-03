using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    // UI ���� ȿ�� �� ��� ���� ��ũ��Ʈ

    // UI ������Ʈ
    // Default UI
    public Image black;                

    // Story UI
    public GameObject storyUI;
    public GameObject player;
    public GameObject master;
    public TextMeshProUGUI line;

    // StageScene UI
    public GameObject stageUI;
    public GameObject[] stageDayResult = new GameObject[7];
    public TextMeshProUGUI[] stageDayResultText = new TextMeshProUGUI[7];
    public TextMeshProUGUI stageTotalSales;

    public GameObject[] endingImages;
    public GameObject ending;
    public TextMeshProUGUI endingText;

    // MainGame UI
    public Text timeText;              // ���� �ð� 
    public Text moneyText;
    public Text startCountText;
    public GameObject[] lifeUI = new GameObject[3];
    public GameObject resultUI;
    public TextMeshProUGUI orderSheet;
    public TextMeshProUGUI orderPriceSheet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// UI ���� �����̵� ȿ�� ���� �޼���
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="distance"></param>
    public void UpUI(GameObject obj, float distance)
    {
        StartCoroutine(MoveUp(obj, distance));
    }

    /// <summary>
    /// UI �Ʒ��� �����̵� ȿ�� ���� �޼���
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="distance"></param>
    public void DownUI(GameObject obj, float distance)
    {
        StartCoroutine(MoveDown(obj, distance));
    }

    /// <summary>
    /// UI ���� �����̵� ȿ�� ���� �޼���
    /// </summary>
    /// <param name="obj"> ���� UI </param>
    /// <param name="distance"> ������ �Ÿ� </param>
    /// <returns></returns>
    IEnumerator MoveUp(GameObject obj, float distance)
    {
        // time�� ���������� �ö󰡴� ��
        float time = 0f;

        // UI ���� ��ǥ
        Vector3 endPos = obj.transform.localPosition;

        // UI ��� ��ǥ ����
        Vector3 startPos = endPos;
        startPos.y -= distance;

        // ������ �� ���� ��µǴ� y�� ����
        while (startPos.y < endPos.y)
        {
            time += Time.deltaTime;
            startPos.y = Mathf.Lerp(startPos.y, endPos.y, time);
            obj.transform.localPosition = startPos;
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// UI �Ʒ��� �����̵� ȿ�� ���� �޼���
    /// </summary>
    /// <param name="obj"> ���� UI </param>
    /// <param name="distance"> ������ �Ÿ� </param>
    /// <returns></returns>
    IEnumerator MoveDown(GameObject obj, float distance)
    {
        // time�� ���������� �ö󰡴� ��
        float time = 0f;

        // UI ��� ��ǥ
        Vector3 endPos = obj.transform.localPosition;

        // UI ���� ��ǥ ����
        Vector3 startPos = endPos;
        startPos.y -= distance;

        // ������ �� ���� ���ҵǴ� y�� ����
        while (endPos.y > startPos.y)
        {
            time += Time.deltaTime;
            endPos.y = Mathf.Lerp(endPos.y, startPos.y, time);
            obj.transform.localPosition = endPos;
            yield return null;
        }

        // 1�� �� �ٽ� ��ġ ���󺹱�
        yield return new WaitForSeconds(1f);
        startPos.y += distance;
        obj.transform.localPosition = startPos;
        yield return null;
    }

    /// <summary>
    /// ������ ���¿��� ��Ÿ���� �ϴ� ȿ�� �޼���
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    IEnumerator FadeIn(Image image)
    {
        // time�� ���������� �ö󰡴� ��
        float time = 0f;

        // �÷� �޾ƿ���
        Color color = image.color;

        // 1�� ������ ��µǴ� ���� ����
        while (color.a < 1f)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, time);
            image.color = color;
            yield return null;
        }
    }

    /// <summary>
    /// ������ ���¿��� �����ϰ� �ϴ� ȿ�� �޼���
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    IEnumerator FadeOut(Image image)
    {
        // time�� ���������� �ö󰡴� ��
        float time = 0f;
        
        // �÷� �޾ƿ���
        Color color = image.color;

        // �������� ������ ���ҵǴ� ���� �� ����
        while (color.a > 0f)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, time);
            image.color = color;
            yield return null;
        }
    }

    /// <summary>
    /// ����ȭ�� ���̵��� ȿ���� ����ϴ� �޼���
    /// </summary>
    /// <param name="image"></param>
    public void ShowBlack()
    {
        StartCoroutine(FadeIn(black));
    }

    /// <summary>
    /// ����ȭ�� ���̵��� ȿ���� ����ϴ� �޼���
    /// </summary>
    /// <param name="image"></param>
    public void HideBlack()
    {
        StartCoroutine(FadeOut(black));
    }

    /// <summary>
    /// �̹��� ���̵��� ȿ���� ����ϴ� �޼���
    /// </summary>
    /// <param name="image"></param>
    public void Show(Image image)
    {

        StartCoroutine(FadeIn(image));
    }

    /// <summary>
    /// �̹��� ���̵�ƿ� ȿ���� ������� �ϴ� �޼���
    /// </summary>
    /// <param name="image"></param>
    public void Hide(Image image)
    {
        StartCoroutine(FadeOut(image));
    }


}
