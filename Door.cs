using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    EventManager EM;
	// Use this for initialization
	void Start () {
        EM = FindObjectOfType<EventManager>();
	}
    public void to_Next_Stage()
    {
        EM.EventnumberSet(209);
    }

    
}
