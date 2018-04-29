using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    AudioSource AS;
	// Use this for initialization
	void Start ()
    {
        AS = GetComponent<AudioSource>();
        AS.loop = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
    
    public void PlaySound(AudioClip AC)
    {
        AS.PlayOneShot(AC);
    }

    public void PlayAudio(string name)          //함수 호출할때 매개변수로 파일이름 넣으면 그거 플레이됨
    {
        //var addAS = this.gameObject.AddComponent<AudioSource>();
             
        AudioClip audio = Resources.Load("Sound/" + name) as AudioClip;

        //addAS.loop = false;
        //addAS.PlayOneShot(audio);
        AS.PlayOneShot(audio);
    }

    public void PlayBGM(string name)            //이건쓰지마셈
    {
        AudioClip audio = Resources.Load("Soundd/" + name) as AudioClip;

        AS.loop = true;
        AS.PlayOneShot(audio);
    }

    public void StopBGM(string name)            //이것도
    {
        AS.Stop();
    }
}
