using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDirector : MonoBehaviour
{
    // 스테이지 진행, 정보값 관리 및 UI 정보 갱신

    // 정보 파라미터
    public static int[] dateBurgerSale = { 0, 0, 0, 0, 0, 0, 0 };     // 일일 버거 판매 수
    public static int[] dateSale = { 0, 0, 0, 0, 0, 0, 0 };           // 일일 매출
    public static int totalSale = 0;                                  // 총 매출
    public static int playDate = 0;                                   // 영업 한 일자 카운트

    // 컴포넌트 불러오기
    UIController uiController;
    StartDirector startDirector;
    SoundDirector soundDirector;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        uiController = GetComponent<UIController>();
        startDirector = GetComponent<StartDirector>();
        soundDirector = GetComponent<SoundDirector>();
        audioSource = GetComponent<AudioSource>();

        // 스테이지UI 초기화
        uiController.stageUI.SetActive(false);

        // 스테이지 정보 세팅
        SetDateValue();

        // 페이드인 장면 전환 효과
        uiController.HideBlack();

        // 스테이지 진행 시작
        StartCoroutine(ShowStage());

        // 영업 7일차 플레이 하면
        if(playDate == 7)
        {
            // 게임 엔딩 출력
            StartCoroutine(ShowEnding());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 스테이지 UI 활성화 메서드
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowStage()
    {
        yield return new WaitForSeconds(1f);
        uiController.stageUI.SetActive(true);
        uiController.UpUI(uiController.stageUI, 1400f);
        yield return null;
    }

    /// <summary>
    /// 스테이지 정보 세팅 메서드
    /// </summary>
    void SetDateValue()
    {
        // 영업 한 일자 만큼
        for(int i = 0; i < playDate; i++)
        {
            // 일일 결과UI 활성화
            uiController.stageDayResult[i].SetActive(true);

            // 일일 매출 결과 출력
            uiController.stageDayResultText[i].text = "판매한 버거\n" + dateBurgerSale[i].ToString() + "\n일일 매출\n" + dateSale[i].ToString();
            
            // 일일 매출 가격에 따라 평가 코멘트 출력
            if(dateSale[i] > 650000)
            {
                uiController.stageDayResultText[i].text += "\n아주 좋아!";
            }
            else if(dateSale[i] > 570000)
            {
                uiController.stageDayResultText[i].text += "\n잘하고 있어!";
            }
            else
            {
                uiController.stageDayResultText[i].text += "\n분발하자..";
            }

            // 총 매출 출력
            uiController.stageTotalSales.text = totalSale.ToString() + "원";
        }
    }

    /// <summary>
    /// 엔딩 출력 메서드
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowEnding()
    {
        // 장면 전환 효과
        uiController.ShowBlack();
        yield return new WaitForSeconds(2f);

        // 장면1 활성화 
        soundDirector.SetAudioClip(soundDirector.voiceSound);
        soundDirector.Play();
        uiController.ending.SetActive(true);
        uiController.endingImages[0].SetActive(true);
        uiController.endingText.text = "일주일동안 번 총 금액은\n" + totalSale + "원..";
        yield return new WaitForSeconds(2f);

        if (totalSale > 4000000)
        {
            // 해피엔딩 장면2 
            soundDirector.Play();
            uiController.endingText.text = "버거를 열심히 판 덕분에\n등록금을 마련할 수 있었어..!";
            yield return new WaitForSeconds(2f);

            // 해피엔딩 장면3 
            soundDirector.Play();
            uiController.endingText.text = "이제 안심하고 방학때 인턴도 하고\n취업 준비를 할 수 있어!";
            yield return new WaitForSeconds(2f);
            soundDirector.Play();

            // 해피엔딩 장면4 
            uiController.endingText.text = "정말 다행이야.. 고마워.. 나 자신!";
            yield return new WaitForSeconds(2f);
            uiController.endingText.text = "- HAPPY ENDING -";
        }
        else
        {
            // 세드엔딩 장면2 
            soundDirector.Play();
            uiController.endingText.text = "이걸론 등록금에 택도 없어..\n400만원은 있어야 해";
            yield return new WaitForSeconds(2f);

            // 해피엔딩 장면3 
            soundDirector.Play();
            uiController.endingText.text = "내 능력이 이거 밖에 안되다니..\n흑 난 안되는건가..";
            yield return new WaitForSeconds(2f);


            // 해피엔딩 장면4
            uiController.endingImages[0].SetActive(false);
            uiController.endingImages[1].SetActive(true);
            audioSource.pitch = 3;
            soundDirector.Play();
            uiController.endingText.text = "\"일어나라 서울여대 인이여..\"";
            yield return new WaitForSeconds(2f);


            // 해피엔딩 장면5
            soundDirector.Play();
            uiController.endingText.text = "\"서울여대인은 이정도에서 끝낼 수 없다..\"";
            yield return new WaitForSeconds(2f);


            // 해피엔딩 장면6
            audioSource.pitch = 1;
            soundDirector.Play();
            uiController.endingImages[0].SetActive(true);
            uiController.endingImages[1].SetActive(false);
            uiController.endingText.text = "헉.. 누구세요..?!";
            yield return new WaitForSeconds(2f);


            // 해피엔딩 장면7
            uiController.endingImages[0].SetActive(false);
            uiController.endingImages[1].SetActive(true);
            audioSource.pitch = 3;
            soundDirector.Play();
            uiController.endingText.text = "\"내가 다시 일주일 전으로 돌려주겠다..\"";
            yield return new WaitForSeconds(2f);


            // 해피엔딩 장면8
            uiController.endingImages[0].SetActive(true);
            uiController.endingImages[1].SetActive(false);
            audioSource.pitch = 1;
            soundDirector.Play();
            uiController.endingText.text = "헉.. 다시 이 개고생은..\n싫어...!";

            // 엔딩 UI 비활성화
            yield return new WaitForSeconds(2f);
            uiController.ending.SetActive(false);

            // 게임 정보 초기화
            StageDirector.playDate = 0;
            StageDirector.totalSale = 0;
            for(int i = 0; i < StageDirector.dateSale.Length; i++)
            {
                StageDirector.dateSale[i] = 0;
                StageDirector.dateBurgerSale[i] = 0;
            }

            // 스테이지씬으로 재시작
            startDirector.BackStage();
        }

        yield return null;
    }

}
