using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideGenerator : MonoBehaviour
{
    // 사이드 메뉴 생성드랍 및 동작, 정보 갱신 스크립트

    // 컴포넌트
    public SoundDirector soundDirector;
    public GameObject gameDirector;
    public PlayerController playerController;

    // 프리팹
    public GameObject cokePrefab;
    public GameObject friesPrefab;
    public GameObject packedCokePrefab;
    public GameObject packedFriesPrefab;

    // 상태 체크, 콜라 제조 됐는지, 감자튀김 제조 됐는지
    public bool isCoke;
    public bool isFries;

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        gameDirector = GameObject.Find("GameDirector");
        soundDirector = gameDirector.GetComponent<SoundDirector>();
        playerController = gameDirector.GetComponent<PlayerController>();

        // 콜라, 감자튀김 프리팹 생성되자마자 각자 효과음 출력
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
    /// 사이드 관련 오브젝트 클릭 메서드
    /// </summary>
    private void OnMouseDown()
    {
        
        if(gameObject.name == "CokeMachine")   // 콜라머신 클릭
        {
            // 콜라 이미 있으면 메서드 종료
            if (isCoke) return;

            // 콜라 생성
            GameObject coke = Instantiate(cokePrefab);
            coke.transform.position = new Vector3(1.301f, 1.539f, 17.604f);

            // 콜라 생성 상태로 갱신
            isCoke = true;
        }
        else if(gameObject.name == "Tray_FF")  // 감자튀김 트레이 클릭
        {
            // 감자튀김 이미 있으면 메서드 종료
            if (isFries) return;

            // 콜라 생성
            GameObject fries = Instantiate(friesPrefab);
            fries.transform.position = new Vector3(-6.66f, 1.64f, 17.797f);

            // 감자튀김 생성 상태로 갱신
            isFries = true;
        }
        else if(gameObject.name == "CokePrefab(Clone)")   // 콜라 머신 위에 있는 생성된 콜라 클릭
        {
            // 포장된 콜라 생성 및 제공 위치로 변경
            GameObject sendCoke = Instantiate(packedCokePrefab);
            sendCoke.transform.position = new Vector3(-2.325f, 1.561f, 15.837f);

            // 콜라 제공 상태로 갱신
            playerController.isCoke = true;

            // 콜라 미생성 상태로 갱신
            GameObject.Find("CokeMachine").GetComponent<SideGenerator>().isCoke = false;

            // 효과음 종료
            soundDirector.Stop();

            // 포장안된 콜라 오브젝트 삭제
            Destroy(gameObject);
        }
        else if(gameObject.name == "FriesPrefab(Clone)")   // 튀김기에 생성된 감자튀김 클릭
        {
            // 포장된 감자튀김 생성 및 제공 위치로 변경
            GameObject sendCoke = Instantiate(packedFriesPrefab);
            sendCoke.transform.position = new Vector3(-3.113f, 1.554f, 15.837f);

            // 감자튀김 제공 상태로 갱신
            playerController.isFires = true;

            // 감자튀김 미생성 상태로 갱신
            GameObject.Find("Tray_FF").GetComponent<SideGenerator>().isFries = false;

            // 효과음 종료
            soundDirector.Stop();

            // 포장안된 감자튀김 오브젝트 삭제
            Destroy(gameObject);
        }
    }
}
