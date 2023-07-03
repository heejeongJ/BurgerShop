using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderGenerator : MonoBehaviour
{
    // 주문서 생성 스크립트

    // 컴포넌트
    UIController controller;
    
    // 동작 정보

    // 주문 중인지 체크
    public bool isOrder;                                // 주문 중인지 true: 주문 후 제조 중, false: 제조 완료 했을 때

    // 주문 정보 설정
    public static int maxBurgerIngredient = 10;         // 최대 재료 개수
    public int breadPrice = 600;   // 빵 가격
    public int pattyPrice = 2000;  // 패티 가격
    public int tomatoPrice = 800;  // 토마토 가격
    public int lettucePrice = 700; // 양상추 가격
    public int cheesePrice = 1000; // 치즈 가격
    public int eggPrice = 1300;            // 계란 가격
    public int chickenPrice = 2500;   // 치킨패티 가격
    public int nuggetPrice = 3000;         // 너겟 가격

    // 주문 정보
    public int[] order = new int[maxBurgerIngredient];  // 주문 내용 데이터
    public string orderString;     // 주문 내용 문자열
    public int orderPrice;         // 주문 가격
    public int bread;              // 판매한 빵 수
    public int patty;              // 판매한 패티 수
    public int tomato;             // 판매한 토마토 수
    public int lettuce;            // 판매한 양상추 수
    public int cheese;             // 판매한 치즈 수
    public int egg;                // 판매한 계란 수
    public int chicken;            // 판매한 치킨패티 수
    public int nugget;             // 판매한 너겟 수

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        controller = GetComponent<UIController>();

        // 주문 상태 초기화
        isOrder = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// 손님 주문 생성 메서드
    /// </summary>
    public void GenerateOrder()
    {
        // 처음과 끝은 무조건 빵
        // 주문서 초기화
        for(int i = 0; i < order.Length; i++)
        {
            order[i] = 0;
        }
        orderString = "";

        // 재료 개수 초기화
        bread = 2;
        patty = 0;
        tomato = 0;
        lettuce = 0;
        cheese = 0;
        egg = 0;
        chicken = 0;
        nugget = 0;

        // 가격 초기화
        orderPrice = 0;

        // 주문 생성
        int count = (int)Random.Range(3, maxBurgerIngredient + 1); // 재료 개수 랜덤 할당

        order[0] = 0;              // 주문 첫 데이터 빵 바닥
        order[count - 1] = 5;      // 주문 마지막 데이터 빵 뚜껑

        // 재료 할당
        for (int i = 1; i < count - 1; i++)
        {
            // 0: 빵 바닥, 1: 패티, 2: 토마토, 3: 상추, 4: 치즈, 5: 빵 뚜껑
            // 주문 배열에 재료 랜덤 할당
            // 날짜별로 재료 범위 지정
            if (StageDirector.playDate == 2)
                order[i] = (int)Random.Range(0, 6);   // 1, 2일차는 빵~치즈
            else if(StageDirector.playDate == 3)
                order[i] = (int)Random.Range(0, 7);   // 3일차 계란 추가
            else if(StageDirector.playDate >= 4)
                order[i] = (int)Random.Range(0, 8);   // 4일차 치킨패티 추가
            else
                order[i] = (int)Random.Range(0, 5);   // 5일차 너겟 추가

            // 각 재료 개수 카운트
            switch (order[i])                      
            {
                case 0:
                    bread += 1;
                    break;
                case 1:
                    patty += 1;
                    break;
                case 2:
                    tomato += 1;
                    break;
                case 3:
                    lettuce += 1;
                    break;
                case 4:
                    cheese += 1;
                    break;
                case 5:
                    egg += 1;
                    break;
                case 6:
                    chicken += 1;
                    break;
                case 7:
                    nugget += 1;
                    break;
            }
        }

        // 주문 문자열 할당
        orderString += "빵 뚜껑\n";
        for (int i = count - 2; i > 0; i--)        // i : count-2 ~ 1
        {
            // 0: 빵 바닥, 1: 패티, 2: 토마토, 3: 상추, 4: 치즈, 5: 빵 뚜껑
            switch (order[i])                      // 각 재료 개수 카운트
            {
                case 0:
                    bread += 1;
                    orderString += "빵 바닥\n";
                    break;
                case 1:
                    patty += 1;
                    orderString += "패티\n";
                    break;
                case 2:
                    tomato += 1;
                    orderString += "토마토\n";
                    break;
                case 3:
                    lettuce += 1;
                    orderString += "양상추\n";
                    break;
                case 4:
                    cheese += 1;
                    orderString += "치즈\n";
                    break;
                case 5:
                    egg += 1;
                    orderString += "계란\n";
                    break;
                case 6:
                    chicken += 1;
                    orderString += "치킨패티\n";
                    break;
                case 7:
                    nugget += 1;
                    orderString += "너겟\n";
                    break;
            }
        }
        orderString += "빵 바닥\n";

        // 주문 가격 카운트
        CalculatePrice();

        // 주문 출력
        ShowOrder();

        isOrder = true;              // 상태 변경 : 주문 중
    }

    // 주문 가격 계산 메서드
    public int CalculatePrice()
    {
        orderPrice = 2000 + // 세트 값
            bread * breadPrice + 
            patty * pattyPrice +
            tomato * tomatoPrice +
            lettuce * lettucePrice +
            cheese * cheesePrice + 
            egg * eggPrice +
            chicken * chickenPrice +
            nugget * nuggetPrice;
        return orderPrice;
    }

    /// <summary>
    /// 주문 재료 개수 카운트 메서드
    /// </summary>
    /// <returns></returns>
    public int[] GetIngredientCount()
    {
        int[] ingredientCount = { 0, 0, 0, 0, 0, 0, 0, 0 };
        ingredientCount[0] = bread;
        ingredientCount[1] = patty;
        ingredientCount[2] = tomato;
        ingredientCount[3] = lettuce;
        ingredientCount[4] = cheese;
        ingredientCount[5] = egg;
        ingredientCount[6] = chicken;
        ingredientCount[7] = nugget;

        return ingredientCount;
    }

    /// <summary>
    /// 주문서 출력 메서드
    /// </summary>
    void ShowOrder()
    {
        controller.orderSheet.text = orderString;
        controller.orderPriceSheet.text = orderPrice.ToString() + "원";
    }

}
