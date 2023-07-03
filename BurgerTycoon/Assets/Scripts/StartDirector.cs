using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour
{
    // 시작씬 진행 및 씬 전환 기능 스크립트

    // 컴포넌트 불러오기
    public UIController uiController;

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        uiController = GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 스테이지 씬으로 이동
    public void BackStage()
    {
        StartCoroutine(GoScene("StageScene"));
    }

    // 스토리 씬으로 이동
    public void OpenGame()
    {
        StartCoroutine(GoScene("StoryScene"));
    }

    // 메인게임 씬으로 이동
    public void OpenShop()
    {
        StartCoroutine(GoScene("MainGame"));
    }

    // 씬 이동 메서드
    public IEnumerator GoScene(string sceneName)
    {
        uiController.ShowBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
