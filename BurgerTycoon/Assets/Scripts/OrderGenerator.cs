using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderGenerator : MonoBehaviour
{
    // �ֹ��� ���� ��ũ��Ʈ

    // ������Ʈ
    UIController controller;
    
    // ���� ����

    // �ֹ� ������ üũ
    public bool isOrder;                                // �ֹ� ������ true: �ֹ� �� ���� ��, false: ���� �Ϸ� ���� ��

    // �ֹ� ���� ����
    public static int maxBurgerIngredient = 10;         // �ִ� ��� ����
    public int breadPrice = 600;   // �� ����
    public int pattyPrice = 2000;  // ��Ƽ ����
    public int tomatoPrice = 800;  // �丶�� ����
    public int lettucePrice = 700; // ����� ����
    public int cheesePrice = 1000; // ġ�� ����
    public int eggPrice = 1300;            // ��� ����
    public int chickenPrice = 2500;   // ġŲ��Ƽ ����
    public int nuggetPrice = 3000;         // �ʰ� ����

    // �ֹ� ����
    public int[] order = new int[maxBurgerIngredient];  // �ֹ� ���� ������
    public string orderString;     // �ֹ� ���� ���ڿ�
    public int orderPrice;         // �ֹ� ����
    public int bread;              // �Ǹ��� �� ��
    public int patty;              // �Ǹ��� ��Ƽ ��
    public int tomato;             // �Ǹ��� �丶�� ��
    public int lettuce;            // �Ǹ��� ����� ��
    public int cheese;             // �Ǹ��� ġ�� ��
    public int egg;                // �Ǹ��� ��� ��
    public int chicken;            // �Ǹ��� ġŲ��Ƽ ��
    public int nugget;             // �Ǹ��� �ʰ� ��

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        controller = GetComponent<UIController>();

        // �ֹ� ���� �ʱ�ȭ
        isOrder = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// �մ� �ֹ� ���� �޼���
    /// </summary>
    public void GenerateOrder()
    {
        // ó���� ���� ������ ��
        // �ֹ��� �ʱ�ȭ
        for(int i = 0; i < order.Length; i++)
        {
            order[i] = 0;
        }
        orderString = "";

        // ��� ���� �ʱ�ȭ
        bread = 2;
        patty = 0;
        tomato = 0;
        lettuce = 0;
        cheese = 0;
        egg = 0;
        chicken = 0;
        nugget = 0;

        // ���� �ʱ�ȭ
        orderPrice = 0;

        // �ֹ� ����
        int count = (int)Random.Range(3, maxBurgerIngredient + 1); // ��� ���� ���� �Ҵ�

        order[0] = 0;              // �ֹ� ù ������ �� �ٴ�
        order[count - 1] = 5;      // �ֹ� ������ ������ �� �Ѳ�

        // ��� �Ҵ�
        for (int i = 1; i < count - 1; i++)
        {
            // 0: �� �ٴ�, 1: ��Ƽ, 2: �丶��, 3: ����, 4: ġ��, 5: �� �Ѳ�
            // �ֹ� �迭�� ��� ���� �Ҵ�
            // ��¥���� ��� ���� ����
            if (StageDirector.playDate == 2)
                order[i] = (int)Random.Range(0, 6);   // 1, 2������ ��~ġ��
            else if(StageDirector.playDate == 3)
                order[i] = (int)Random.Range(0, 7);   // 3���� ��� �߰�
            else if(StageDirector.playDate >= 4)
                order[i] = (int)Random.Range(0, 8);   // 4���� ġŲ��Ƽ �߰�
            else
                order[i] = (int)Random.Range(0, 5);   // 5���� �ʰ� �߰�

            // �� ��� ���� ī��Ʈ
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

        // �ֹ� ���ڿ� �Ҵ�
        orderString += "�� �Ѳ�\n";
        for (int i = count - 2; i > 0; i--)        // i : count-2 ~ 1
        {
            // 0: �� �ٴ�, 1: ��Ƽ, 2: �丶��, 3: ����, 4: ġ��, 5: �� �Ѳ�
            switch (order[i])                      // �� ��� ���� ī��Ʈ
            {
                case 0:
                    bread += 1;
                    orderString += "�� �ٴ�\n";
                    break;
                case 1:
                    patty += 1;
                    orderString += "��Ƽ\n";
                    break;
                case 2:
                    tomato += 1;
                    orderString += "�丶��\n";
                    break;
                case 3:
                    lettuce += 1;
                    orderString += "�����\n";
                    break;
                case 4:
                    cheese += 1;
                    orderString += "ġ��\n";
                    break;
                case 5:
                    egg += 1;
                    orderString += "���\n";
                    break;
                case 6:
                    chicken += 1;
                    orderString += "ġŲ��Ƽ\n";
                    break;
                case 7:
                    nugget += 1;
                    orderString += "�ʰ�\n";
                    break;
            }
        }
        orderString += "�� �ٴ�\n";

        // �ֹ� ���� ī��Ʈ
        CalculatePrice();

        // �ֹ� ���
        ShowOrder();

        isOrder = true;              // ���� ���� : �ֹ� ��
    }

    // �ֹ� ���� ��� �޼���
    public int CalculatePrice()
    {
        orderPrice = 2000 + // ��Ʈ ��
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
    /// �ֹ� ��� ���� ī��Ʈ �޼���
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
    /// �ֹ��� ��� �޼���
    /// </summary>
    void ShowOrder()
    {
        controller.orderSheet.text = orderString;
        controller.orderPriceSheet.text = orderPrice.ToString() + "��";
    }

}
