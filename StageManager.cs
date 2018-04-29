using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager : MonoBehaviour {
    public GameObject computerObjeect;
    public EventManager EM;
    public GameObject[] Stage_active = new GameObject[4];
    public GameObject[] Stage_deactive = new GameObject[4];
    public GameObject[] Stage_complete = new GameObject[4];

    public AudioManager AM;

    int stageinfo = 0;
    // Use this for initialization
    private void Awake()
    {
        stageinfo = EM.getstageinfo();

        Stage_active[0].SetActive(false);
        Stage_active[1].SetActive(false);
        Stage_active[2].SetActive(false);
        Stage_active[3].SetActive(false);

        Stage_deactive[0].SetActive(true);
        Stage_deactive[1].SetActive(true);
        Stage_deactive[2].SetActive(true);
        Stage_deactive[3].SetActive(true);

        Stage_complete[0].SetActive(false);
        Stage_complete[1].SetActive(false);
        Stage_complete[2].SetActive(false);
        Stage_complete[3].SetActive(false);
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetStage()
    {
        stageinfo = EM.getstageinfo();
        Debug.Log(stageinfo);
        if (stageinfo != 0)
        {
            for (int i = 0; i < stageinfo; i++)
            {
                Stage_active[i].SetActive(false);
                Stage_deactive[i].SetActive(false);
                Stage_complete[i].SetActive(true);
            }

            Stage_active[stageinfo].SetActive(true);
            Stage_deactive[stageinfo].SetActive(false);
        }
        computerObjeect.SetActive(true);
        AM.PlayAudio("Stage0/OpenStage");
    }

    public void FirstAccess()
    {
        Stage_deactive[0].SetActive(false);
        Stage_active[0].SetActive(true);
    }
}
