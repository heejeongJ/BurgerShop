using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 버거 제작 정보 관리 및 갱신 스크립트 

    // 제조 정보
    public GameObject[] makingList = new GameObject[10];      // 제조 중인 버거 재료 오브젝트 리스트
    public int[] make = new int[10];                          // 제조 중인 버거 재료 정보 리스트(재료, 순서)
    public int index = 0;                                     // 제조 중 버거 재료 개수
    public bool isCoke;                                       // 콜라 제공 여부
    public bool isFires;                                      // 감자튀김 제공 여부

    // Start is called before the first frame update
    void Start()
    {
        // 정보 초기화
        for (int i = 0; i < make.Length; i++)
        {
            makingList[i] = null;
            make[i] = 0;
        }
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
