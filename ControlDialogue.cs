using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using LitJson;
using UnityEngine.UI;

public class ControlDialogue : MonoBehaviour
{
    public KeyWord KWcontrol;

    public bool KeyWordMode = false;
    public bool ChoiceMode = false;
    public int ChoiceOpt = 0;

    public GameObject[] Options = new GameObject[4];

    public bool isSpecial;
    public AlertItem At;

    public Text Name_Text;                  //이름
    public Text Dialogue_Text;              //대사
    public Image Stand_Image_Left;                //이미지
    public Image Stand_Image_Right;
    public Image Panel_Image;

    public string namestring;               
    public string scriptstring;
    public string imagestring_left;
    public string imagestring_right;

    public string TextForAnmiate;           //텍스트애니메이션을 위한 문자열
    public int cntForAnimate;

    public bool isScriptEnd;                //현재의 대사가 끝났는지의 여부

    public GameObject SetChat;
        
    public int End_of_Line;                 //대사의 끝

    public bool isActive;                   //대화의 실행 여부 / true이면 실행, flase이면 종료

    public JsonData ConvertedData;          //Json의 객체로, 캐릭터의 이름, 대사, 이미지의 상태 정보

    public int currentIndex=0;
    public GameObject Empty;

    public EventManager EM;

    bool is_event_plus = false;

    public string[] BackLog = new string[50];

    public bool keywordrunning = false;

    IEnumerator CoroutineforText;
    IEnumerator SkipEn;

