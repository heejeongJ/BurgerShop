using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoryDirector : MonoBehaviour
{
    //  ���丮 ��� �� ���� ��� ��ũ��Ʈ

    // ������Ʈ �ҷ�����
    UIController uiController;
    StartDirector startDirector;
    SoundDirector soundDirector;
    AudioSource audioSource;

    // ������ ī�޶� ����
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;

    // ���丮 ����
    public int lineNum = 0;             // ���ѷα����� Ʃ�丮������ ��ȭ ��ȣ
    public int lineCount = 0;           // ���� ��ȭ �ε���
    public int[] prologueNum = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0 };
    public string[] prologue = {        // ���ѷα� ��� ����Ʈ
        "������ ���� ������ ��..",
        "�̹����� ���б��� �� �޾ƾ� �ϴµ�..",
        "������ Ȯ���غ���..",
        "�̷�����.. ���б��� ��ġ�ٴ�..\n���б��� ��� ������..?",
        "���п� ��� �غ��ؾߵż� �ٻ۵�..!!\n�����..",
        "��.. �ϴ� ���� ������\n�ܱ�˹ٶ� �پ��..",
        "-�˹ٸ��� Ų��-",
        "�ܹ��� ���� ������ �ܱ�˹ٰ�\n�ö���ֳ�.. �����غ���?",
        "-�ܹ��� ���Է� ��ȭ�Ѵ�-",
        "��������� ���ִ� �ܹ��� �����Դϴ�^^!\n���� ���̽Ű����?", // master 8
        "�˹ٸ� ���� ������Ƚ��ϴ�!\n�Ƹ�����Ʈ ������ �� �������?",
        "�����ϰ� �������� �� �� ��������^^?", // master 10
        "�� ���� 23�� ���б� �ٴϰ� �ִ�\n�轴���Դϴ�!",
        "�л��̿�����^^\n��� ���б� �ٴϰ� �ִµ����?", // master 12
        "���� ���￩�ڴ��б� �ٴϰ� �ֽ��ϴ�!",
        "ȣ�� ���￩�� �л��̸�\n�ϰ� �ñ� �� �������^^", // master 14
        "���ú��� �� ���� �� �־��?\n���� ���ϰ� ������ ������ �ؼ�^^", // master 15
        "�þ��� ����� �ʿ��ؿ��^^\n�ܰ� �մԵ鵵 �ְ�", // master 16
        "���� �� �ȹ�� �Ǵϱ�^^\n���Ը� �þ�����", // master 17
        "��.. ����� �����ϰ�^^\n�����͸� �л��� �޾ư��� �ɷ� �ؿ��^^", // master 18
        "��..! �׷��� �ǳ���..?\n���� �ʹ� ����!",
        "�� ���Դ� �����ϰ� ������ ���ϰڽ��ϴ�!!",
        "�׷��� ���� ���Կ��� �μ��ΰ� ���ٰԿ��^^", // master 21
        "�� �˰ڽ��ϴ�..!!",
        "(�̰� ��ȸ��..!)"
    };

    public string[] tutorial = {         // Ʃ�丮�� ��� ����Ʈ
        "���ݺ��� �ܹ��� ����� ����� �˷��ٰԿ��",
        "S�� FŰ Ȥ�� ȭ��ǥ �¿� Ű�� ������ �¿�� �̵��� �� �־���",
        "�ܹ��� ��ᰡ ��� Ʈ���̵��� Ŭ���ϸ�",
        "��� ������ ��ᰡ ���������",
        "�ܹ��Ÿ� �� ����� �մ� �տ� �ִ� Ʈ���̸� Ŭ���ϸ�",
        "�մ� ������ �ܹ��Ű� �����ſ��",
        "�ֹ��� Ű����ũ�� �ִ� �ܹ��� �����Ǹ� ���� �ܹ��Ÿ� ����� �ſ��",
        "��� �մԿ��� �⺻������ �ݶ�� ����Ƣ���� �����ſ��",
        "�ݶ�� ������ �ִ� ���漭�� Ŭ���ϸ� ���� �� �־���",
        "������� �ݶ� Ŭ���ϸ� �մ� ������ �����ſ��",
        "����Ƣ���� ������ ����Ƣ���� ��� Ʈ���̸� Ŭ���ϸ� Ƣ�� �� �־���",
        "���� Ƣ��⿡ �ִ� ����Ƣ���� Ŭ���ϸ� ����Ƣ���� �մ� ������ �����ſ��",
        "�ܹ��ſ� �ݶ�, ����Ƣ���� ��� ������ �� ���� ���� ġ�� �Ǹ��� �� �־���",
        "�ܹ����� �����ǰ� �߸��ǰų� �ݶ�� ����Ƣ���� �������� �ʾҴٸ� ���� ���� ���ؿ��",
        "�� LIFE�� �ϳ� �پ�����.. LIFE�� ��� �������� �׳� ������ ����Ǵ� �����ؿ��",
        "�׷� �轴�� �縸 �ϰ� ���� ���� ���Կ�� ȭ����^^!"
    };

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        uiController = GetComponent<UIController>();
        startDirector = GetComponent<StartDirector>();
        soundDirector = GetComponent<SoundDirector>();
        audioSource = GetComponent<AudioSource>();

        // ���丮 ���� �����ϱ�
        StartCoroutine(ShowStory());
    }

    // Update is called once per frame
    void Update()
    {
        // �����̽��� Ű�� ������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (lineNum)
            {
                // ���ѷα׸�
                case 0:
                    if (lineCount < prologue.Length)
                    {
                        // ������ ��ȭ �ƴϸ�
                        // ���� ��� ����
                        NextLine();
                    }
                    else
                    {
                        // ������ ����̸�
                        // Ʃ�丮�� �������� ����
                        lineNum = 1;

                        // ���丮 ��ȭâ �Ʒ��� �����̵� UI ȿ��
                        uiController.DownUI(uiController.storyUI, 1400f);

                        // Ʃ�丮�� ��� ����
                        StartCoroutine(ShowTutorial());
                    }
                    break;
                // Ʃ�丮�� �̸�
                case 1:
                    if (lineCount < tutorial.Length)
                    {
                        // ������ ��ȭ �ƴϸ�
                        // ���� ��� ����
                        NextTutorial();
                    }
                    else
                    {
                        // ������ ����̸�
                        // ���丮 ��ȭâ UI �Ʒ��� �����̵� ȿ��
                        uiController.DownUI(uiController.storyUI, 1400f);

                        // ���������� ���ư���
                        startDirector.BackStage();
                    }
                    break;
            }


        }
    }

    /// <summary>
    /// ���丮 ��� �޼���
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowStory()
    {
        // ���丮 ���� �ʱ�ȭ
        lineCount = 0;
        lineNum = 0;

        // ��� ��ȯ
        uiController.HideBlack();
        yield return new WaitForSeconds(1f);

        // UI ���� ȿ��
        uiController.UpUI(uiController.storyUI, 1400f);
        uiController.storyUI.SetActive(true);

        // ���丮 ��� ��Ҹ� ȿ����
        soundDirector.SetAudioClip(soundDirector.voiceSound);
        soundDirector.Play();

        // ���� ��� ����
        NextLine();
        yield return null;
    }
    
    /// <summary>
    /// Ʃ�丮�� ��� �޼���
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowTutorial()
    {
        // ���丮 ���� ������Ʈ
        lineCount = 0;
        lineNum = 1;
        yield return new WaitForSeconds(1f);

        // UI Ȱ��ȭ, ��� ��ȯ
        uiController.storyUI.SetActive(false);
        uiController.ShowBlack();
        yield return new WaitForSeconds(1f);

        // ī�޶� ��ȯ
        camera2.SetActive(false);
        camera3.SetActive(true);

        // UI ȿ��
        uiController.HideBlack();
        yield return new WaitForSeconds(1f);
        uiController.UpUI(uiController.storyUI, 1400f);
        uiController.storyUI.SetActive(true);

        // ���� ��� ����
        NextTutorial();
        yield return null;
    }

    /// <summary>
    /// ���� ��� ������Ʈ �޼���
    /// </summary>
    public void NextLine()
    {
        uiController.line.text = prologue[lineCount];
        switch (prologueNum[lineCount])
        {
            case 0:                                      // �÷��̾ ���� ��
                audioSource.pitch = 1;                   // ��ġ ���̰� 
                audioSource.volume = 0.5f;               // ȿ���� ũ�� ���� 
                uiController.player.SetActive(true);     // �÷��̾� �̹��� Ȱ��ȭ 
                uiController.master.SetActive(false);     
                break;
            case 1:                                      // ���� ������ ���� ��
                audioSource.pitch = 0.6f;                // ��ġ ���߱�
                audioSource.volume = 1f;                 // ȿ���� ũ�� ����
                uiController.master.SetActive(true);     // ���� ���� �̹��� Ȱ��ȭ
                uiController.player.SetActive(false);    
                break;
        }

        // ��Ҹ� ȿ���� ���
        soundDirector.Play();

        // ���� ��ȭ �ε��� ī��Ʈ
        lineCount++;
    }

    /// <summary>
    /// ���� Ʃ�丮�� ��� ���� �޼���
    /// </summary>
    public void NextTutorial()
    {
        // ��Ҹ� ȿ���� ���
        audioSource.pitch = 0.6f;
        soundDirector.Play();

        // ��� ���
        uiController.line.text = tutorial[lineCount];

        // �̹��� Ȱ��ȭ
        uiController.master.SetActive(true);
        uiController.player.SetActive(false);

        // ���� ��ȭ �ε���
        lineCount++;
    }
}
