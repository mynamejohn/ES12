using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
public class NPC : MonoBehaviour {

    public ControlDialogue CD;

    public JsonData normalConver;
    public TextAsset normalConver_Text;

    TextAsset KeywordFile;
    JsonData KeywordFileJson;

    public KeyWord KW;

    public Text Conver;

    public bool main_conver;

    // Use this for initialization

    private void Awake()
    {
        normalConver = JsonMapper.ToObject(normalConver_Text.text);
        //FirstMeet = JsonMapper.ToObject(FirstMeet_Text.text);
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*public void FirstTalk()
    {
        CD.LoadJson_KeyWord(FirstMeet);
    }*/
    public void StartConver()
    {
        KW.OpenKeyWord();
        CD.LoadJson_KeyWord(normalConver);
    }
    public void TalkAboutKeyWord(string Keyword)
    {
        KeywordFile = Resources.Load<TextAsset>("Main/NPC/" + Keyword);
        KeywordFileJson = JsonMapper.ToObject(KeywordFile.text);
        CD.LoadJson_KeyWord(KeywordFileJson);
    }
    public void ExitKeyWord()
    {
        CD.ExitKeywordDialouge();
        KW.CloseKeyWord();
    }
}
