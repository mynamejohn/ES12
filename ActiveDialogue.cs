using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using LitJson;


public class ActiveDialogue : MonoBehaviour
{
    TextAsset data;                    //Json파일을 문자열로 읽어들임
    public string EventName;        //실행시킬 대화 이벤트 파일의 이름

    public bool DestroyActivated;           //1번 실행하고 오브젝트를 지울것인지의 여부
    public bool WaitForClick;                       //버튼을 눌러 대화를 시작하는지의 여부  / false이면 자동(강제이벤트라던가) true이면 직접 다가가서 상호작용
    public bool isUpade;                        //업데이트 되었는지
    public bool NotNum = true;
    ControlDialogue control;                //대화창 제어

    JsonData ConvertedData;                 //data로 읽어들인 텍스트를 객체로 변환하여 저장

	// Use this for initialization
	void Start () {
        control = FindObjectOfType<ControlDialogue>();

        data = Resources.Load<TextAsset>(EventName);
        ConvertedData = JsonMapper.ToObject(data.text);//Json 객체로 변환

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Click_obj()
    {
        if (WaitForClick)
        {
            if (control.isActive == false)                            //충돌처리 구현필요
            {
                NotNum = true;
                control.LoadJSON(ConvertedData, WaitForClick);
                

                if (DestroyActivated)
                    Destroy(gameObject);
            }

            else
                return;
        }
        else
            return; 
    }
    public void SpecialItem()
    {
        if (WaitForClick)
        {
            if (control.isActive == false)                            //충돌처리 구현필요
            {
                NotNum = true;
                control.LoadJSON(ConvertedData, WaitForClick,true);

                if (DestroyActivated)
                    Destroy(gameObject);
            }

            else
                return;
        }
        else
            return;
    }

    public void SpecialDragItem(JsonData CData, bool Wait)
    {

        if (WaitForClick)
        {
            if (control.isActive == false)                            //충돌처리 구현필요
            {
                NotNum = true;
                control.LoadJSON(CData, Wait, true);

                if (DestroyActivated)
                    Destroy(gameObject);
            }

            else
                return;
        }
        else
            return;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (WaitForClick || control.isActive)
            return;
        else
        {
            if (other.name == "2_Player")
            {
                control.LoadJSON(ConvertedData, WaitForClick);
                if (DestroyActivated)
                      Destroy(gameObject);
            }
        }
    }
}
