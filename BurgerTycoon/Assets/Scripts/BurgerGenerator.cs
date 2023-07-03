using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerGenerator : MonoBehaviour
{
    // �ܹ��� ��� ������� �� ����, ���� ����

    // ������ ����
    public GameObject breadTopPrefab;
    public GameObject breadBottomPrefab;
    public GameObject lettucePrefab;
    public GameObject pattyPrefab;
    public GameObject cheesePrefab;
    public GameObject tomatoPrefab;
    public GameObject ingredientPrefab;

    // ������Ʈ �ҷ�����
    public GameObject gameDirector;
    public PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        gameDirector = GameObject.Find("GameDirector");
        playerController = gameDirector.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ��ũ��Ʈ ����� ������Ʈ Ŭ������ ��
    private void OnMouseDown()
    {
        // ���� �ܹ��� ���� �� ����̸� ����
        if (gameObject.tag == "Top")
        {
            // ���� ���� ���� ���� ����
            playerController.index--;
            playerController.makingList[playerController.index] = null;
            playerController.make[playerController.index] = 0;

            // �Ʒ� ��Ḧ ž���� ������ֱ�
            if(playerController.index > 0)
                playerController.makingList[playerController.index - 1].tag = "Top";

            // �Ҹ� ���
            gameDirector.GetComponent<AudioSource>().clip = gameDirector.GetComponent<SoundDirector>().deleteIngredientSound;
            gameDirector.GetComponent<AudioSource>().Play();

            // ����
            Destroy(gameObject);
        }
        else if (gameObject.tag == "Result")
        {
            // �մ� �� ��� Ʈ���̸� Ŭ���ϸ�
            // ���� ������� �����ִ� �޼��� ȣ��
            SendBurger();
        }
        else if (gameObject.name == "DoneBell")
        {
            // �Ϸ� ���� Ŭ���ϸ�

            // ȿ���� ���
            gameDirector.GetComponent<AudioSource>().clip = gameDirector.GetComponent<SoundDirector>().burgerResultBellSound;
            gameDirector.GetComponent<AudioSource>().Play();

            // ���� ������ ���� �޼��� ȣ��
            gameDirector.GetComponent<GameDirector>().JudgeBurger();
        }

        // ���� ���� ������ ��� ������ 9���� ������ (10������ ž�ױ� ����)
        if (playerController.index > 9)
        {
            // ��� Ʈ���̵��� Ŭ���� �� ������ �޼��� ����
            return;
        }

        // ��� Ʈ���̵��� Ŭ���ϴ� ���
        switch (gameObject.name)
        {
            // ����, �Ʒ���, �����, ��Ƽ, ġ��, �丶��, ���, ġŲ, �ʰ��� ��� �� Ʈ���̸� Ŭ���ϸ�
            // ���� ���� ���� ��� ����Ʈ�� ��� ������ �־��ֱ�
            // ��� ������ ��� �޼��� ȣ��
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
    /// ��� ���� �� ����ϴ� �޼���
    /// </summary>
    /// <param name="ingredient"></param>
    void DropIngredient(GameObject ingredient)
    {
        // ù ��� ��ġ ����
        Vector3 dropPos = new Vector3(-2.68f, 1.5f, 17.7f);

        // �ι�° �����ʹ� �Ʒ� ��Ẹ�� 0.3 ������ ��� ��ġ ����
        if(playerController.index > 0)
        {
            dropPos = new Vector3(-2.68f,
                        playerController.makingList[playerController.index - 1].transform.localPosition.y + 0.3f,
                        17.7f);
        }

        // ��� ������ ���� �� ���
        playerController.makingList[playerController.index] = Instantiate(ingredient, dropPos, ingredient.transform.rotation);

        // ��� ȿ���� ���
        gameDirector.GetComponent<AudioSource>().clip = gameDirector.GetComponent<SoundDirector>().dropIngredientSound;
        gameDirector.GetComponent<AudioSource>().Play();

        // ���� ���� ����� ���� ���� �� ��� ž �±� ����
        playerController.makingList[playerController.index].tag = "Top";
        if(playerController.index > 0)
        {
            playerController.makingList[playerController.index - 1].tag = "Ingredient";
        }

        // ��� ���� ī��Ʈ
        playerController.index++;
    }

    /// <summary>
    /// ������ ���Ÿ� �մ� �� Ʈ���̷� �̵��ϴ� �޼���
    /// </summary>
    void SendBurger()
    {
        // ���� �ִ� ������ŭ ����
        for(int i = 0; i < playerController.make.Length; i++)
        {
            // ���� ��ᰡ ������ �޼��� ����
            if (playerController.makingList[i] == null)
            {
                return;
            }

            // ��� ��� ��ġ ����
            Vector3 dropPos = new Vector3(-2.718f, 1.5f, 15.95f);

            // �ι�° �����ʹ� �Ʒ� ��Ẹ�� 0.3 ������ ��� ��ġ ����
            if (playerController.index > 0)
            {
                dropPos = new Vector3(-2.718f,
                            playerController.makingList[playerController.index - 1].transform.localPosition.y + 0.3f,
                            15.95f);
            }

            // ��� ����
            playerController.makingList[i].transform.position = dropPos;
        }


    }


}
