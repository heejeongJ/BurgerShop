using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideGenerator : MonoBehaviour
{
    // ���̵� �޴� ������� �� ����, ���� ���� ��ũ��Ʈ

    // ������Ʈ
    public SoundDirector soundDirector;
    public GameObject gameDirector;
    public PlayerController playerController;

    // ������
    public GameObject cokePrefab;
    public GameObject friesPrefab;
    public GameObject packedCokePrefab;
    public GameObject packedFriesPrefab;

    // ���� üũ, �ݶ� ���� �ƴ���, ����Ƣ�� ���� �ƴ���
    public bool isCoke;
    public bool isFries;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        gameDirector = GameObject.Find("GameDirector");
        soundDirector = gameDirector.GetComponent<SoundDirector>();
        playerController = gameDirector.GetComponent<PlayerController>();

        // �ݶ�, ����Ƣ�� ������ �������ڸ��� ���� ȿ���� ���
        if (gameObject.name == "CokePrefab(Clone)")
        {
            soundDirector.SetAudioClip(soundDirector.cokeSound);
            soundDirector.Play();
        }
        else if(gameObject.name == "FriesPrefab(Clone)")
        {
            soundDirector.SetAudioClip(soundDirector.friesSound);
            soundDirector.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ���̵� ���� ������Ʈ Ŭ�� �޼���
    /// </summary>
    private void OnMouseDown()
    {
        
        if(gameObject.name == "CokeMachine")   // �ݶ�ӽ� Ŭ��
        {
            // �ݶ� �̹� ������ �޼��� ����
            if (isCoke) return;

            // �ݶ� ����
            GameObject coke = Instantiate(cokePrefab);
            coke.transform.position = new Vector3(1.301f, 1.539f, 17.604f);

            // �ݶ� ���� ���·� ����
            isCoke = true;
        }
        else if(gameObject.name == "Tray_FF")  // ����Ƣ�� Ʈ���� Ŭ��
        {
            // ����Ƣ�� �̹� ������ �޼��� ����
            if (isFries) return;

            // �ݶ� ����
            GameObject fries = Instantiate(friesPrefab);
            fries.transform.position = new Vector3(-6.66f, 1.64f, 17.797f);

            // ����Ƣ�� ���� ���·� ����
            isFries = true;
        }
        else if(gameObject.name == "CokePrefab(Clone)")   // �ݶ� �ӽ� ���� �ִ� ������ �ݶ� Ŭ��
        {
            // ����� �ݶ� ���� �� ���� ��ġ�� ����
            GameObject sendCoke = Instantiate(packedCokePrefab);
            sendCoke.transform.position = new Vector3(-2.325f, 1.561f, 15.837f);

            // �ݶ� ���� ���·� ����
            playerController.isCoke = true;

            // �ݶ� �̻��� ���·� ����
            GameObject.Find("CokeMachine").GetComponent<SideGenerator>().isCoke = false;

            // ȿ���� ����
            soundDirector.Stop();

            // ����ȵ� �ݶ� ������Ʈ ����
            Destroy(gameObject);
        }
        else if(gameObject.name == "FriesPrefab(Clone)")   // Ƣ��⿡ ������ ����Ƣ�� Ŭ��
        {
            // ����� ����Ƣ�� ���� �� ���� ��ġ�� ����
            GameObject sendCoke = Instantiate(packedFriesPrefab);
            sendCoke.transform.position = new Vector3(-3.113f, 1.554f, 15.837f);

            // ����Ƣ�� ���� ���·� ����
            playerController.isFires = true;

            // ����Ƣ�� �̻��� ���·� ����
            GameObject.Find("Tray_FF").GetComponent<SideGenerator>().isFries = false;

            // ȿ���� ����
            soundDirector.Stop();

            // ����ȵ� ����Ƣ�� ������Ʈ ����
            Destroy(gameObject);
        }
    }
}
