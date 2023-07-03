using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviour
{
    // ���ΰ��� ���� ������
    public bool isGame;                 // ���� �����ߴ���
    public bool isJudge;                // �����ߴ���
    public int life;                    // ���� ���� �� - heart
    public int totalMoney;              // �� ��

    // timer ���� �� ���� �뵵
    private float curTime;                  // ���� �ð� tIme

    // ����                                 
    public int soldBurger;                  // �Ǹ� ���� ����
    public int[] soldIndgredient;           // �Ǹ� ��� ����

    public GameObject customerPrefab;            // �մ� ������
    public GameObject customer;                  // ������ �մ� ������Ʈ

    // ������Ʈ �ҷ�����
    public OrderGenerator orderGenerator;
    public PlayerController playerController;
    public SoundDirector soundDirector;
    public UIController uiController;
    public StartDirector sceneDirector;

    // ������ ��� ������Ʈ
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
        // ������Ʈ �ҷ�����
        orderGenerator = GetComponent<OrderGenerator>();
        playerController = GetComponent<PlayerController>();
        soundDirector = GetComponent<SoundDirector>();
        uiController = GetComponent<UIController>();

        // ���� ���� �ʱ�ȭ
        isGame = false;
        life = 3;
        totalMoney = 0;
        curTime = 70;
        soldBurger = 0;
        soldIndgredient = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0};

        // LIFE �ʱ�ȭ
        uiController.lifeUI[0].SetActive(true);
        uiController.lifeUI[1].SetActive(true);
        uiController.lifeUI[2].SetActive(true);

        // ���� ����
        uiController.HideBlack();
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �����ϸ� Ÿ�̸� ����
        if (isGame)
        {
            Timer();
        }
    }

    /// <summary>
    /// ������ �� ī��Ʈ ����ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    IEnumerator StartCount()
    {
        // ������ �� ī��Ʈ ���
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
    /// ���� �����ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);

        // ���� ����
        isGame = true;

        // ���� ���� Ȱ��ȭ
        friesTray.SetActive(true);
        breadBottomTray.SetActive(true);
        breadTopTray.SetActive(true);
        pattyTray.SetActive(true);
        cheeseTray.SetActive(true);
        lettuceTray.SetActive(true);
        tomatoTray.SetActive(true);

        // 3���� ��� ��� �ر�
        if(StageDirector.playDate >= 2)
        {
            eggTray.SetActive(true);
        }

        // 4���� ġŲ��Ƽ ��� �ر�
        if (StageDirector.playDate >= 3)
        {
            chickenTray.SetActive(true);
        }

        // 5���� �ʰ� ��� �ر�
        if (StageDirector.playDate >= 4)
        {
            nuggetTray.SetActive(true);
        }

        // �մ� ����
        StartCoroutine(GenerateCustomer());
    }

    /// <summary>
    /// �մ� ���� �޼���
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateCustomer()
    {
        // ���� ���� �ƴϸ� �޼��� ����
        if (!isGame)
        {
            yield return null;
        }

        // 1�� �Ŀ�
        yield return new WaitForSeconds(1);

        // ���� ���� ����
        isJudge = false;

        // ���� ȿ����
        soundDirector.SetAudioClip(soundDirector.customerComingSound);
        soundDirector.Play();

        // �մ� ������Ʈ ����
        Color customerColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        customer = Instantiate(customerPrefab);

        // �մ� �÷� ���� ����
        MeshRenderer[] customerMeterial = new MeshRenderer[2];
        customerMeterial = customer.GetComponentsInChildren<MeshRenderer>();
        customerMeterial[0].material.color = customerMeterial[1].material.color = customerColor;

        // �ֹ� ����
        yield return new WaitForSeconds(2);
        orderGenerator.GenerateOrder();
    }

    /// <summary>
    /// LIFE ���� �Լ�
    /// </summary>
    void DecreaseLife()
    {
        // ��ġ ����
        life--;

        // UI ��Ȱ��ȭ
        if (life == 2)
            uiController.lifeUI[2].SetActive(false);
        else if(life == 1)
            uiController.lifeUI[1].SetActive(false);
        else if(life == 0)
            uiController.lifeUI[0].SetActive(false);

        if (isGame && life <= 0)
        {
            // LIFE 0�̸� ���� ����
            // �ѹ��� ���� ���� ���� ������ üũ
            EndGame();
        }
    }


    /// <summary>
    /// Ÿ�̸� ���� �޼���
    /// </summary>
    void Timer()
    {
        // �ð��� ������
        if (curTime > 0)
        {
            // �ð� ����
            curTime -= Time.deltaTime;

            // �ð�UI ���
            uiController.timeText.text = Mathf.Ceil(curTime).ToString();
            uiController.timeText.text = string.Format("{00:N2}", curTime);
        }
        else
        {
            // Ÿ�� ����
            curTime = 0;

            // �ð�UI ���
            uiController.timeText.text = string.Format("{00:N2}", curTime);

            // Ÿ�̸� ������ ���� ����
            if(isGame)
                EndGame();
        }
    }


    /// <summary>
    /// �ܹ��� ������ ���� �޼���
    /// </summary>
    public void JudgeBurger()
    {
        // �մ� ������ �޼��� ����
        if(!customer || isJudge)
        {
            return;
        }

        // ���� �մ� ������ ǥ��
        isJudge = true;

        // ���� ����
        bool isCorrect = false;
        uiController.orderSheet.text = "";

        if (orderGenerator.order.Length == playerController.make.Length)
        {
            for (int i = 0; i < orderGenerator.order.Length; i++)
            {
                if (orderGenerator.order[i] != playerController.make[i])
                {
                    // �ϳ��� �����ǰ� �߸��� ���

                    // �ֹ��� ���
                    uiController.orderSheet.text = "�ܹ��� �����ǰ�\n�߸��Ǿ��ݾƿ�-_-!\n";
                    uiController.orderPriceSheet.text = " ȹ�� ����!";

                    // ������ ����
                    DecreaseLife();

                    // �մ� ����
                    StartCoroutine(DestroyCustomer());

                    // �޼��� ����
                    return;
                }
            }

            // ������ �������� ����
            isCorrect = true;
        }
        else
        {
            // �ֹ� �����ǿ� ���� ������ ����Ʈ ���� �ȸ´� ��� ���� ����
            uiController.orderSheet.text = "�ܹ��� �����ǰ�\n�߸��Ǿ��ݾƿ�-_-!\n";
        }

        // �ݶ� ������ �� �ֹ��� ���
        if (!playerController.isCoke)
        {
            uiController.orderSheet.text += "�ݶ�¿�!\n";
        }

        // ����Ƣ�� ������ �� �ֹ��� ���
        if (!playerController.isFires)
        {
            uiController.orderSheet.text += "����Ƣ������!\n";
        }

        // �ܹ���, �ݶ�, ����Ƣ�� ��� ������ ���
        if (isCorrect && playerController.isCoke && playerController.isFires)
        {
            // ���� �Ǹ�
            SaleBurger();

            // �ֹ��� ���
            uiController.orderSheet.text = "�� �԰ڽ��ϴ� ��_��!";
            uiController.orderPriceSheet.text += " ȹ��!";
        }
        else
        {
            // �ϳ��� Ʋ�� ���
            //������ ����
            DecreaseLife();

            // UI ���
            uiController.orderPriceSheet.text = " ȹ�� ����!";
        }

        // �մ� ����
        StartCoroutine(DestroyCustomer());
    }

    /// <summary>
    /// ���� �Ǹ� �޼���
    /// </summary>
    void SaleBurger()
    {
        // ���� ���� ī��Ʈ
        soldBurger++;                                                        // ���� ���� ����

        // ���� ����
        totalMoney += orderGenerator.orderPrice;                             // �� ���

        // UI ����
        uiController.moneyText.text = totalMoney.ToString();

        // ��� ���� ī��Ʈ
        int[] ingredientCount = orderGenerator.GetIngredientCount();      // ��� ���� ����

        // ���� ���� ����
        for(int i = 0; i < soldIndgredient.Length; i++)
        {
            soldIndgredient[i] += ingredientCount[i];
        }

    }

    /// <summary>
    /// �մ� ���� �޼���
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyCustomer()
    {
        // ���� 2�� ��
        yield return new WaitForSeconds(2);

        // �մ� ������Ʈ ����
        Destroy(customer);

        // �ֹ��� �ʱ�ȭ
        uiController.orderSheet.text = "";
        uiController.orderPriceSheet.text = "";

        // ���� �ʱ�ȭ
        customer = null;

        // ���� ���� ����Ʈ �� �ݶ�, ����Ƣ�� ���� �ʱ�ȭ
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

        // ���ο� �մ� ����
        StartCoroutine(GenerateCustomer());

    }

    /// <summary>
    /// ���� ������
    /// </summary>
    void EndGame()
    {
        // ���� ���߱�
        isGame = false;

        // �������� ���� ������Ʈ

        // ���ϸ���
        StageDirector.dateSale[StageDirector.playDate] = totalMoney * 10;

        // ���� �Ǹ� ����
        StageDirector.dateBurgerSale[StageDirector.playDate] = soldBurger;

        // ���� ����
        StageDirector.totalSale += totalMoney * 10;

        // ��¥ ī��Ʈ
        StageDirector.playDate++;



        // ���꼭 ����
        // ȿ���� ���
        soundDirector.SetAudioClip(soundDirector.billSound);
        soundDirector.Play();

        // ���꼭 Ȱ��ȭ
        uiController.resultUI.SetActive(true);

        // ���� �����̵� ȿ��
        uiController.UpUI(uiController.resultUI, 1400f);

        // ���� ���� �� ���
        GameObject.Find("content").GetComponent<Text>().text = "�ܹ���\n\n��\n��Ƽ\n�丶��\n�����\nġ��\n";
        GameObject.Find("contentValue").GetComponent<Text>().text = soldBurger.ToString() + "��\n\n" +
            soldIndgredient[0] + "��\n" +
            soldIndgredient[1] + "��\n" +
            soldIndgredient[2] + "��\n" +
            soldIndgredient[3] + "��\n" +
            soldIndgredient[4] + "��\n";

        // 3���� �̻� ��� ��� ���
        if (StageDirector.playDate >= 3)
        {
            GameObject.Find("content").GetComponent<Text>().text += "���\n";
            GameObject.Find("contentValue").GetComponent<Text>().text += soldIndgredient[5] + "��\n";
        }

        // 4���� �̻� ġŲ ��� ���
        if (StageDirector.playDate >= 4)
        {
            GameObject.Find("content").GetComponent<Text>().text += "ġŲ ��Ƽ\n";
            GameObject.Find("contentValue").GetComponent<Text>().text += soldIndgredient[6] + "��\n";
        }

        // 5���� �̻� �ʰ� ��� ���
        if (StageDirector.playDate >= 5)
        {
            GameObject.Find("content").GetComponent<Text>().text += "�ʰ�\n";
            GameObject.Find("contentValue").GetComponent<Text>().text += soldIndgredient[7] + "��\n";
        }

        GameObject.Find("moneyValue").GetComponent<Text>().text = totalMoney.ToString() + "��";
    }
}