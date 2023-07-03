using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public UIController uIController;
    // Start is called before the first frame update
    void Start()
    {
        uIController = GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        if(gameObject.name == "startButton")
        {
            transform.localScale = Vector3.one * 1.1f;
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.name == "startButton")
        {
            transform.localScale = Vector3.one * 1.0f;
        }

    }

    private void OnMouseDown()
    {
        if (gameObject.name == "startButton")
        {
            SceneManager.LoadScene("MainGame");
        }
    }

    public void StartGame()
    {
        StartCoroutine(GoStage());
    }

    public void OpenGame()
    {
        StartCoroutine(GoMainGame());
    }

    public IEnumerator GoStage()
    {
        uIController.ShowBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("StageScene");
    }
    public IEnumerator GoMainGame()
    {
        uIController.ShowBlack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainGame");
    }
}
