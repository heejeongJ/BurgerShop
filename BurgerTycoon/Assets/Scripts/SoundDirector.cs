using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDirector : MonoBehaviour
{
    // �Ҹ� ��ȯ �� ��� ���

    // �����Ŭ��
    public AudioClip startBGM;
    public AudioClip BGM;
    public AudioClip customerComingSound;
    public AudioClip dropIngredientSound;
    public AudioClip deleteIngredientSound;
    public AudioClip burgerResultBellSound;
    public AudioClip cokeSound;
    public AudioClip friesSound;
    public AudioClip billSound;
    public AudioClip voiceSound;
    public AudioClip tempSound;

    // ������Ʈ
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ �ҷ�����
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ����� Ŭ�� ���� �޼���
    /// </summary>
    /// <param name="clip"></param>
    public void SetAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    /// <summary>
    /// ����� ��� �޼���
    /// </summary>
    public void Play()
    {
        audioSource.Play();
    }

    /// <summary>
    /// ����� ���� �޼���
    /// </summary>
    public void Stop()
    {
        audioSource?.Stop();
    }
}
