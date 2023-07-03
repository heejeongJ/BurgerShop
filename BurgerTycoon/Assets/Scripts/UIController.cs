using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    // UI 등장 효과 및 출력 동작 스크립트

    // UI 오브젝트
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
    public Text timeText;              // 현재 시간 
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
    /// UI 위로 슬라이드 효과 실행 메서드
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="distance"></param>
    public void UpUI(GameObject obj, float distance)
    {
        StartCoroutine(MoveUp(obj, distance));
    }

    /// <summary>
    /// UI 아래로 슬라이드 효과 실행 메서드
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="distance"></param>
    public void DownUI(GameObject obj, float distance)
    {
        StartCoroutine(MoveDown(obj, distance));
    }

    /// <summary>
    /// UI 위로 슬라이드 효과 동작 메서드
    /// </summary>
    /// <param name="obj"> 동작 UI </param>
    /// <param name="distance"> 움직일 거리 </param>
    /// <returns></returns>
    IEnumerator MoveUp(GameObject obj, float distance)
    {
        // time은 연속적으로 올라가는 값
        float time = 0f;

        // UI 도착 좌표
        Vector3 endPos = obj.transform.localPosition;

        // UI 출발 좌표 설정
        Vector3 startPos = endPos;
        startPos.y -= distance;

        // 도착할 때 까지 상승되는 y값 대입
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
    /// UI 아래로 슬라이드 효과 동작 메서드
    /// </summary>
    /// <param name="obj"> 동작 UI </param>
    /// <param name="distance"> 움직일 거리 </param>
    /// <returns></returns>
    IEnumerator MoveDown(GameObject obj, float distance)
    {
        // time은 연속적으로 올라가는 값
        float time = 0f;

        // UI 출발 좌표
        Vector3 endPos = obj.transform.localPosition;

        // UI 도착 좌표 설정
        Vector3 startPos = endPos;
        startPos.y -= distance;

        // 도착할 때 까지 감소되는 y값 대입
        while (endPos.y > startPos.y)
        {
            time += Time.deltaTime;
            endPos.y = Mathf.Lerp(endPos.y, startPos.y, time);
            obj.transform.localPosition = endPos;
            yield return null;
        }

        // 1초 후 다시 위치 원상복귀
        yield return new WaitForSeconds(1f);
        startPos.y += distance;
        obj.transform.localPosition = startPos;
        yield return null;
    }

    /// <summary>
    /// 투명한 상태에서 나타나게 하는 효과 메서드
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    IEnumerator FadeIn(Image image)
    {
        // time은 연속적으로 올라가는 값
        float time = 0f;

        // 컬러 받아오기
        Color color = image.color;

        // 1될 때까지 상승되는 투명도 대입
        while (color.a < 1f)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, time);
            image.color = color;
            yield return null;
        }
    }

    /// <summary>
    /// 선명한 상태에서 투명하게 하는 효과 메서드
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    IEnumerator FadeOut(Image image)
    {
        // time은 연속적으로 올라가는 값
        float time = 0f;
        
        // 컬러 받아오기
        Color color = image.color;

        // 투명해질 때까지 감소되는 투명도 값 대입
        while (color.a > 0f)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, time);
            image.color = color;
            yield return null;
        }
    }

    /// <summary>
    /// 검은화면 페이드인 효과로 출력하는 메서드
    /// </summary>
    /// <param name="image"></param>
    public void ShowBlack()
    {
        StartCoroutine(FadeIn(black));
    }

    /// <summary>
    /// 검은화면 페이드인 효과로 출력하는 메서드
    /// </summary>
    /// <param name="image"></param>
    public void HideBlack()
    {
        StartCoroutine(FadeOut(black));
    }

    /// <summary>
    /// 이미지 페이드인 효과로 출력하는 메서드
    /// </summary>
    /// <param name="image"></param>
    public void Show(Image image)
    {

        StartCoroutine(FadeIn(image));
    }

    /// <summary>
    /// 이미지 페이드아웃 효과로 사라지게 하는 메서드
    /// </summary>
    /// <param name="image"></param>
    public void Hide(Image image)
    {
        StartCoroutine(FadeOut(image));
    }


}
