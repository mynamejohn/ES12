using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HangedMan : MonoBehaviour {

    // Use this for initialization
    AudioManager AM;
    public AudioClip Hanged_Man_Sound;
    public AudioClip ClearSound;

    public bool is_Turning=false;
    public bool Activated = false;
    public GameObject HangedMan_Control;
    public GameObject Answerd_obj;
    EventManager EM;
    public GameObject Exit_Button;
    public int stat=0;
    BoxCollider2D Box;
    public bool Answerd=false;
    public GameObject ExitBG;

    public BlindforGanges blind;
	void Start ()
    {
        AM = FindObjectOfType<AudioManager>();
        EM = FindObjectOfType<EventManager>();
        Box = gameObject.transform.GetChild(1).GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (stat==0||Answerd)
        {
            Box.enabled = false;
        }
        else
            Box.enabled = true;
    }

    public void Turning()
    {
        if (Answerd)
            return;
        if(!is_Turning)
            StartCoroutine("Rot");
        return;
    }
    IEnumerator Rot()
    {
        for (int i = 0; i < 45; i++)
        {
            HangedMan_Control.GetComponent<Transform>().Rotate(0, 0, 4);
            yield return new WaitForSeconds(0.01f);
        }

        stat = (stat + 1) % 2;
    }
    public void Turn_ON()
    {
        if (EM.Doing_Event)
            return;
        Activated = true;
        HangedMan_Control.SetActive(Activated);
        EM.Doing_Event = true;
        ExitBG.SetActive(true);
        return;
    }
    public void Turn_OFF()
    {
        Activated = false;
        HangedMan_Control.SetActive(Activated);
        EM.Doing_Event = false;
        ExitBG.SetActive(false);

        return;
    }

    public void Answer()
    {
        HangedMan_Control.GetComponent<Image>().sprite = Resources.Load<Sprite>("2_Stage/Hanged_Man_with_Rope");
        Answerd_obj.SetActive(true);
        Answerd_obj.GetComponent<Fade>().StartFade();
        Answerd = true;
        AM.PlaySound(Hanged_Man_Sound);
        AM.PlaySound(ClearSound);
        blind.PuzzleSolved();

    }
}
