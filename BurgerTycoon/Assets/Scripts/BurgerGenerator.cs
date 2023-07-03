using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerGenerator : MonoBehaviour
{
    // 햄버거 재료 생성드랍 및 동작, 정보 갱신

    // 프리팹 연결
    public GameObject breadTopPrefab;
    public GameObject breadBottomPrefab;
    public GameObject lettucePrefab;
    public GameObject pattyPrefab;
    public GameObject cheesePrefab;
    public GameObject tomatoPrefab;
    public GameObject ingredientPrefab;

    // 컴포넌트 불러오기
    public GameObject gameDirector;
    public PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        gameDirector = GameObject.Find("GameDirector");
        playerController = gameDirector.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 스크립트 적용된 오브젝트 클릭했을 때
    private void OnMouseDown()
    {
        // 제조 햄버거 가장 윗 재료이면 삭제
        if (gameObject.tag == "Top")
        {
            // 제작 중인 버거 정보 갱신
            playerController.index--;
            playerController.makingList[playerController.index] = null;
            playerController.make[playerController.index] = 0;

            // 아래 재료를 탑으로 만들어주기
            if(playerController.index > 0)
                playerController.makingList[playerController.index - 1].tag = "Top";

            // 소리 출력
            gameDirector.GetComponent<AudioSource>().clip = gameDirector.GetComponent<SoundDirector>().deleteIngredientSound;
            gameDirector.GetComponent<AudioSource>().Play();

            // 삭제
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Result")
        {
            // 손님 앞 쟁반 트레이를 클릭하면
            // 버거 쟁반으로 보내주는 메서드 호출
            SendBurger();
        }
        else if (gameObject.name == "DoneBell")
        {
            // 완료 종을 클릭하면

            // 효과음 출력
            gameDirector.GetComponent<AudioSource>().clip = gameDirector.GetComponent<SoundDirector>().burgerResultBellSound;
            gameDirector.GetComponent<AudioSource>().Play();

            // 버거 레시피 판정 메서드 호출
            gameDirector.GetComponent<GameDirector>().JudgeBurger();
        }

        // 제조 중인 버거의 재료 개수가 9개를 넘으면 (10개까지 탑쌓기 가능)
        if (playerController.index > 9)
        {
            // 재료 트레이들을 클릭할 수 없도록 메서드 종료
            return;
        }

        // 재료 트레이들을 클릭하는 경우
        switch (gameObject.name)
        {
            // 윗빵, 아랫빵, 양상추, 패티, 치즈, 토마토, 계란, 치킨, 너겟이 담긴 각 트레이를 클릭하면
            // 제조 중인 버거 재료 리스트에 재료 정보값 넣어주기
            // 재료 프리팹 드랍 메서드 호출
            case "Tray_breadTop":
                playerController.make[playerController.index] = 5;
                DropIngredient(ingredientPrefab);
                break;
            case "Tray_breadBottom":
                playerController.make[playerController.index] = 0;
                DropIngredient(ingredientPrefab);
                break;
            case "Tray_lettuce":
                playerController.make[playerController.index] = 3;
                DropIngredient(ingredientPrefab);
                break;
            case "Tray_patty":
                playerController.make[playerController.index] = 1;
                DropIngredient(ingredientPrefab);
                break;
            case "Tray_cheese":
                playerController.make[playerController.index] = 4;
                DropIngredient(ingredientPrefab);
                break;
            case "Tray_tomato":
                playerController.make[playerController.index] = 2;
                DropIngredient(ingredientPrefab);
                break;
            case "Tray_egg":
                playerController.make[playerController.index] = 5;
                DropIngredient(ingredientPrefab);
                break;
            case "Tray_chicken":
                playerController.make[playerController.index] = 6;
                DropIngredient(ingredientPrefab);
                break;
            case "Tray_nugget":
                playerController.make[playerController.index] = 7;
                DropIngredient(ingredientPrefab);
                break;
        }
    }

    /// <summary>
    /// 재료 생성 및 드랍하는 메서드
    /// </summary>
    /// <param name="ingredient"></param>
    void DropIngredient(GameObject ingredient)
    {
        // 첫 드랍 위치 지정
        Vector3 dropPos = new Vector3(-2.68f, 1.5f, 17.7f);

        // 두번째 재료부터는 아래 재료보다 0.3 위에서 드랍 위치 지정
        if(playerController.index > 0)
        {
            dropPos = new Vector3(-2.68f,
                        playerController.makingList[playerController.index - 1].transform.localPosition.y + 0.3f,
                        17.7f);
        }

        // 재료 프리팹 생성 및 드랍
        playerController.makingList[playerController.index] = Instantiate(ingredient, dropPos, ingredient.transform.rotation);

        // 드랍 효과음 출력
        gameDirector.GetComponent<AudioSource>().clip = gameDirector.GetComponent<SoundDirector>().dropIngredientSound;
        gameDirector.GetComponent<AudioSource>().Play();

        // 추후 삭제 기능을 위한 가장 윗 재료 탑 태그 지정
        playerController.makingList[playerController.index].tag = "Top";
        if(playerController.index > 0)
        {
            playerController.makingList[playerController.index - 1].tag = "Ingredient";
        }

        // 재료 개수 카운트
        playerController.index++;
    }

    /// <summary>
    /// 제조한 버거를 손님 앞 트레이로 이동하는 메서드
    /// </summary>
    void SendBurger()
    {
        // 버거 최대 개수만큼 실행
        for(int i = 0; i < playerController.make.Length; i++)
        {
            // 버거 재료가 없으면 메서드 종료
            if (playerController.makingList[i] == null)
            {
                return;
            }

            // 쟁반 드랍 위치 지정
            Vector3 dropPos = new Vector3(-2.718f, 1.5f, 15.95f);

            // 두번째 재료부터는 아래 재료보다 0.3 위에서 드랍 위치 지정
            if (playerController.index > 0)
            {
                dropPos = new Vector3(-2.718f,
                            playerController.makingList[playerController.index - 1].transform.localPosition.y + 0.3f,
                            15.95f);
            }

            // 드랍 실행
            playerController.makingList[i].transform.position = dropPos;
        }


    }


}
