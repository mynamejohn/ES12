using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyWordTrigger : MonoBehaviour {
    AudioManager AM;
    KeyWord KW;
    public int ChapterIndex;
    public int KeywordNumber;
    // Use this for initialization
    private void Awake()
    {
        AM = FindObjectOfType<AudioManager>();
        KW = FindObjectOfType<KeyWord>();
    }
    public void TriggerKeyword_0Stage()
    {
        KW.AccquiredKeyword_0Stage(ChapterIndex, KeywordNumber);
        AM.PlayAudio("UI/Keyword");
        Destroy(gameObject.GetComponent<KeyWordTrigger>());
    }
    public void TriggerKeyword()
    {
        KW.AccquiredKeyword(ChapterIndex, KeywordNumber);
        AM.PlayAudio("UI/Keyword");
        Destroy(gameObject.GetComponent<KeyWordTrigger>());
    }
    public void TriggerKeyword(int chapter, int number)
    {
        KW.AccquiredKeyword(chapter, number);
        AM.PlayAudio("UI/Keyword");
        Destroy(gameObject.GetComponent<KeyWordTrigger>());
    }
}
