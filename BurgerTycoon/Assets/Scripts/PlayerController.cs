using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ���� ���� ���� ���� �� ���� ��ũ��Ʈ 

    // ���� ����
    public GameObject[] makingList = new GameObject[10];      // ���� ���� ���� ��� ������Ʈ ����Ʈ
    public int[] make = new int[10];                          // ���� ���� ���� ��� ���� ����Ʈ(���, ����)
    public int index = 0;                                     // ���� �� ���� ��� ����
    public bool isCoke;                                       // �ݶ� ���� ����
    public bool isFires;                                      // ����Ƣ�� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        // ���� �ʱ�ȭ
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
