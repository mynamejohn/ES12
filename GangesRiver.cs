using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GangesRiver : MonoBehaviour {

    public AudioManager AM;
    public AudioClip ClearSound;

    public GameObject Red;
    public GameObject Blue;
    public GameObject Yellow;
    bool is_Red = false;
    bool is_Blue = false;
    bool is_Yellow = false;
    bool Answerd = false;

    int stack=0;

    public GameObject[] picture = new GameObject[3];

    public GameObject Door;

    EventManager EM;
	// Use this for initialization
	void Start () {
        EM = FindObjectOfType<EventManager>();
        picture[0] = Red;
        picture[1] = Blue;
        picture[2] = Yellow;
    }
	
	// Update is called once per frame
	void Update () {
        if (is_Red && is_Blue && is_Yellow && !Answerd)
        {
            EM.Event_Number = 202;
            Answerd = true;
            AM.PlaySound(ClearSound);
        }
	}

    public void On_Red()
    {
        Red.SetActive(true);
        StartCoroutine(Fading(picture[stack]));
        stack++;
        is_Red = true;
        return;
    }
    public void On_Blue()
    {
        Blue.SetActive(true);
        StartCoroutine(Fading(picture[stack]));
        stack++;
        is_Blue = true;
        return;
    }
    public void On_Yellow()
    {
        Yellow.SetActive(true);
        StartCoroutine(Fading(picture[stack]));
        stack++;
        is_Yellow = true;
        return;
    }

    public bool getRed() { return is_Red; }
    public bool getYellow() { return is_Yellow; }
    public bool getBlue() { return is_Blue; }

    public void Activate_Door()
    {
        Door.SetActive(true);
        return;
    }

    IEnumerator Fading(GameObject Answerd_Obj)
    {
        Image AC = Answerd_Obj.GetComponent<Image>();
        for (float i = 0; i <= 1; i += 0.01f)
        {
            AC.color = new Color(AC.color.r, AC.color.g, AC.color.b, i);
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
}
