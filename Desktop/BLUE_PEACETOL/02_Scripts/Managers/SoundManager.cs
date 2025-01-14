 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : SingletonBase<SoundManager>
{
    public List<AudioClip> BGMClip;
    public List<AudioClip> SFXClip;

    public int MaxAudioSource = 5;

    AudioSource BGMSource;
    AudioSource[] SFXSource;


    private void OnEnable()
    {
        // BGM AudioSource Setup
        BGMSource = gameObject.AddComponent<AudioSource>();
        BGMSource.volume = 1;
        BGMSource.playOnAwake = false;
        BGMSource.loop = true;

        SFXSource = new AudioSource[MaxAudioSource];

        for (int i = 0; i < SFXSource.Length; ++i)
        {
            SFXSource[i] = gameObject.AddComponent<AudioSource>();
            SFXSource[i].volume = 1;
            SFXSource[i].playOnAwake = false;
            SFXSource[i].loop = false;
        }

        // 씬 로드 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 로드 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 이름에 맞는 BGM 재생
        // 이전 씬 BGM은 정지
        StopBGM();
        
        switch (scene.name)
        {
            case "03_ROOM_PVG_KOR":
                PlayBGM("KOR_PVG", true, 0.3f);
                break;
            case "07_ACT_RUN_SHOOTING":
                PlayBGM("AfterExplosion", true, 0.3f);
                break;
            default:
                StopBGM();
                break;
        }
    }

    public void PlayBGM(string name, bool isLoop = true, float volume = 1.0f)
    {
        foreach (var clip in BGMClip)
        {
            if (clip.name == name)
            {
                BGMSource.clip = clip;
                BGMSource.loop = isLoop;
                BGMSource.volume = volume;
                BGMSource.Play();
                return;
            }
        }
        Debug.LogWarning($"BGM Clip '{name}' not found in the list.");
    }

    public void StopBGM(float fadeOutDuration = 0)
    {
        if (BGMSource && BGMSource.isPlaying)
        {
            if (fadeOutDuration > 0)
            {
                StartCoroutine(FadeOutBGM(fadeOutDuration));
            }
            else
            {
                BGMSource.Stop();
            }
        }
    }

    private IEnumerator FadeOutBGM(float duration)
    {
        float startVolume = BGMSource.volume;

        while (BGMSource.volume > 0)
        {
            BGMSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        BGMSource.Stop();
        BGMSource.volume = startVolume;
    }
    
    public void PlaySFX(string name, float volume = 1.0f, bool isLoop = false)
    {
        foreach (var clip in SFXClip)
        {
            if (clip.name == name)
            {
                AudioSource source = GetEmptyAudioSource();
                if (source != null)
                {
                    source.clip = clip;
                    source.loop = isLoop;
                    source.volume = volume;
                    source.Play();
                    return;
                }
                else
                {
                    Debug.LogWarning("No available AudioSource to play SFX.");
                }
            }
        }
        Debug.LogWarning($"SFX Clip '{name}' not found in the list.");
    }

    public void StopSFX(string name)
    {
        for (int i = 0; i < SFXSource.Length; ++i)
        {
            if (SFXSource[i].isPlaying && SFXSource[i].clip.name == name)
            {
                SFXSource[i].Stop();
                SFXSource[i].clip = null;
            }
        }
    }

    private AudioSource GetEmptyAudioSource()
    {
        float largestProgress = 0;
        int largestIndex = -1;

        for (int i = 0; i < SFXSource.Length; ++i)
        {
            if (!SFXSource[i].isPlaying)
            {
                return SFXSource[i];
            }

            float progress = SFXSource[i].time / SFXSource[i].clip.length;
            if (progress > largestProgress && !SFXSource[i].loop)
            {
                largestProgress = progress;
                largestIndex = i;
            }
        }

        return largestIndex != -1 ? SFXSource[largestIndex] : null;
    }

    public void SetBGMVolume(float volume)
    {
        BGMSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var source in SFXSource)
        {
            source.volume = Mathf.Clamp01(volume);
        }
    }
}