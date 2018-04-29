using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class KeyWord : MonoBehaviour
{

    public GameObject KeyWordControl;
    public NPC npcControl;

    public Text Chapter;

    public string[] KeywordTitle;
    public Text[] CurrentKeywordTitle = new Text[3];

    public GameObject[] ChapterList = new GameObject[5];

    public string[] KeywordSummary;
    public Text[] CurrentKeywordSummary = new Text[3];

    public int[] ActiveStatus;
    int[] ReadOnce;

    public Image[] NotRead = new Image[3];

    public int contents;

    public Text Pages;

    public int currentpages = 1;
    public int chapterindex = 0;
    public int endofpages;

    public TextAsset keywords;
    JsonData keywords_Json;

    public Pump AlertImage;
    // Use this for initialization
    private void Awake()
    {
        keywords_Json = JsonMapper.ToObject(keywords.text);

        Pages.text = currentpages.ToString() + "/" + endofpages.ToString();
        SetChapter();
        CheckNew();
    }
    void Start()
    {

    }

    // Update is called once per frame

    public void ControlPages(bool isNext)
    {
        if (isNext)
        {
            if (currentpages == endofpages)
                return;
            currentpages++;
            SetPage();
        }
        else
        {
            if (currentpages == 1)
                return;
            currentpages--;
            SetPage();
        }
    }
    public void ControlChapter(int Next)
    {
        chapterindex = Next;
        SetChapter();
    }
    public void ChapterListSet()
    {
        for (int i = 0; i < 5; i++)
        {
            ChapterList[i].SetActive(false);
        }
        ChapterList[chapterindex].SetActive(true);
    }
    public void Updatastatus()
    {
        for (int i = 0; i < keywords_Json["KeyWord"][chapterindex]["Contents"].Count; i++)
        {
            KeywordTitle[i] = keywords_Json["KeyWord"][chapterindex]["Contents"][i]["KeywordTitle"].ToString();
            KeywordSummary[i] = keywords_Json["KeyWord"][chapterindex]["Contents"][i]["KeywordSummary"].ToString();
            ActiveStatus[i] = int.Parse(keywords_Json["KeyWord"][chapterindex]["Contents"][i]["isActive"].ToString());
            ReadOnce[i] = int.Parse(keywords_Json["KeyWord"][chapterindex]["Contents"][i]["ReadOnce"].ToString());
        }

        SetPage();
    }
    public void SetChapter()
    {
        currentpages = 1;
        ChapterListSet();
        if (keywords_Json["KeyWord"][chapterindex]["Contents"].Count % 3 == 0)
            endofpages = (keywords_Json["KeyWord"][chapterindex]["Contents"].Count) / 3;
        else
            endofpages = ((keywords_Json["KeyWord"][chapterindex]["Contents"].Count) / 3) + 1;

        Chapter.text = keywords_Json["KeyWord"][chapterindex]["Chapter"].ToString();

        contents = keywords_Json["KeyWord"][chapterindex]["Contents"].Count;

        if (contents % 3 != 0)
        {
            int size = 3;

            if (contents > 3 && contents < 6)
                size = 6;
            else if (contents > 6 && contents < 9)
                size = 9;
            KeywordTitle = new string[size];
            KeywordSummary = new string[size];
            ActiveStatus = new int[size];
            ReadOnce = new int[size];
        }

        else
        {
            KeywordTitle = new string[contents];
            KeywordSummary = new string[contents];
            ActiveStatus = new int[contents];
            ReadOnce = new int[contents];
        }
        for (int i = 0; i < keywords_Json["KeyWord"][chapterindex]["Contents"].Count; i++)
        {
            KeywordTitle[i] = keywords_Json["KeyWord"][chapterindex]["Contents"][i]["KeywordTitle"].ToString();
            KeywordSummary[i] = keywords_Json["KeyWord"][chapterindex]["Contents"][i]["KeywordSummary"].ToString();
            ActiveStatus[i] = int.Parse(keywords_Json["KeyWord"][chapterindex]["Contents"][i]["isActive"].ToString());
            ReadOnce[i] = int.Parse(keywords_Json["KeyWord"][chapterindex]["Contents"][i]["ReadOnce"].ToString());
        }

        SetPage();
    }

    public void SetPage()
    {
        for (int i = 0; i < 3; i++)
        {
            if (ActiveStatus[i + ((currentpages - 1) * 3)] == 1)
            {
                CurrentKeywordTitle[i].text = "????";
                CurrentKeywordSummary[i].text = "???????";
                NotRead[i].color = new Color(NotRead[i].color.r, NotRead[i].color.g, NotRead[i].color.b, 0);
            }
            else if (ActiveStatus[i + ((currentpages - 1) * 3)] == 2)
            {
                CurrentKeywordTitle[i].text = KeywordTitle[i + ((currentpages * 3) - 3)];
                CurrentKeywordSummary[i].text = KeywordSummary[i + ((currentpages * 3) - 3)];
                if (ReadOnce[i + ((currentpages * 3) - 3)] == 0)
                    NotRead[i].color = new Color(NotRead[i].color.r, NotRead[i].color.g, NotRead[i].color.b, 255);
                else
                    NotRead[i].color = new Color(NotRead[i].color.r, NotRead[i].color.g, NotRead[i].color.b, 0);
            }
            else
            {
                CurrentKeywordTitle[i].text = "";
                CurrentKeywordSummary[i].text = "";
                NotRead[i].color = new Color(NotRead[i].color.r, NotRead[i].color.g, NotRead[i].color.b, 0);
            }
        }
        Pages.text = currentpages.ToString() + "/" + endofpages.ToString();
    }
    public void CheckNew()
    {
        for (int i = 0; i <4 ; i++)
        {
            for (int j = 0; j < keywords_Json["KeyWord"][i]["Contents"].Count; j++)
            {
                if (int.Parse(keywords_Json["KeyWord"][i]["Contents"][j]["isActive"].ToString()) == 1)
                    continue;
                else if (int.Parse(keywords_Json["KeyWord"][i]["Contents"][j]["isActive"].ToString()) == 2)
                {
                    if (int.Parse(keywords_Json["KeyWord"][i]["Contents"][j]["ReadOnce"].ToString()) == 0)
                    {
                        AlertImage.Trun(true);
                        return;
                    }
                    else
                        continue;
                }
            }
        }
        AlertImage.Trun(false);
    }
    public void SlectTopic(int index)
    {
        if (currentpages * 3 - (3 - index) > contents - 1 || ActiveStatus[currentpages * 3 - (3 - index)] == 1)
            return;
        string Conver;
        Conver = keywords_Json["KeyWord"][chapterindex]["Contents"][currentpages * 3 - (3 - index)]["Conver"].ToString();

        if (keywords_Json["KeyWord"][chapterindex]["Contents"][currentpages * 3 - (3 - index)]["ReadOnce"].ToString() == "0")
        {
            keywords_Json["KeyWord"][chapterindex]["Contents"][currentpages * 3 - (3 - index)]["ReadOnce"] = "1";
            SetChapter();
        }
        CheckNew();

        KeyWordControl.SetActive(false);
        npcControl.TalkAboutKeyWord(Conver);
    }

    public void OpenKeyWord()
    {
        chapterindex = 0;
        SetChapter();
        KeyWordControl.SetActive(true);
    }
    public void CloseKeyWord()
    {
        KeyWordControl.SetActive(false);
    }

    public void AccquiredKeyword_0Stage(int chapter, int number)
    {
        if (int.Parse(keywords_Json["KeyWord"][chapter]["Contents"][number - 1]["isActive"].ToString()) == 2)
        {
            return;
        }
        AlertImage.Trun(true);
        keywords_Json["KeyWord"][chapter]["Contents"][number - 1]["isActive"] = "2";
        keywords_Json["KeyWord"][chapter]["Contents"][number - 1]["ReadOnce"] = "0";

    }
    public void AccquiredKeyword(int chapter, int number)
    {
        if (int.Parse(keywords_Json["KeyWord"][chapter]["Contents"][number - 1]["isActive"].ToString()) == 2)
        {
            return;
        }
        keywords_Json["KeyWord"][chapter]["Contents"][number - 1]["isActive"] = "2";
        keywords_Json["KeyWord"][chapter]["Contents"][number - 1]["ReadOnce"] = "0";
    }

    public bool getInfo(int chapter, int number)
    {
        if (keywords_Json["KeyWord"][chapter]["Contents"][number - 1]["ReadOnce"].ToString() == "1")
        {
            return true;
        }
        return false;
    }
    public void LoadKeyWord(JsonData data)
    {
        keywords_Json = data;
        SetChapter();
        CheckNew();
    }
    public JsonData GetKeywordData()
    {
        return keywords_Json;
    }
}
