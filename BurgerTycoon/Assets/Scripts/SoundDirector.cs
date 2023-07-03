using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDirector : MonoBehaviour
{
    // 소리 전환 및 출력 기능

    // 오디오클립
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

    // 컴포넌트
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // 컴포넌트 불러오기
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 오디오 클립 세팅 메서드
    /// </summary>
    /// <param name="clip"></param>
    public void SetAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    /// <summary>
    /// 오디오 출력 메서드
    /// </summary>
    public void Play()
    {
        audioSource.Play();
    }

    /// <summary>
    /// 오디오 종료 메서드
    /// </summary>
    public void Stop()
    {
        audioSource?.Stop();
    }
}
