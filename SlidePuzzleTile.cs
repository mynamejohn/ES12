using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlidePuzzleTile : MonoBehaviour {

    AudioManager AM;
    SlidePuzzle SP;
    public int Hor;    //가로
    public int Ver;    //세로
    int OriginHor;
    int OriginVer;
    public MouseCursor Cs;

    int direction; // 왼 - 위 - 오 - 아 : 0 - 1 - 2 - 3
    public bool is_blank = false;

    // Use this for initialization
    void Start()
    {
        AM = FindObjectOfType<AudioManager>();
        SP = FindObjectOfType<SlidePuzzle>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        SP.CheckDirection(this);
        return;
    }
    private void OnMouseEnter()
    {
        Cs.PointerChangtoEyes();
    }
    private void OnMouseExit()
    {
        Cs.PointerChangetoNormal();
    }
    public void triggerMoving(int dir, int block)
    {
        direction = dir;
        StartCoroutine(moving(direction,block));
    }

    IEnumerator moving(int direction, int block)
    {
        int i = 0;
        AM.PlaySound(SP.slideSound);
        switch(direction)
        {
            case 0:
                for (i = 0; i < 38; i++)
                {
                    Vector3 Vec = gameObject.transform.localPosition;
                    Vec.x -= (float)5*block;
                    gameObject.transform.localPosition = Vec;
                    yield return new WaitForSecondsRealtime(0.001f);
                }
                break;
            case 1:
                for (i = 0; i < 41; i++)
                {
                    Vector3 Vec = gameObject.transform.localPosition;
                    Vec.y += (float)5 * block;
                    gameObject.transform.localPosition = Vec;
                    yield return new WaitForSecondsRealtime(0.001f);
                }
                break;
            case 2:
                for (i = 0; i < 38; i++)
                {
                    Vector3 Vec = gameObject.transform.localPosition;
                    Vec.x += (float)5 * block;

                    gameObject.transform.localPosition = Vec;

                    yield return new WaitForSecondsRealtime(0.001f);
                }
                break;  
            case 3:
                for (i = 0; i < 41; i++)
                {
                    Vector3 Vec = gameObject.transform.localPosition;
                    Vec.y -= (float)5 * block;
                    gameObject.transform.localPosition = Vec;

                    yield return new WaitForSecondsRealtime(0.001f);
                }
                break;
        }

        yield break;
    }

    public void SetPos(int i, int j)
    {
        Hor = i;
        Ver = j;
        gameObject.transform.localPosition = new Vector2((-191 + (190 * j)), 288 - (206 * i));
    }
    public void SetoriginPos(int i, int j)
    {
        Hor = OriginHor = i;
        Ver = OriginVer = j;
        gameObject.transform.localPosition = new Vector2((-191 + (190 * j)), 288 - (206 * i));
    }
    public bool Check_Tile()
    {
        if (Hor == OriginHor && Ver == OriginVer)
            return true;
        else
            return false;
    }

    public void StartFade(GameObject Answerd_Obj)
    {
        StartCoroutine(Fading(Answerd_Obj));
    }
    IEnumerator Fading(GameObject Answerd_Obj)
    {
        Image AC = GetComponent<Image>();
        for(float i=0;i<=1;i+=0.1f)
        {
            AC.color = new Color(AC.color.r, AC.color.g, AC.color.b, i);
            yield return new WaitForSeconds(0.01f);
        }
        Answerd_Obj.SetActive(true);
        Answerd_Obj.GetComponent<Fade>().StartFade();
        yield break;
    }
    public void cheat()
    {
        Hor = OriginHor;
        Ver = OriginVer;
    }

}