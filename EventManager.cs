using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using LitJson;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameLoader GLManager;
    public SoundManager SM;
    public Image fadeImage;
    public GameObject fadeImage_;
    private bool isInTransition = false;
    private float transition;
    private bool isShowing;
    private float duration;

    public ControlDialogue CD;
    private TextAsset Text_Data;
    private JsonData Json_Data;

    public int Event_Number=0;
    public int Event_Number_1=1;

    public bool Doing_Event=false;

    // 현경
	public Transform Stage3;
    Transform Click;
	bool FirstSea = false;
    public Transform KWControl;
    public GameObject Noise;


    //SB꺼
    ActiveDialogue AD;
    public GameObject Panel1_1_defalut;
    public GameObject Panel1_1;
    public GameObject Panel1_2_defalut;
    public GameObject Panel1_2;
    public GameObject BirdCage;
    public GameObject Window;
    public GameObject inventory;
    public GameObject SpiderJem;
    public bool MakeDreamCatcher = false;
    public GameObject Owl_Cage;
    public GameObject Panel1_3_defalut;
    public GameObject Panel1_3;
    int FirstDay = 1;
    bool FirstRain = false;
    bool FirstNight = false;

    public GameObject MainStage;
    public GameObject Stage_1;
    public GameObject Stage_2;
    public GameObject Stage_3;

    public int statusofstage = 0;
    //인벤토리 아이템 삭제용
    public GameObject Grid;

    public KeyWord KWhandler;
    public Pump pumphandler;
    // Use this for initialization
    void Start ()
    {
        SM = FindObjectOfType<SoundManager>();
        AD = FindObjectOfType<ActiveDialogue>();
	}
	
	// Update is called once per frame
	void Update () {
        EventList();
    }

    public void EventList()                                         //스테이지에 있을 이벤트리스트
    {
        if (Event_Number_1 == Event_Number)
        {
            return;
        }
        else
        {
            switch (Event_Number)
            {
                // 0~100 Main
                case 0:
                    GLManager.SaveGame(4);
                    //statusofstage = GLManager.LoadGame();
                    
                    SM.ChangeBGM_Fun("Stage0/TheVisitors");
                    StartCoroutine(Fadeing(false, 3.0f, false));
                    break;
                case 1:
                    Text_Data = Resources.Load<TextAsset>("Main/NPC/FirstMeet");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;


                case 10:
                    StartCoroutine(Fadeing(true, 3.0f, false));
                    pumphandler.EndPump();
                    break;
                case 11:
                    MainStage.SetActive(false);
                    Stage_1.SetActive(true);
                    SM.ChangeBGM_Fun("Stage1/Bgm/Bgm(1-1)");
                    KWhandler.AccquiredKeyword(1, 1);
                    KWhandler.ControlChapter(1);
                    KWhandler.ChapterListSet();
                    StartCoroutine(Fadeing(false, 3.0f, false));
                    break;

                case 20:
                    StartCoroutine(Fadeing(true, 3.0f, false));
                    pumphandler.EndPump();
                    break;
                case 21:
                    MainStage.SetActive(false);
                    Stage_2.SetActive(true);
                    SM.ChangeBGM_Fun("Stage2/Main");
                    StartCoroutine(Fadeing(false, 3.0f, false));
                    break;

                case 30:
                    StartCoroutine(Fadeing(true, 3.0f, false));
                    pumphandler.EndPump();
                    break;
                case 31:
                    MainStage.SetActive(false);
                    Stage_3.SetActive(true);
                    Stage_3.transform.Find("1Round").gameObject.SetActive(true);
                    Stage_3.transform.Find("2Round").gameObject.SetActive(false);
                    Stage_3.transform.Find("3Round").gameObject.SetActive(false);
                    SM.ChangeBGM_Fun("Stage3/1Round/비창소리키움");
                    StartCoroutine(Fadeing(false, 3.0f, false));
                    break;

                case 40:
                    StartCoroutine(Fadeing(true, 3.0f, false));
                    pumphandler.EndPump();
                    break;
                case 41:
                    MainStage.SetActive(false);
                    Stage_3.SetActive(true);
                    Stage_3.transform.Find("1Round").gameObject.SetActive(false);
                    Stage_3.transform.Find("2Round").gameObject.SetActive(false);
                    Stage_3.transform.Find("3Round").gameObject.SetActive(true);
                    SM.ChangeBGM_Fun("Stage3/3Round/3-3 BGM");
                    StartCoroutine(Fadeing(false, 3.0f, false));
                    break;
                // 50번부터 쓸게 ㅜㅜ
                //50-
                case 50:
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/EventDialogue/PutFeed");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 52:
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/EventDialogue/PutSpiderWeb");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 54:
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/EventDialogue/PutSpiderJem");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 56:
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/EventDialogue/PutFeather");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 58:
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/EventDialogue/PutStarPowder");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                // 90-94 1-1 방안 상태 변경시마다 대사 발생
                case 90:
                    FirstDay += 1;
                    if (FirstDay >= 2)
                    {
                        Text_Data = Resources.Load<TextAsset>("Stage1-1/Dialogue/Day");
                        Json_Data = JsonMapper.ToObject(Text_Data.text);
                        CD.LoadJSON(Json_Data);
                    }
                    break;
                case 92:
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/Dialogue/Night");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 94:
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/Dialogue/Rain");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                // 100~199 1Stage
                //100~102 1에서 아버지편지읽고 1-1로 넘어가는 이벤트
                case 100:
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/EventDialogue/ReadFathersLetter");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 101:
                    StartCoroutine(Fadeing(true, 3f, false));
                    //Fade(true, 1.5f);
                    break;
                case 102:
                    Panel1_1_defalut.SetActive(false);
                    Panel1_1.SetActive(true);
                    //BirdCage.SetActive(true);
                    //Window.SetActive(true);
                    StartCoroutine(Fadeing(false, 3f, false));
                    break;
                case 103:
                    //1-1BGM재생
                    //SM.ChangeBGM_Fun("Stage1/Bgm/Bgm(1-1)");
                    /*
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_Number = 2;
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_ListPlay();
                    */

                    //1-1 시작시 혼잣말
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/EventDialogue/StartBrazilStage");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                //105~107 1-1에서 드림캐쳐를 완성하고 난후 x버튼으로 나오면 1-2로 넘어가는 이벤트
                case 105:
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/EventDialogue/CompleteDreamCatcher");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 106:
                    StartCoroutine(Fadeing(true, 1.5f, false));
                    break;
                case 107:
                    Panel1_1.SetActive(false);
                    BirdCage.SetActive(false);
                    Panel1_2_defalut.SetActive(true);
                    Window.SetActive(false);
                    StartCoroutine(Fadeing(false, 1.5f, false));
                    //1-2 BGM재생
                    SM.ChangeBGM_Fun("Stage1/Bgm/Bgm(1-2)");
                    /*
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_Number = 4;
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_ListPlay();
                    */


                    /*
                    //몽골스테이지 첫대사 발생
                    Event_Number = 115;
                    */
                    break;
                //새장에 모이를 두고난 후 창문상태가 변할때 맑은날이면 새가 들어오는 이벤트
                case 109:

                    //새지저귐 효과음 재생
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_Number = 11;
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();
                    
                    GameObject BirdCagePutFeed = BirdCage.transform.Find("BirdCagePutFeed").gameObject;
                    BirdCagePutFeed.SetActive(false);
                    GameObject BirdCageFull = BirdCage.transform.Find("BirdCageFull").gameObject;
                    BirdCageFull.SetActive(true);
                    //대사진행
                    /*
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/EventDialogue/CompleteBirdCage");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    */
                    GameObject.Find("WindowButton").GetComponent<RoomWindow>().PutFeedToBirdCage = false;

                    break;
                //거미줄로 빗물보석 얻기 이벤트    
                case 111:
                    
                    Window.transform.Find("2_CleanWindow").Find("PutSpiderWeb").gameObject.SetActive(false);
                    SpiderJem.SetActive(true);
                    GameObject.Find("WindowButton").GetComponent<RoomWindow>().IsSpiderJemEvent = true;
                    /*
                    int CurtainStateTempNum = GameObject.Find("WindowButton").GetComponent<RoomWindow>().Curtain_State;
                    if(CurtainStateTempNum == 2)
                        SpiderJem.SetActive(true);
                    */
                    //대사진행
                    /*
                    Text_Data = Resources.Load<TextAsset>("Stage1-1/EventDialogue/CompleteSpiderJem");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    */
                    GameObject.Find("WindowButton").GetComponent<RoomWindow>().PutSpiderWeb = false;

                    break;

                //몽골스테이지(1-2)
                //몽골 디폴트방에서 로잘린 편지 읽고 넘어가기
                case 113:
                    //허르헉레시피 단서얻음
                    GameObject.Find("HintTextManager").GetComponent<TextChange>().ToUnlockTextNumberSet(4);

                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/ReadRoJalLetter");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 114:
                    StartCoroutine(Fadeing(true, 1.5f, false));
                    //Fade(true, 1.5f);
                    break;
                case 115:
                    Panel1_2_defalut.SetActive(false);
                    Panel1_2.SetActive(true);
                    StartCoroutine(Fadeing(false, 1.5f, false));
                    break;

                //몽골 되고나서 첫대사(116-117)
                case 116:
                    //1-2BGM재생
                    //GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_Number = 4;
                    //GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_ListPlay();
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/StartMongGoalStage");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    //박제 단서얻음
                    GameObject.Find("HintTextManager").GetComponent<TextChange>().ToUnlockTextNumberSet(5);
                    break;
                    
                //허르헉을 완성시 대사후 1-3 디폴트방으로 이동 (118-121)
                case 118:
                    Destroy(Grid.transform.Find("0-Knife").gameObject); //칼 삭제
                    Destroy(Grid.transform.Find("2-FullWateringCan").gameObject); //물뿌리개 삭제
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/CompleteTable");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 119:
                    StartCoroutine(Fadeing(true, 1.5f, false));
                    break;
                case 120:
                    Panel1_2.SetActive(false);
                    //1-3방 BGM재생
                    SM.ChangeBGM_Fun("Stage1/Bgm/Bgm(1-3)");
                    /*
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_Number = 11;
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_ListPlay();
                    */
                    Panel1_3_defalut.SetActive(true);
                    StartCoroutine(Fadeing(false, 1.5f, false));
                    break;

                case 122:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/Getmeat");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 124:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/Getmilk");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                //빈물뿌리개 하마에 놓았을시
                case 126:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/GetWater");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                //(128-134) 씨앗에 물을 줬을시 이벤트대사
                case 128:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/CompleteCarrot");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 130:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/CompletePotato");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 132:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/CompleteFoxtail");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 134:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/CompleteBean");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                //(136-139) 강아지풀 & 콩 을 잘라냈을시 이벤트 대사
                case 136:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/CutFoxtail");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 138:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/CutBean");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                //(140-141) 모닥불에 돌을 놨을시 대사
                case 140:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/PutStoneTofire");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                //(142-151) 테이블에 음식 재료들 놓기
                case 142:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/PutHotStone");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 144:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/PutMeat");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 146:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/PutMilkBottle");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 148:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/PutPotato");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 150:
                    Text_Data = Resources.Load<TextAsset>("Stage1-2/EventDialogue/PutCarrot");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;




                //(1-3)
          
                
                /*
                //망치로 금시계에서 금침을 획득했을시 (154-155)
                case 154:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/GetGoldNeedle");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                */

                //맑은날 올빼미를 얻음(154-155)
                case 152:
                    //효과음재생
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_Number = 18;
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();
                    GameObject OwlCageFeed = Owl_Cage.transform.Find("OwlCageFeed").gameObject;
                    OwlCageFeed.SetActive(false);
                    GameObject OwlCageFull = Owl_Cage.transform.Find("OwlCageFull").gameObject;
                    OwlCageFull.SetActive(true);


                    //대사진행
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/OwlEnterCage");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                
                //나무씨앗이 심어져 있는 상황에서 장미씨앗 심었을 경우(154-155)
                case 154:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/WrongRose");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                //장미가 심어져 있는 상황에서 나무씨앗을 심을경우(156-158)
                case 156:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/WrongTree");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;


                //동화책을 완성했을시
                case 160:
                    StartCoroutine(Fadeing(true, 1.5f, false));
                    break;
                case 161:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/CompleteFairyTale");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    //StartCoroutine(Fadeing(true, 1.5f));
                    break;
                case 162:
                    Destroy(Grid.transform.Find("8-FullWateringCan").gameObject); //물뿌리개 삭제
                    statusofstage = 1;
                    GLManager.SaveGame(statusofstage);
                    StartCoroutine(Fadeing(true, 2.0f, false));
                    break;
                case 163:
                    Stage_1.SetActive(false);
                    MainStage.SetActive(true);
                    pumphandler.StartPump();
                    StartCoroutine(Fadeing(false, 2.0f, false));
                    SM.ChangeBGM_Fun("Stage0/TheVisitors");
                    break;
                    /*
                case 164:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/GoldClockBroke");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                    */
                case 166:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/PutOwlFeed");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 168:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/PutRoseSeed");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 170:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/PutTreeSeed");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 172:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/PutWater");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 174:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/PutOwlName");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 176:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/PutTreeName");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 178:
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/PutRoseName");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;


                //1-3 디폴트방에서 대사 넘기기
                case 180:
                    //동화책 단서 얻음
                    GameObject.Find("HintTextManager").GetComponent<TextChange>().ToUnlockTextNumberSet(6);
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/StartFairyTale");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 181:
                    StartCoroutine(Fadeing(true, 1.5f, false));
                    break;
                case 182:
                    //패널1-3디폴트 비활성화 시킴
                    Panel1_3_defalut.SetActive(false);
                    //1-3 BGM재생
                    //GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_Number = 11;
                    //GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_ListPlay();
                    //패널1-3 활성화시킴
                    Panel1_3.SetActive(true);
                    StartCoroutine(Fadeing(false, 1.5f, false));
                    break;

                case 184: //장미꽃에 물주었을때 대사
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/PutWaterToRose");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 186: //물뿌린 나무씨앗에 시계를 놓았을시 대사
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/PutClockToTree");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 188: //나무에 물을 안준상태에서 시계를 놓았을시
                    Text_Data = Resources.Load<TextAsset>("Stage1-3/EventDialogue/WrongClock");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                // 200~299 2Stage
                case 200:
                    StartCoroutine("WaitASecond", 1.25);               //이벤트 대기시키기 / 오른쪽 숫자가 초
                    break;
                case 201:                                               //로프획득
                    GameObject.Find("Circle_Puzzle_Control").transform.GetChild(9).gameObject.SetActive(true);
                    GameObject.Find("Circle_Puzzle_Control").transform.GetChild(9).gameObject.GetComponent<Fade>().StartFade();
                    break;
                case 202:
                    StartCoroutine(WaitASecond(1.0f));
                    break;
                case 203:
                    StartCoroutine(Fadeing(true, 3.0f, true));
                    break;
                case 204:
                    StartCoroutine("WaitASecond", 0.5);
                    GameObject.Find("Ganges_river").GetComponent<GangesRiver>().Activate_Door();
                    break;
                case 205:
                    Text_Data = Resources.Load<TextAsset>("2_Stage/Event_Script/Artrium_Script");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;
                case 206:
                    StartCoroutine(Fadeing(false, 3.0f, true));
                    break;
                case 207:
                    Text_Data = Resources.Load<TextAsset>("2_Stage/Event_Script/Door");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 209:
                    KWhandler.AccquiredKeyword(2, 1);
                    KWhandler.AccquiredKeyword(2, 2);
                    KWhandler.AccquiredKeyword(2, 3);
                    KWhandler.ControlChapter(2);
                    KWhandler.ChapterListSet();

                    statusofstage = 2;
                    GLManager.SaveGame(statusofstage);
                    StartCoroutine(Fadeing(true, 2.0f, false));
                    break;
                case 210:
                    Stage_2.SetActive(false);
                    MainStage.SetActive(true);
                    pumphandler.StartPump();
                    StartCoroutine(Fadeing(false, 2.0f, false));

                    SM.ChangeBGM_Fun("Stage0/TheVisitors");

                    break;

                // 300~399 3Stage
                // 바다에 처음갔을때 화이트 인아웃 
                case 350:
                    if (!FirstSea)          
                        StartCoroutine(Fadeing(true, 3.0f, true));
                    break;

                case 351:
					if (!FirstSea) 
					{
						Text_Data = Resources.Load<TextAsset>("3Stage/EventDialogue/1Round/FirstSea");
						Json_Data = JsonMapper.ToObject(Text_Data.text);
						CD.LoadJSON(Json_Data);
			
						FirstSea = true;
					}
					break;

                case 352:
                        StartCoroutine(Fadeing(false, 3.0f, true)); 
                    break;

                case 355:
                    Text_Data = Resources.Load<TextAsset>("3Stage/EventDialogue/1Round/Calender");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 357:
                    Text_Data = Resources.Load<TextAsset>("3Stage/EventDialogue/1Round/SeaRainPopup");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 359:
                    Text_Data = Resources.Load<TextAsset>("3Stage/EventDialogue/1Round/NomalRing");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                // 꽃밭 만들때 
                case 300:
                    StartCoroutine(Fadeing(true, 3.0f, true));
                    break;

                case 301:
					Text_Data = Resources.Load<TextAsset>("3Stage/EventDialogue/1Round/Flower");
					Json_Data = JsonMapper.ToObject(Text_Data.text);
					CD.LoadJSON(Json_Data);
					break;

               case 302:
                    StartCoroutine(Fadeing(false, 3.0f, true));
                    break;
                    

                case 304:
                    StartCoroutine(Fadeing(false, 3.0f, false));
                    GameObject.Find("Rain").SetActive(false);
					Stage3.Find ("1Round").gameObject.SetActive (false);
                    GameObject.Find("2_Inventory").GetComponent<Item>().DeleteInventory();
                    break;

				case 305:
                    Stage3.Find("2Round").gameObject.SetActive(true);
                    break;

				// *************************************************************************************



				case 310:
					//inventory.gameObject.SetActive (false);
					Text_Data = Resources.Load<TextAsset>("3Stage/EventDialogue/2Round/FinishRound");
					Json_Data = JsonMapper.ToObject(Text_Data.text);
					CD.LoadJSON(Json_Data);
                    CD.Dialogue_Text.GetComponent<Text>().fontStyle = FontStyle.Italic;
                    break;

				case 311:
                    statusofstage = 3;
                    GLManager.SaveGame(statusofstage);

                    StartCoroutine(Fadeing(true, 3.0f, true));
					Stage3.Find ("2Round").gameObject.SetActive (false);
					break;

				case 312:

                    //Stage3.Find ("3Round").gameObject.SetActive (true);
                    MainStage.gameObject.SetActive(true);
					StartCoroutine (Fadeing (false, 3.0f, true));
                    KWhandler.AccquiredKeyword(3, 1);
                    KWhandler.AccquiredKeyword(3, 2);
                    KWhandler.AccquiredKeyword(3, 3);
                    pumphandler.StartPump();
                    KWhandler.ControlChapter(3);
                    KWhandler.ChapterListSet();
                    statusofstage = 3;
                    GLManager.SaveGame(statusofstage);
                    //GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_NumberSet(303);
                    //GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_ListPlay();
                    CD.Dialogue_Text.GetComponent<Text>().fontStyle = FontStyle.Normal;
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().ChangeBGM_Fun("Stage0/TheVisitors");
                    break;

                // *************************************************************************************

                case 320:
					Text_Data = Resources.Load<TextAsset>("3Stage/EventDialogue/3Round/GetFeather");
					Json_Data = JsonMapper.ToObject(Text_Data.text);
					CD.LoadJSON(Json_Data);
					break;

				case 322:
					Text_Data = Resources.Load<TextAsset>("3Stage/EventDialogue/3Round/SleepJaguar");
					Json_Data = JsonMapper.ToObject(Text_Data.text);
					CD.LoadJSON(Json_Data);
                    break;

				case 324:
					Text_Data = Resources.Load<TextAsset>("3Stage/EventDialogue/3Round/OpenLock");
					Json_Data = JsonMapper.ToObject(Text_Data.text);
					CD.LoadJSON(Json_Data);
					break;

                // 3stage 클리어
                case 326:
                    StartCoroutine(Fadeing(true, 3.0f, true));
                    break;

                case 327:
                    Text_Data = Resources.Load<TextAsset>("3Stage/EventDialogue/3Round/Exit");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 328:
                    //StartCoroutine(Fadeing(false, 3.0f, true));
                    StartCoroutine(Fadeing(true, 3.0f, false));
                    break;


                case 329:
                    Stage3.gameObject.SetActive(false);
                    MainStage.gameObject.SetActive(true);
                    MainStage.transform.Find("Panel").Find("EndingButton").gameObject.SetActive(true);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSE_fun();
                    inventory.GetComponent<Item>().DeleteInventory();

                   
                    StartCoroutine(Fadeing(false, 3.0f, false));
                    break;

                // 엔딩
                case 399:
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound();
                    Text_Data = Resources.Load<TextAsset>("Ending/4StageEnd");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    
                    break;

                case 400:
                    KWhandler.AccquiredKeyword(4, 1);
                    KWhandler.AccquiredKeyword(4, 2);
                    pumphandler.StartPump();

                    statusofstage = 4;
                    GLManager.SaveGame(statusofstage);

                    KWhandler.ControlChapter(4);
                    KWhandler.ChapterListSet();
                    KWhandler.transform.Find("KeyWordControl").gameObject.SetActive(true);
                    //KWhandler.transform.Find("KeyWordControl").Find("Text").gameObject.SetActive(false);
                    KWhandler.transform.Find("KeyWordControl").Find("KWbutton").gameObject.SetActive(true);
                    break;

                case 402:
                    Text_Data = Resources.Load<TextAsset>("Ending/OrgelJaguar");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 403:
                    KWControl.gameObject.SetActive(false);
                    StartCoroutine(Fadeing(true, 3.0f, false));
                    break;

                case 404:
                    StartCoroutine(WaitASecond(2.0f));
                    break;

                case 405:
                    // 소리 추가하기, 노이즈 배경음악
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_NumberSet(400);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_ListPlay();
                    StartCoroutine(WaitASecond(4.0f));
                    break;

                case 406:
                    // 노이즈 1
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(400);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();

                    Noise.SetActive(true);                  

                    Text_Data = Resources.Load<TextAsset>("Ending/Noise1");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 407:
                    Noise.SetActive(false);
                    Noise.transform.localPosition = new Vector3(700.0f, 150.0f, 0.0f);
                    StartCoroutine(WaitASecond(2.0f));
                    break;

                case 408:
                    // 노이즈 2
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(401);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();

                    Noise.SetActive(true);
                    
                    Text_Data = Resources.Load<TextAsset>("Ending/Noise2");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 409:
                    Noise.SetActive(false);
                    Noise.transform.localPosition = new Vector3(-660.0f, 20.0f, 0.0f);
                    StartCoroutine(WaitASecond(2.0f));
                    break;

                case 410:
                    // 노이즈 3
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(402);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();

                    Noise.SetActive(true);
                   
                    Text_Data = Resources.Load<TextAsset>("Ending/Noise3");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 411:
                    Noise.SetActive(false);
                    Noise.transform.localPosition = new Vector3(-370.0f, 475.0f, 0.0f);
                    StartCoroutine(WaitASecond(2.0f));
                    break;

                case 412:
                    // 노이즈 4
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(403);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();

                    Noise.SetActive(true);                    

                    Text_Data = Resources.Load<TextAsset>("Ending/Noise4");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 413:
                    Noise.SetActive(false);
                    Noise.transform.localPosition = new Vector3(0.0f, 100.0f, 0.0f);
                    StartCoroutine(WaitASecond(3.5f));
                    break;

                case 414:
                    // 노이즈 5
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(404);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();

                    Noise.SetActive(true);                 

                    Text_Data = Resources.Load<TextAsset>("Ending/Noise5");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 415:              
                    Noise.SetActive(false);
                    MainStage.transform.Find("Panel").Find("NPC").gameObject.SetActive(false);
                    MainStage.transform.Find("Panel").Find("EndingNPCImage").gameObject.SetActive(true);              
                    MainStage.transform.Find("Panel").Find("EndingButton").gameObject.SetActive(false);
                    MainStage.transform.Find("Panel").Find("EndingButton2").gameObject.SetActive(true);
                    StartCoroutine(WaitASecondNotNum(4.0f));
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound();
                    StartCoroutine(Fadeing(false, 3.0f, false));
                    break;

                case 417:
                    // 임시로 BGM끄기
                    //GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound();
                    Text_Data = Resources.Load<TextAsset>("Ending/HideBox");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    MainStage.transform.Find("Panel").Find("HideBoxButton").gameObject.SetActive(true);
                    break;

                case 420:           
                    StartCoroutine(Fadeing(true, 3.0f, true));
                    StartCoroutine(WaitASecondNotNum(3.5f));
                    break;

                // 발걸음 소리
                case 421:  
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(405);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();
                    StartCoroutine(WaitASecond(5.0f));
                    break;

                case 422:
                    StartCoroutine(WaitASecond(1.0f));
                    break;

                // 문닫히는 소리
                case 423:
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(406);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();

                    MainStage.transform.Find("Panel").Find("OpenDoor").gameObject.SetActive(true);
                    StartCoroutine(WaitASecond(1.0f));
                    break;

                case 424:
                    StartCoroutine(Fadeing(false, 3.0f, true));
                    break;

                case 425:
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_NumberSet(401);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_ListPlay();
                    StartCoroutine(WaitASecond(1.0f));
                    break;

                case 426:
                    Text_Data = Resources.Load<TextAsset>("Ending/End1");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                // 총장전 소리
                case 427:
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(407);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();
                    StartCoroutine(WaitASecond(1.0f));
                    break;

                case 428:
                    Text_Data = Resources.Load<TextAsset>("Ending/End2");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 429:
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().StopSound();
                    StartCoroutine(WaitASecond(1.0f));
                    break;

                case 430:
                    Text_Data = Resources.Load<TextAsset>("Ending/End3");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 431:
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(408);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();
                    StartCoroutine(WaitASecond(2.0f));
                    break;

                case 432:
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_NumberSet(402);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().BGM_ListPlay();
                    StartCoroutine(WaitASecond(4.0f));
                    break;

                case 433:
                    Text_Data = Resources.Load<TextAsset>("Ending/End4");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                // 로한 발소리
                case 434:
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(405);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();
                    StartCoroutine(WaitASecond(4.0f));
                    break;

                case 435:
                    StartCoroutine(WaitASecond(1.0f));
                    break;

                case 436:
                    Text_Data = Resources.Load<TextAsset>("Ending/End5");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 437:
                    StartCoroutine(Fadeing(true, 3.0f, true));
                    break;

                case 438:
                    MainStage.transform.Find("Panel").Find("NPC").gameObject.SetActive(false);
                    MainStage.transform.Find("Panel").Find("EndingNPCImage").gameObject.SetActive(false);
                    MainStage.transform.Find("Panel").Find("Dead").gameObject.SetActive(true);
                    StartCoroutine(Fadeing(false, 3.0f, true));
                    break;

                case 440:
                    Text_Data = Resources.Load<TextAsset>("Ending/End6");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 441:
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(407);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();
                    StartCoroutine(WaitASecond(4.0f));
                    break;

                case 442:
                    Text_Data = Resources.Load<TextAsset>("Ending/End7");
                    Json_Data = JsonMapper.ToObject(Text_Data.text);
                    CD.LoadJSON(Json_Data);
                    break;

                case 443:
                    StartCoroutine(Fadeing(true, 3.0f, true));
                    break;

                case 444:
                    MainStage.transform.Find("Panel").Find("Gun").gameObject.SetActive(true);
                    MainStage.transform.Find("Panel").Find("Dead").gameObject.SetActive(false);
                    StartCoroutine(Fadeing(false, 3.0f, true));
                    break;

                case 450:
                    StartCoroutine(Fadeing(true, 3.0f, false));
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_NumberSet(410);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().SE_ListPlay();
                    break;

                // 이후 엔딩 크레딧


                case 999:
                    GLManager.LoadGame();
                    break;
                default:
	   	           break;
            }
            Event_Number_1 = Event_Number;
        }
    }

    IEnumerator WaitASecond(float sec)                //  이벤트 대기시키기
    {
        yield return new WaitForSeconds(sec);
 
        Event_Number++;
    }

    IEnumerator WaitASecondNotNum(float sec)                //  이벤트 대기시키기
    {
        yield return new WaitForSeconds(sec);
    }

    IEnumerator Fadeing(bool Showing, float duration, bool isWhite)
    {

        /*if (isInTransition)
            yield break;

        isInTransition = true;
        float dealtime = 255 / duration;
        if (isWhite)
            fadeImage.color = Color.white;
        else
            fadeImage.color = Color.black;

        while (isInTransition)
        {
            if(Showing)
            {
                fadeImage.color = 
            }
        }*/
        if (!isInTransition)
        {
            isInTransition = true;
            transition = (Showing) ? 0 : 1;
            float dealtime = 0.02f / duration;
            Color origincolor = fadeImage.color;
            while (isInTransition)
            {
                fadeImage_.SetActive(true);
                transition += (Showing) ? dealtime : -dealtime;
                if(isWhite)
                    fadeImage.color = Color.Lerp(new Color(255,255,255,0), Color.white, transition);
                else
                    fadeImage.color = Color.Lerp(new Color(0,0,0,0), Color.black, transition);

                if (transition > 1 || transition < 0)
                {
                    isInTransition = false;
                    Doing_Event = false;
                    if (!Showing)
                        fadeImage_.SetActive(false);
                    Event_Number++;
                    yield break;
                }
                yield return new WaitForSeconds(0.01f);
            }

        }
        yield break;

    }
    //이벤트숫자 받아서 변경해주는 함수
    public void EventnumberSet(int num)
    {
        //드림캐처를 완성했을시 자동으로 완성이벤트로 이동
        if (num == 105)
        {
            if (MakeDreamCatcher == true)
            {
                Event_Number = num;
                return;
            }
            else if (MakeDreamCatcher == false)
                return;
        }

        Event_Number = num;
    }

    public int getstageinfo()
    {
        return statusofstage;
    }
}
