using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour
{
    // ���۾� ���� �� �� ��ȯ ��� ��ũ��Ʈ

    // ������Ʈ �ҷ�����
    public UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        uiController = GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // �������� ������ �̵�
    public void BackStage()
    {
        StartCoroutine(GoScene("StageScene"));
    }

    // ���丮 ������ �̵�
    public void OpenGame()
    {
        StartCoroutine(GoScene("StoryScene"));
    }

    // ���ΰ��� ������ �̵�
    public void OpenShop()
    {
        StartCoroutine(GoScene("MainGame"));
    }

    // �� �̵� �޼���
    public IEnumerator GoScene(string sceneName)
    {
        uiController.ShowBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
