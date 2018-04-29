using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Keypad : MonoBehaviour {

    public Keypad tri_key;
    public EventManager EM;
    // Use this for initialization
    void Start ()
    {
	}
    private void OnMouseDown()
    {
        if (EM.Doing_Event)
        {
            Debug.Log("이베트진행중");
            return;
        }
        else
        {
            EM.Doing_Event = true;
            tri_key.Activated = true;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
