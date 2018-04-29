using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public MouseCursor Cs;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    private void OnMouseEnter()
    {
        Cs.PointerChangetoExit();
    }
    private void OnMouseExit()
    {
        Cs.PointerChangetoNormal();
    }
}