    void Start()
    {
        CoroutineforText =  TextAnimation(0.01f);
        SkipEn = Skip();
        cntForAnimate = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (isActive)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                Debug.Log("누르는중");
                SkipEn = Skip();
                StartCoroutine(SkipEn);
            }
            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                Debug.Log("뗌");
                StopCoroutine(SkipEn);
            }

            if (isScriptEnd == false)                  //대사가 진행중인 상황에서
            {
                if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))         //r키를 누르면
                {
                    StopTextAnime();
                }

            }
            else if(isScriptEnd)                                        //대사가 끝난 상태에서
            {
                if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))        //r키를 누르면
                {
                    //Debug.Log("Next");
                    ChangeSettings();
                }
            }

        }
        else
            return;

    }

    public void LoadJSON(JsonData ConvertedData_of_Object, bool WaitForClick)   //객체와 상호작용할때마다 호출되는 함수 (해당 객체의 JSON을 로드함) **단순상호작용**
    {
        if(ConvertedData_of_Object == null)
        {
            Debug.Log("파일이없음(경로/파일명확인)");
            return;
        }
        currentIndex = 0;

        isActive = true;

        isScriptEnd = false;

        ConvertedData = ConvertedData_of_Object;

        TextForAnmiate = "";
        Dialogue_Text.text = "";

        End_of_Line = ConvertedData["dialogues"].Count;
        Empty.SetActive(true);

        is_event_plus = false;

        ChangeSettings();
        SetChat.SetActive(true);
        isSpecial = false;

    }
    public void LoadJSON(JsonData ConvertedData_of_Object, bool WaitForClick, bool isspecial)   //특별아이템 획득시
    {
        if (ConvertedData_of_Object == null)
        {
            Debug.Log("파일이없음(경로/파일명확인)");
            return;
        }
        currentIndex = 0;

        isActive = true;

        isScriptEnd = false;

        ConvertedData = ConvertedData_of_Object;

        TextForAnmiate = "";
        Dialogue_Text.text = "";

        End_of_Line = ConvertedData["dialogues"].Count;
        Empty.SetActive(true);

        is_event_plus = false;

        ChangeSettings();
        SetChat.SetActive(true);

        isSpecial = isspecial;
    }

    public void LoadJSON(JsonData ConvertedData_of_Object)         //스토리 진행 대화 
    {
        if (ConvertedData_of_Object == null)
        {
            Debug.Log("파일이없음(경로/파일명확인)");
            return;
        }
        currentIndex = 0;

        isActive = true;

        isScriptEnd = false;

        ConvertedData = ConvertedData_of_Object;

        TextForAnmiate = "";
        Dialogue_Text.text = "";

        End_of_Line = ConvertedData["dialogues"].Count;
        Empty.SetActive(true);

        is_event_plus = true;

        KeyWordMode = false;

        ChangeSettings();

        SetChat.SetActive(true);
        isSpecial = false;
    }

    public void LoadJson_KeyWord(JsonData KeywordData)
    {
        if (KeywordData == null)
        {
            Debug.Log("파일이없음(경로/파일명확인)");
            return;
        }
        currentIndex = 0;

        isActive = true;
        keywordrunning = true;
        isScriptEnd = false;

        ConvertedData = KeywordData;

        TextForAnmiate = "";
        Dialogue_Text.text = "";

        End_of_Line = ConvertedData["dialogues"].Count;

        Empty.SetActive(true);

        is_event_plus = false;

        KeyWordMode = true;

        ChangeSettings();

        SetChat.SetActive(true);
        isSpecial = false;

    }

    public void ExitKeywordDialouge()
    {
        currentIndex = 0;
        isActive = false;

        EM.Doing_Event = false;
        Empty.SetActive(false);

        SetChat.gameObject.SetActive(false);

        TextForAnmiate = "";
        Dialogue_Text.text = "";
        Name_Text.text = "";

        currentIndex = 0;
        cntForAnimate = 0;
        KeyWordMode = false;
    }
    void ChangeSettings()
    {

        if (KeyWordMode)
        {

            if (currentIndex == End_of_Line)
            {

                KWcontrol.KeyWordControl.SetActive(true);
                KWcontrol.Updatastatus();
                keywordrunning = false;
                //return;
            }
            else
            {
                asd();

                scriptstring = ConvertedData["dialogues"][currentIndex]["script_Text"].ToString();
                Name_Text.text = ConvertedData["dialogues"][currentIndex]["character_name"].ToString();

                if (ConvertedData["dialogues"][currentIndex]["standImage_left_Name"] != null)
                {
                    imagestring_left = ConvertedData["dialogues"][currentIndex]["standImage_left_Name"].ToString();
                    Stand_Image_Left.sprite = Resources.Load<Sprite>("StandImage/" + imagestring_left);
                }
                else
                    Stand_Image_Left.sprite = Resources.Load<Sprite>("StandImage/Nothing");

                if (ConvertedData["dialogues"][currentIndex]["standImage_right_Name"] != null)
                {
                    imagestring_right = ConvertedData["dialogues"][currentIndex]["standImage_right_Name"].ToString();
                    Stand_Image_Right.sprite = Resources.Load<Sprite>("StandImage/" + imagestring_right);
                }
                else
                    Stand_Image_Right.sprite = Resources.Load<Sprite>("StandImage/Nothing");
                isScriptEnd = false;

                CoroutineforText = TextAnimation(0.01f);
                StartCoroutine(CoroutineforText);
                currentIndex++;                              //다음대사로
            }
        }
        else
        {
            if (currentIndex == End_of_Line)
            {
                currentIndex = 0;
                isActive = false;

                EM.Doing_Event = false;
                Empty.SetActive(false);
                if (is_event_plus)
                    EM.Event_Number++;
                SetChat.gameObject.SetActive(false);
                TextForAnmiate = "";
                Dialogue_Text.text = "";
                Name_Text.text = "";
                currentIndex = 0;
                cntForAnimate = 0;
                if (isSpecial)
                {
                    Debug.Log(At.ItemName_String);
                    At.PopUp();
                }
                StopAllCoroutines();
                return;
            }
            asd();

            scriptstring = ConvertedData["dialogues"][currentIndex]["script_Text"].ToString();
            Name_Text.text = ConvertedData["dialogues"][currentIndex]["character_name"].ToString();

            if (ConvertedData["dialogues"][currentIndex]["standImage_left_Name"] != null)
            {
                imagestring_left = ConvertedData["dialogues"][currentIndex]["standImage_left_Name"].ToString();
                Stand_Image_Left.sprite = Resources.Load<Sprite>("StandImage/" + imagestring_left);
            }
            else
                Stand_Image_Left.sprite = Resources.Load<Sprite>("StandImage/Nothing");

            if (ConvertedData["dialogues"][currentIndex]["standImage_right_Name"] != null)
            {
                imagestring_right = ConvertedData["dialogues"][currentIndex]["standImage_right_Name"].ToString();
                Stand_Image_Right.sprite = Resources.Load<Sprite>("StandImage/" + imagestring_right);
            }
            else
                Stand_Image_Right.sprite = Resources.Load<Sprite>("StandImage/Nothing");
            
            isScriptEnd = false;

            CoroutineforText = TextAnimation(0.01f);
            StartCoroutine(CoroutineforText);
            currentIndex++;                              //다음대사로

        }
    }
    void StopTextAnime()
    {
        StopCoroutine(CoroutineforText);
        Dialogue_Text.text = scriptstring;          //모든 대사가 한번에 표시
        isScriptEnd = true;
        cntForAnimate = 0;
    }
    void asd()
    {
        StopCoroutine(CoroutineforText);
        cntForAnimate = 0;
    }
    IEnumerator TextAnimation(float stringspeed)
    {
        if (!isScriptEnd)
        {
            Dialogue_Text.text = "";
            while (Dialogue_Text.text != scriptstring)
            {
        
                Dialogue_Text.text += scriptstring[cntForAnimate];
                cntForAnimate++;

                yield return new WaitForSeconds(stringspeed);
            }
            cntForAnimate = 0;
            isScriptEnd = true;
            yield break;
        }
        else
        {
            yield break;
        }
    }

    public void JumptoEnd()
    {
        currentIndex = End_of_Line;
        ChangeSettings();
    }
    IEnumerator Skip()
    {
        while(true)
        {
            ChangeSettings();
            yield return new WaitForSeconds(0.2f);

        }
    }
}