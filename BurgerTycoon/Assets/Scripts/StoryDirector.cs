using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoryDirector : MonoBehaviour
{
    //  스토리 출력 및 진행 기능 스크립트

    // 컴포넌트 불러오기
    UIController uiController;
    StartDirector startDirector;
    SoundDirector soundDirector;
    AudioSource audioSource;

    // 제어할 카메라 연결
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;

    // 스토리 정보
    public int lineNum = 0;             // 프롤로그인지 튜토리얼인지 대화 번호
    public int lineCount = 0;           // 현재 대화 인덱스
    public int[] prologueNum = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0 };
    public string[] prologue = {        // 프롤로그 대사 리스트
        "오늘은 학점 나오는 날..",
        "이번에는 장학금을 꼭 받아야 하는데..",
        "성적을 확인해볼까..",
        "이럴수가.. 장학금을 놓치다니..\n장학금을 어디서 구하지..?",
        "방학에 취업 준비해야돼서 바쁜데..!!\n어떡하지..",
        "하.. 일단 방학 전까지\n단기알바라도 뛰어볼까..",
        "-알바몽을 킨다-",
        "햄버거 가게 일주일 단기알바가\n올라와있네.. 지원해볼까?",
        "-햄버거 가게로 전화한다-",
        "여보세요옹 맛있는 햄버거 가게입니다^^!\n무슨 일이신가요옹?", // master 8
        "알바몽 보고 연락드렸습니다!\n아르바이트 지원할 수 있을까요?",
        "간단하게 인적사항 알 수 있을까요옹^^?", // master 10
        "네 저는 23살 대학교 다니고 있는\n김슈니입니다!",
        "학생이였구나^^\n어디 대학교 다니고 있는데요옹?", // master 12
        "현재 서울여자대학교 다니고 있습니다!",
        "호오 서울여대 학생이면\n믿고 맡길 수 있지요옹^^", // master 14
        "오늘부터 일 해줄 수 있어요?\n내가 급하게 여행을 가봐야 해서^^", // master 15
        "맡아줄 사람이 필요해요옹^^\n단골 손님들도 있고", // master 16
        "나는 돈 안벌어도 되니까^^\n가게만 맡아줘요옹", // master 17
        "그.. 재료비랑 제외하고^^\n순수익만 학생이 받아가는 걸로 해요옹^^", // master 18
        "헉..! 그래도 되나요..?\n저야 너무 좋죠!",
        "제 가게다 생각하고 열심히 일하겠습니다!!",
        "그래용 지금 가게오면 인수인계 해줄게요옹^^", // master 21
        "네 알겠습니다..!!",
        "(이건 기회야..!)"
    };

    public string[] tutorial = {         // 튜토리얼 대사 리스트
        "지금부터 햄버거 만드는 방법을 알려줄게요옹",
        "S와 F키 혹은 화살표 좌우 키를 누르면 좌우로 이동할 수 있어요옹",
        "햄버거 재료가 담긴 트레이들을 클릭하면",
        "가운데 도마로 재료가 떨어져요옹",
        "햄버거를 다 만들고 손님 앞에 있는 트레이를 클릭하면",
        "손님 앞으로 햄버거가 제공돼요옹",
        "주문서 키오스크에 있는 햄버거 레시피를 보고 햄버거를 만들면 돼요옹",
        "모든 손님에게 기본적으로 콜라와 감자튀김이 제공돼요옹",
        "콜라는 좌측에 있는 디스펜서를 클릭하면 만들 수 있어요옹",
        "만들어진 콜라를 클릭하면 손님 앞으로 제공돼요옹",
        "감자튀김은 우측의 감자튀김이 담긴 트레이를 클릭하면 튀길 수 있어요옹",
        "우측 튀김기에 있는 감자튀김을 클릭하면 감자튀김이 손님 앞으로 제공돼요옹",
        "햄버거와 콜라, 감자튀김을 모두 제공한 후 옆에 종을 치면 판매할 수 있어요옹",
        "햄버거의 레시피가 잘못되거나 콜라와 감자튀김을 제공하지 않았다면 돈을 받지 못해요옹",
        "또 LIFE도 하나 줄어들어요옹.. LIFE가 모두 없어지면 그날 영업은 종료되니 주의해요옹",
        "그럼 김슈니 양만 믿고 나는 이제 갈게요옹 화이팅^^!"
    };

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        uiController = GetComponent<UIController>();
        startDirector = GetComponent<StartDirector>();
        soundDirector = GetComponent<SoundDirector>();
        audioSource = GetComponent<AudioSource>();

        // 스토리 진행 시작하기
        StartCoroutine(ShowStory());
    }

    // Update is called once per frame
    void Update()
    {
        // 스페이스바 키를 누르면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (lineNum)
            {
                // 프롤로그면
                case 0:
                    if (lineCount < prologue.Length)
                    {
                        // 마지막 대화 아니면
                        // 다음 대사 실행
                        NextLine();
                    }
                    else
                    {
                        // 마지막 대사이면
                        // 튜토리얼 유형으로 갱신
                        lineNum = 1;

                        // 스토리 대화창 아래로 슬라이드 UI 효과
                        uiController.DownUI(uiController.storyUI, 1400f);

                        // 튜토리얼 출력 시작
                        StartCoroutine(ShowTutorial());
                    }
                    break;
                // 튜토리얼 이면
                case 1:
                    if (lineCount < tutorial.Length)
                    {
                        // 마지막 대화 아니면
                        // 다음 대사 실행
                        NextTutorial();
                    }
                    else
                    {
                        // 마지막 대사이면
                        // 스토리 대화창 UI 아래로 슬라이드 효과
                        uiController.DownUI(uiController.storyUI, 1400f);

                        // 스테이지로 돌아가기
                        startDirector.BackStage();
                    }
                    break;
            }


        }
    }

    /// <summary>
    /// 스토리 출력 메서드
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowStory()
    {
        // 스토리 정보 초기화
        lineCount = 0;
        lineNum = 0;

        // 장면 전환
        uiController.HideBlack();
        yield return new WaitForSeconds(1f);

        // UI 등장 효과
        uiController.UpUI(uiController.storyUI, 1400f);
        uiController.storyUI.SetActive(true);

        // 스토리 출력 목소리 효과음
        soundDirector.SetAudioClip(soundDirector.voiceSound);
        soundDirector.Play();

        // 다음 대사 실행
        NextLine();
        yield return null;
    }
    
    /// <summary>
    /// 튜토리얼 출력 메서드
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowTutorial()
    {
        // 스토리 정보 업데이트
        lineCount = 0;
        lineNum = 1;
        yield return new WaitForSeconds(1f);

        // UI 활성화, 장면 전환
        uiController.storyUI.SetActive(false);
        uiController.ShowBlack();
        yield return new WaitForSeconds(1f);

        // 카메라 전환
        camera2.SetActive(false);
        camera3.SetActive(true);

        // UI 효과
        uiController.HideBlack();
        yield return new WaitForSeconds(1f);
        uiController.UpUI(uiController.storyUI, 1400f);
        uiController.storyUI.SetActive(true);

        // 다음 대사 실행
        NextTutorial();
        yield return null;
    }

    /// <summary>
    /// 다음 대사 업데이트 메서드
    /// </summary>
    public void NextLine()
    {
        uiController.line.text = prologue[lineCount];
        switch (prologueNum[lineCount])
        {
            case 0:                                      // 플레이어가 말할 때
                audioSource.pitch = 1;                   // 피치 높이고 
                audioSource.volume = 0.5f;               // 효과음 크기 조절 
                uiController.player.SetActive(true);     // 플레이어 이미지 활성화 
                uiController.master.SetActive(false);     
                break;
            case 1:                                      // 버거 사장이 말할 때
                audioSource.pitch = 0.6f;                // 피치 낮추기
                audioSource.volume = 1f;                 // 효과음 크기 조절
                uiController.master.SetActive(true);     // 버거 사장 이미지 활성화
                uiController.player.SetActive(false);    
                break;
        }

        // 목소리 효과음 출력
        soundDirector.Play();

        // 현재 대화 인덱스 카운트
        lineCount++;
    }

    /// <summary>
    /// 다음 튜토리얼 대사 실행 메서드
    /// </summary>
    public void NextTutorial()
    {
        // 목소리 효과음 출력
        audioSource.pitch = 0.6f;
        soundDirector.Play();

        // 대사 출력
        uiController.line.text = tutorial[lineCount];

        // 이미지 활성화
        uiController.master.SetActive(true);
        uiController.player.SetActive(false);

        // 현재 대화 인덱스
        lineCount++;
    }
}
