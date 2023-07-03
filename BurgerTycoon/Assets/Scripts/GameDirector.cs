using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviour
{
    // 메인게임 상태 정보값
    public bool isGame;                 // 게임 시작했는지
    public bool isJudge;                // 판정했는지
    public int life;                    // 현재 생명 수 - heart
    public int totalMoney;              // 번 돈

    // timer 생성 및 기제 용도
    private float curTime;                  // 현재 시간 tIme

    // 개수                                 
    public int soldBurger;                  // 판매 버거 개수
    public int[] soldIndgredient;           // 판매 재료 개수

    public GameObject customerPrefab;            // 손님 프리팹
    public GameObject customer;                  // 생성된 손님 오브젝트

    // 컴포넌트 불러오기
    public OrderGenerator orderGenerator;
    public PlayerController playerController;
    public SoundDirector soundDirector;
    public UIController uiController;
    public StartDirector sceneDirector;

    // 관리할 기능 오브젝트
    public GameObject friesTray;
    public GameObject breadTopTray;
    public GameObject breadBottomTray;
    public GameObject pattyTray;
    public GameObject cheeseTray;
    public GameObject tomatoTray;
    public GameObject lettuceTray;
    public GameObject eggTray;
    public GameObject chickenTray;
    public GameObject nuggetTray;


    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        orderGenerator = GetComponent<OrderGenerator>();
        playerController = GetComponent<PlayerController>();
        soundDirector = GetComponent<SoundDirector>();
        uiController = GetComponent<UIController>();

        // 게임 정보 초기화
        isGame = false;
        life = 3;
        totalMoney = 0;
        curTime = 70;
        soldBurger = 0;
        soldIndgredient = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0};

        // LIFE 초기화
        uiController.lifeUI[0].SetActive(true);
        uiController.lifeUI[1].SetActive(true);
        uiController.lifeUI[2].SetActive(true);

        // 게임 시작
        uiController.HideBlack();
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 시작하면 타이머 설정
        if (isGame)
        {
            Timer();
        }
    }

    /// <summary>
    /// 시작할 때 카운트 출력하는 메서드
    /// </summary>
    /// <returns></returns>
    IEnumerator StartCount()
    {
        // 시작할 때 카운트 출력
        yield return new WaitForSeconds(1);
        uiController.startCountText.text = "3";
        yield return new WaitForSeconds(1);
        uiController.startCountText.text = "2";
        yield return new WaitForSeconds(1);
        uiController.startCountText.text = "1";
        yield return new WaitForSeconds(1);
        uiController.startCountText.text = "START";
        yield return new WaitForSeconds(1);
        uiController.startCountText.text = "";
    }

    /// <summary>
    /// 게임 시작하는 메서드
    /// </summary>
    /// <returns></returns>
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);

        // 게임 시작
        isGame = true;

        // 가능 재료들 활성화
        friesTray.SetActive(true);
        breadBottomTray.SetActive(true);
        breadTopTray.SetActive(true);
        pattyTray.SetActive(true);
        cheeseTray.SetActive(true);
        lettuceTray.SetActive(true);
        tomatoTray.SetActive(true);

        // 3일차 계란 재료 해금
        if(StageDirector.playDate >= 2)
        {
            eggTray.SetActive(true);
        }

        // 4일차 치킨패티 재료 해금
        if (StageDirector.playDate >= 3)
        {
            chickenTray.SetActive(true);
        }

        // 5일차 너겟 재료 해금
        if (StageDirector.playDate >= 4)
        {
            nuggetTray.SetActive(true);
        }

        // 손님 생성
        StartCoroutine(GenerateCustomer());
    }

    /// <summary>
    /// 손님 생성 메서드
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateCustomer()
    {
        // 게임 중이 아니면 메서드 종료
        if (!isGame)
        {
            yield return null;
        }

        // 1초 후에
        yield return new WaitForSeconds(1);

        // 판정 여부 갱신
        isJudge = false;

        // 등장 효과음
        soundDirector.SetAudioClip(soundDirector.customerComingSound);
        soundDirector.Play();

        // 손님 오브젝트 생성
        Color customerColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        customer = Instantiate(customerPrefab);

        // 손님 컬러 랜덤 지정
        MeshRenderer[] customerMeterial = new MeshRenderer[2];
        customerMeterial = customer.GetComponentsInChildren<MeshRenderer>();
        customerMeterial[0].material.color = customerMeterial[1].material.color = customerColor;

        // 주문 생성
        yield return new WaitForSeconds(2);
        orderGenerator.GenerateOrder();
    }

    /// <summary>
    /// LIFE 감소 함수
    /// </summary>
    void DecreaseLife()
    {
        // 수치 갱신
        life--;

        // UI 비활성화
        if (life == 2)
            uiController.lifeUI[2].SetActive(false);
        else if(life == 1)
            uiController.lifeUI[1].SetActive(false);
        else if(life == 0)
            uiController.lifeUI[0].SetActive(false);

        if (isGame && life <= 0)
        {
            // LIFE 0이면 게임 오버
            // 한번만 실행 위해 게임 중인지 체크
            EndGame();
        }
    }


    /// <summary>
    /// 타이머 동작 메서드
    /// </summary>
    void Timer()
    {
        // 시간이 남으면
        if (curTime > 0)
        {
            // 시간 감소
            curTime -= Time.deltaTime;

            // 시간UI 출력
            uiController.timeText.text = Mathf.Ceil(curTime).ToString();
            uiController.timeText.text = string.Format("{00:N2}", curTime);
        }
        else
        {
            // 타임 오버
            curTime = 0;

            // 시간UI 출력
            uiController.timeText.text = string.Format("{00:N2}", curTime);

            // 타이머 끝나면 게임 종료
            if(isGame)
                EndGame();
        }
    }


    /// <summary>
    /// 햄버거 레시피 판정 메서드
    /// </summary>
    public void JudgeBurger()
    {
        // 손님 없으면 메서드 종료
        if(!customer || isJudge)
        {
            return;
        }

        // 현재 손님 판정함 표시
        isJudge = true;

        // 판정 시작
        bool isCorrect = false;
        uiController.orderSheet.text = "";

        if (orderGenerator.order.Length == playerController.make.Length)
        {
            for (int i = 0; i < orderGenerator.order.Length; i++)
            {
                if (orderGenerator.order[i] != playerController.make[i])
                {
                    // 하나라도 레시피가 잘못된 경우

                    // 주문서 출력
                    uiController.orderSheet.text = "햄버거 레시피가\n잘못되었잖아요-_-!\n";
                    uiController.orderPriceSheet.text = " 획득 실패!";

                    // 라이프 감소
                    DecreaseLife();

                    // 손님 삭제
                    StartCoroutine(DestroyCustomer());

                    // 메서드 종료
                    return;
                }
            }

            // 레시피 성공으로 세팅
            isCorrect = true;
        }
        else
        {
            // 주문 레시피와 제조 레시피 리스트 길이 안맞는 경우 판정 실패
            uiController.orderSheet.text = "햄버거 레시피가\n잘못되었잖아요-_-!\n";
        }

        // 콜라 안줬을 때 주문서 출력
        if (!playerController.isCoke)
        {
            uiController.orderSheet.text += "콜라는요!\n";
        }

        // 감자튀김 안줬을 때 주문서 출력
        if (!playerController.isFires)
        {
            uiController.orderSheet.text += "감자튀김은요!\n";
        }

        // 햄버거, 콜라, 감자튀김 모두 충족한 경우
        if (isCorrect && playerController.isCoke && playerController.isFires)
        {
            // 버거 판매
            SaleBurger();

            // 주문서 출력
            uiController.orderSheet.text = "잘 먹겠습니다 ♥_♥!";
            uiController.orderPriceSheet.text += " 획득!";
        }
        else
        {
            // 하나라도 틀릴 경우
            //라이프 감소
            DecreaseLife();

            // UI 출력
            uiController.orderPriceSheet.text = " 획득 실패!";
        }

        // 손님 삭제
        StartCoroutine(DestroyCustomer());
    }

    /// <summary>
    /// 버거 판매 메서드
    /// </summary>
    void SaleBurger()
    {
        // 버거 개수 카운트
        soldBurger++;                                                        // 버거 개수 증가

        // 매출 갱신
        totalMoney += orderGenerator.orderPrice;                             // 돈 계산

        // UI 갱신
        uiController.moneyText.text = totalMoney.ToString();

        // 재료 개수 카운트
        int[] ingredientCount = orderGenerator.GetIngredientCount();      // 재료 개수 증가

        // 개수 정보 갱신
        for(int i = 0; i < soldIndgredient.Length; i++)
        {
            soldIndgredient[i] += ingredientCount[i];
        }

    }

    /// <summary>
    /// 손님 퇴장 메서드
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyCustomer()
    {
        // 판정 2초 후
        yield return new WaitForSeconds(2);

        // 손님 오브젝트 삭제
        Destroy(customer);

        // 주문서 초기화
        uiController.orderSheet.text = "";
        uiController.orderPriceSheet.text = "";

        // 변수 초기화
        customer = null;

        // 제조 버거 리스트 및 콜라, 감자튀김 조건 초기화
        for(int i = 0; i < playerController.makingList.Length; i++)
        {
            Destroy(playerController.makingList[i]);
            Destroy(GameObject.Find("PackedCokePrefab(Clone)"));
            Destroy(GameObject.Find("PackedFriesPrefab(Clone)"));
            playerController.makingList[i] = null;
            playerController.make[i] = 0;
        }
        playerController.index = 0;
        playerController.isCoke = false;
        playerController.isFires = false;

        // 새로운 손님 생성
        StartCoroutine(GenerateCustomer());

    }

    /// <summary>
    /// 게임 끝내기
    /// </summary>
    void EndGame()
    {
        // 게임 멈추기
        isGame = false;

        // 스테이지 정보 업데이트

        // 일일매출
        StageDirector.dateSale[StageDirector.playDate] = totalMoney * 10;

        // 버거 판매 개수
        StageDirector.dateBurgerSale[StageDirector.playDate] = soldBurger;

        // 매출 갱신
        StageDirector.totalSale += totalMoney * 10;

        // 날짜 카운트
        StageDirector.playDate++;



        // 정산서 띄우기
        // 효과음 출력
        soundDirector.SetAudioClip(soundDirector.billSound);
        soundDirector.Play();

        // 정산서 활성화
        uiController.resultUI.SetActive(true);

        // 위로 슬라이드 효과
        uiController.UpUI(uiController.resultUI, 1400f);

        // 내용 갱신 및 출력
        GameObject.Find("content").GetComponent<Text>().text = "햄버거\n\n빵\n패티\n토마토\n양상추\n치즈\n";
        GameObject.Find("contentValue").GetComponent<Text>().text = soldBurger.ToString() + "개\n\n" +
            soldIndgredient[0] + "개\n" +
            soldIndgredient[1] + "개\n" +
            soldIndgredient[2] + "개\n" +
            soldIndgredient[3] + "개\n" +
            soldIndgredient[4] + "개\n";

        // 3일차 이상 계란 기록 출력
        if (StageDirector.playDate >= 3)
        {
            GameObject.Find("content").GetComponent<Text>().text += "계란\n";
            GameObject.Find("contentValue").GetComponent<Text>().text += soldIndgredient[5] + "개\n";
        }

        // 4일차 이상 치킨 기록 출력
        if (StageDirector.playDate >= 4)
        {
            GameObject.Find("content").GetComponent<Text>().text += "치킨 패티\n";
            GameObject.Find("contentValue").GetComponent<Text>().text += soldIndgredient[6] + "개\n";
        }

        // 5일차 이상 너겟 기록 출력
        if (StageDirector.playDate >= 5)
        {
            GameObject.Find("content").GetComponent<Text>().text += "너겟\n";
            GameObject.Find("contentValue").GetComponent<Text>().text += soldIndgredient[7] + "개\n";
        }

        GameObject.Find("moneyValue").GetComponent<Text>().text = totalMoney.ToString() + "원";
    }
}