using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    // 用于播放背景音乐的音乐源  


    // 用于播放音效的音乐源  



    /*
    // 控制背景音乐音量大小  
    public float BgVolume
    {
        get
        {
            return m_bgMusic.volume;
        }
        set
        {
            m_bgMusic.volume = value;
        }
    }
    //控制音效音量的大小  
    public float EffectVolmue
    {
        get
        {
            return m_effectMusic.volume;
        }
        set
        {
            m_effectMusic.volume = value;
        }
    }
    */
    /*
    //重写虚方法  
    protected override void Awake()
    {
        base.Awake();
        //实例化音乐源  
        m_bgMusic = gameObject.AddComponent<AudioSource>();
        m_bgMusic.loop = true;    //开启循环  
        m_bgMusic.playOnAwake = false;        //开始播放  

        //实例化音乐源  
        m_effectMusic = gameObject.AddComponent<AudioSource>();
        m_effectMusic.loop = true;
        m_effectMusic.playOnAwake = false;
    }
    */
    // 播放背景音乐，传进一个音频剪辑的name  
    private void PlayLoopSound(object bgName, bool restart = false, bool isLoop = true)
    {
        bool isAudioSourceExsit = false;
        AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
        foreach (AudioSource ad in audioSources)
        {
            if (ad.clip.name.Equals(ResourceManager.Instance.Load<AudioClip>(bgName).name))
            {
                isAudioSourceExsit = true;
            }
        }
        if (!isAudioSourceExsit)
        {
            AudioSource m_bgMusic;
            m_bgMusic = gameObject.AddComponent<AudioSource>();
            m_bgMusic.loop = true;    //开启循环  
            m_bgMusic.playOnAwake = false;        //开始播放  
                                                  //定义一个空的字符串  
            string curBgName = string.Empty;
            //如果这个音乐源的音频剪辑不为空的话  
            if (m_bgMusic.clip != null)
            {
                //得到这个音频剪辑的name  
                curBgName = m_bgMusic.clip.name;
            }

            // 根据用户的音频片段名称, 找到AuioClip, 然后播放,  
            //ResourcesMgr是提前定义好的查找音频剪辑对应路径的单例脚本，并动态加载出来  
            AudioClip clip = ResourceManager.Instance.Load<AudioClip>(bgName);
            //如果找到了，不为空  
            if (clip != null)
            {
                //如果这个音频剪辑已经复制给类音频源，切正在播放，那么直接跳出  
                if (clip.name == curBgName && !restart)
                {
                    return;
                }
                //否则，把改音频剪辑赋值给音频源，然后播放  
                m_bgMusic.clip = clip;
                if (!m_bgMusic.isPlaying)
                {
                    m_bgMusic.Play();
                }
                m_bgMusic.loop = isLoop;
                UnityEngine.Debug.Log("已播放");
            }
            else
            {
                //没找到直接报错  
                // 异常, 调用写日志的工具类.  
                UnityEngine.Debug.Log("没有找到音频片段");
            }

        }

    }
    public void StopSound(string bgClipname)
    {
        AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
        foreach (AudioSource ad in audioSources)
        {
            if (ad.clip.name.Equals(bgClipname) && ad.isPlaying)
            {
                UnityEngine.Debug.Log("已暂停");
                Destroy(ad);

            }
        }
    }
    //播放各种音频剪辑的调用方法，AudioClass是提前写好的存放各种音乐名称的枚举类，便于外面直接调用  
    public void PlayLoop(AudioClass.environment bgName, bool restart = false)
    {
        PlayLoopSound(bgName, restart);
    }

    public void PlayLoop(AudioClass.player bgName, bool restart = false)
    {
        PlayLoopSound(bgName, restart);
    }

    public void PlayLoop(AudioClass.baby bgName, bool restart = false)
    {
        PlayLoopSound(bgName, restart);
    }
    public void PlayLoop(AudioClass.ghost bgName, bool restart = false)
    {
        PlayLoopSound(bgName, restart);
    }

    // 播放音效  
    /// <summary>
    /// 
    /// </summary>
    /// <param name="effectName">音效名</param>
    /// <param name="defAudio">是否从头播放</param>
    /// <param name="pitch">音调（最好0到3之间）</param>
    /// <param name="volume">音量</param>
    private void PlayOneshotSound(object effectName, bool defAudio = true, float pitch = 1f,float volume =1f)
    {
        AudioSource m_effectMusic;
        m_effectMusic = gameObject.AddComponent<AudioSource>();
        m_effectMusic.loop = true;
        m_effectMusic.playOnAwake = false;
        //根据查找路径加载对应的音频剪辑  
        AudioClip clip = ResourceManager.Instance.Load<AudioClip>(effectName);
        //如果为空的画，直接报错，然后跳出  
        if (clip == null)
        {
            UnityEngine.Debug.Log("没有找到音效片段");
            return;
        }
        //否则，就是clip不为空的话，如果defAudio=true，直接播放  
        if (defAudio)
        {

            print(clip.length);
            //PlayOneShot (音频剪辑, 音量大小)  

            if (!m_effectMusic.isPlaying)
            {
                m_effectMusic.pitch = pitch;
                m_effectMusic.PlayOneShot(clip, volume);
            }
        }
        else
        {
            //指定点播放  
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1);
        }
        StartCoroutine(destoryClipAfterPlayed(clip.length, m_effectMusic));

    }

    //播放各种音频剪辑的调用方法，AudioClass是提前写好的存放各种音乐名称的枚举类，便于外面直接调用  
    public void PlayOneshot(AudioClass.environment effectName, bool defAudio = true, float pitch = 1f,float volume = 1f)
    {
        PlayOneshotSound(effectName, defAudio, pitch, volume);
    }

    public void PlayOneshot(AudioClass.player effectName, bool defAudio = true, float pitch = 1f, float volume = 1f)
    {
        PlayOneshotSound(effectName, defAudio, pitch, volume);
    }

    public void PlayOneshot(AudioClass.baby effectName, bool defAudio = true, float pitch = 1f, float volume = 1f)
    {
        PlayOneshotSound(effectName, defAudio, pitch, volume);
    }
    public void PlayOneshot(AudioClass.ghost effectName, bool defAudio = true, float pitch = 1f, float volume = 1f)
    {
        PlayOneshotSound(effectName, defAudio, pitch,volume);
    }

    IEnumerator destoryClipAfterPlayed(float waittime, AudioSource ad)
    {
        yield return new WaitForSeconds(waittime);
        Destroy(ad);
    }
}

