using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlindforGanges : MonoBehaviour {
    public GangesRiver ganges;
    public int SolvedPuzzleCount = 0;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void PuzzleSolved()
    {
        SolvedPuzzleCount++;
        CheckBlindOut();
    }
    void CheckBlindOut()
    {
        if(SolvedPuzzleCount==4)
        {
            StartCoroutine(Fading());
            ganges.gameObject.SetActive(GetComponent<BoxCollider2D>());
        }
        return;
    }

    IEnumerator Fading()
    {
        Image AC = GetComponent<Image>();

        for (float i = 1; i >= 0; i -= 0.1f)
        {
            AC.color = new Color(AC.color.r, AC.color.g, AC.color.b, i);
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.SetActive(false);
        yield break;
    }
}
