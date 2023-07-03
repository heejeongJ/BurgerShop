using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDirector : MonoBehaviour
{
    // �������� ����, ������ ���� �� UI ���� ����

    // ���� �Ķ����
    public static int[] dateBurgerSale = { 0, 0, 0, 0, 0, 0, 0 };     // ���� ���� �Ǹ� ��
    public static int[] dateSale = { 0, 0, 0, 0, 0, 0, 0 };           // ���� ����
    public static int totalSale = 0;                                  // �� ����
    public static int playDate = 0;                                   // ���� �� ���� ī��Ʈ

    // ������Ʈ �ҷ�����
    UIController uiController;
    StartDirector startDirector;
    SoundDirector soundDirector;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        uiController = GetComponent<UIController>();
        startDirector = GetComponent<StartDirector>();
        soundDirector = GetComponent<SoundDirector>();
        audioSource = GetComponent<AudioSource>();

        // ��������UI �ʱ�ȭ
        uiController.stageUI.SetActive(false);

        // �������� ���� ����
        SetDateValue();

        // ���̵��� ��� ��ȯ ȿ��
        uiController.HideBlack();

        // �������� ���� ����
        StartCoroutine(ShowStage());

        // ���� 7���� �÷��� �ϸ�
        if(playDate == 7)
        {
            // ���� ���� ���
            StartCoroutine(ShowEnding());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �������� UI Ȱ��ȭ �޼���
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
    /// �������� ���� ���� �޼���
    /// </summary>
    void SetDateValue()
    {
        // ���� �� ���� ��ŭ
        for(int i = 0; i < playDate; i++)
        {
            // ���� ���UI Ȱ��ȭ
            uiController.stageDayResult[i].SetActive(true);

            // ���� ���� ��� ���
            uiController.stageDayResultText[i].text = "�Ǹ��� ����\n" + dateBurgerSale[i].ToString() + "\n���� ����\n" + dateSale[i].ToString();
            
            // ���� ���� ���ݿ� ���� �� �ڸ�Ʈ ���
            if(dateSale[i] > 650000)
            {
                uiController.stageDayResultText[i].text += "\n���� ����!";
            }
            else if(dateSale[i] > 570000)
            {
                uiController.stageDayResultText[i].text += "\n���ϰ� �־�!";
            }
            else
            {
                uiController.stageDayResultText[i].text += "\n�й�����..";
            }

            // �� ���� ���
            uiController.stageTotalSales.text = totalSale.ToString() + "��";
        }
    }

    /// <summary>
    /// ���� ��� �޼���
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowEnding()
    {
        // ��� ��ȯ ȿ��
        uiController.ShowBlack();
        yield return new WaitForSeconds(2f);

        // ���1 Ȱ��ȭ 
        soundDirector.SetAudioClip(soundDirector.voiceSound);
        soundDirector.Play();
        uiController.ending.SetActive(true);
        uiController.endingImages[0].SetActive(true);
        uiController.endingText.text = "�����ϵ��� �� �� �ݾ���\n" + totalSale + "��..";
        yield return new WaitForSeconds(2f);

        if (totalSale > 4000000)
        {
            // ���ǿ��� ���2 
            soundDirector.Play();
            uiController.endingText.text = "���Ÿ� ������ �� ���п�\n��ϱ��� ������ �� �־���..!";
            yield return new WaitForSeconds(2f);

            // ���ǿ��� ���3 
            soundDirector.Play();
            uiController.endingText.text = "���� �Ƚ��ϰ� ���ж� ���ϵ� �ϰ�\n��� �غ� �� �� �־�!";
            yield return new WaitForSeconds(2f);
            soundDirector.Play();

            // ���ǿ��� ���4 
            uiController.endingText.text = "���� �����̾�.. ����.. �� �ڽ�!";
            yield return new WaitForSeconds(2f);
            uiController.endingText.text = "- HAPPY ENDING -";
        }
        else
        {
            // ���忣�� ���2 
            soundDirector.Play();
            uiController.endingText.text = "�̰ɷ� ��ϱݿ� �õ� ����..\n400������ �־�� ��";
            yield return new WaitForSeconds(2f);

            // ���ǿ��� ���3 
            soundDirector.Play();
            uiController.endingText.text = "�� �ɷ��� �̰� �ۿ� �ȵǴٴ�..\n�� �� �ȵǴ°ǰ�..";
            yield return new WaitForSeconds(2f);


            // ���ǿ��� ���4
            uiController.endingImages[0].SetActive(false);
            uiController.endingImages[1].SetActive(true);
            audioSource.pitch = 3;
            soundDirector.Play();
            uiController.endingText.text = "\"�Ͼ�� ���￩�� ���̿�..\"";
            yield return new WaitForSeconds(2f);


            // ���ǿ��� ���5
            soundDirector.Play();
            uiController.endingText.text = "\"���￩������ ���������� ���� �� ����..\"";
            yield return new WaitForSeconds(2f);


            // ���ǿ��� ���6
            audioSource.pitch = 1;
            soundDirector.Play();
            uiController.endingImages[0].SetActive(true);
            uiController.endingImages[1].SetActive(false);
            uiController.endingText.text = "��.. ��������..?!";
            yield return new WaitForSeconds(2f);


            // ���ǿ��� ���7
            uiController.endingImages[0].SetActive(false);
            uiController.endingImages[1].SetActive(true);
            audioSource.pitch = 3;
            soundDirector.Play();
            uiController.endingText.text = "\"���� �ٽ� ������ ������ �����ְڴ�..\"";
            yield return new WaitForSeconds(2f);


            // ���ǿ��� ���8
            uiController.endingImages[0].SetActive(true);
            uiController.endingImages[1].SetActive(false);
            audioSource.pitch = 1;
            soundDirector.Play();
            uiController.endingText.text = "��.. �ٽ� �� �������..\n�Ⱦ�...!";

            // ���� UI ��Ȱ��ȭ
            yield return new WaitForSeconds(2f);
            uiController.ending.SetActive(false);

            // ���� ���� �ʱ�ȭ
            StageDirector.playDate = 0;
            StageDirector.totalSale = 0;
            for(int i = 0; i < StageDirector.dateSale.Length; i++)
            {
                StageDirector.dateSale[i] = 0;
                StageDirector.dateBurgerSale[i] = 0;
            }

            // �������������� �����
            startDirector.BackStage();
        }

        yield return null;
    }

}
