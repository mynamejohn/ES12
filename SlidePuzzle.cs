using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzle : MonoBehaviour {

    //int width = 190;
    //int height = 206;

    public AudioClip ClearSound;
    public AudioManager AM;
    public AudioClip slideSound;
    EventManager EM;
    public GameObject SlidepuzzleControl;
    public SlidePuzzleTile[,] Tiles = new SlidePuzzleTile[4,3];
    SlidePuzzleTile blank_Tile;

    public Fade FadeImage_outside;

    public bool Answerd = false;
    public bool Activated = false;

    public GameObject Answerd_Obj;

    public BlindforGanges blind;

    // Use this for initialization
    void Start ()
    {
        EM = FindObjectOfType<EventManager>();
        SlidepuzzleControl = gameObject.transform.GetChild(0).gameObject;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Tiles[i, j] = gameObject.transform.GetChild(0).GetChild(0).GetChild((i * 3) + j).gameObject.GetComponent<SlidePuzzleTile>();
                Tiles[i, j].SetoriginPos(i, j);
            }
        }
        blank_Tile = Tiles[3, 2];
        blank_Tile.is_blank = true;

        Tiles[0, 0].SetPos(1, 2);
        Tiles[0, 1].SetPos(2, 0);
        Tiles[0, 2].SetPos(0, 2);
        Tiles[1, 0].SetPos(1, 1);
        Tiles[1, 1].SetPos(2, 2);
        Tiles[1, 2].SetPos(1, 0);   
        Tiles[2, 0].SetPos(2, 1);
        Tiles[2, 1].SetPos(3, 1);
        Tiles[2, 2].SetPos(0, 1);
        Tiles[3, 0].SetPos(3, 0);
        Tiles[3, 1].SetPos(3, 2);
        Tiles[3, 2].SetPos(0, 0);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!Activated)
            return;
        Check();
	}

    public void CheckDirection(SlidePuzzleTile SPT)
    {
        if (Answerd)
            return;
        if (SPT.Ver == blank_Tile.Ver && SPT.Hor == blank_Tile.Hor)
        {
            return;
        }
        if (blank_Tile.Ver == SPT.Ver)
        {
            if (blank_Tile.Hor - SPT.Hor == 1)
            {
                SPT.triggerMoving(3,1);
                blank_Tile.triggerMoving(1,1);
                SPT.Hor++;
                blank_Tile.Hor--;
            }
            else if (SPT.Hor - blank_Tile.Hor == 1)
            {
                SPT.triggerMoving(1, 1);
                blank_Tile.triggerMoving(3, 1);
                SPT.Hor--;
                blank_Tile.Hor++;
            }

            if (blank_Tile.Hor - SPT.Hor > 1)
            {
                int middle = blank_Tile.Hor - SPT.Hor;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Tiles[i, j].Ver == SPT.Ver && Tiles[i, j].Hor < blank_Tile.Hor&& Tiles[i, j].Hor >= SPT.Hor)
                        {
                            Tiles[i, j].triggerMoving(3, 1);
                            Tiles[i, j].Hor++;
                        }
                        continue;
                    }
                    continue;
                }
                Debug.Log("2↑");
                blank_Tile.triggerMoving(1, middle);
                blank_Tile.Hor -= middle;
            }


            else if (SPT.Hor - blank_Tile.Hor > 1)
            {
                int middle = SPT.Hor- blank_Tile.Hor;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Tiles[i, j].Ver == SPT.Ver && Tiles[i, j].Hor > blank_Tile.Hor && Tiles[i, j].Hor <= SPT.Hor) 
                        {
                            Tiles[i, j].triggerMoving(1, 1);
                            Tiles[i, j].Hor--;
                        }
                        continue;
                    }
                    continue;
                }
                Debug.Log("2↓");
                blank_Tile.triggerMoving(3, middle);
                blank_Tile.Hor += middle;
            }
        }

        if (blank_Tile.Hor == SPT.Hor)
        {
            if (blank_Tile.Ver - SPT.Ver == 1)
            {
                SPT.triggerMoving(2,1);
                blank_Tile.triggerMoving(0,1);
                SPT.Ver++;
                blank_Tile.Ver--;
            }
            else if (SPT.Ver - blank_Tile.Ver == 1)
            {
                SPT.triggerMoving(0,1);
                blank_Tile.triggerMoving(2,1);
                SPT.Ver--;
                blank_Tile.Ver++;
            }
            if (blank_Tile.Ver - SPT.Ver > 1)
            {
                int middle = blank_Tile.Ver - SPT.Ver;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Tiles[i, j].Hor == SPT.Hor && Tiles[i, j].Ver < blank_Tile.Ver && Tiles[i, j].Ver >= SPT.Ver)
                        {
                            Tiles[i, j].triggerMoving(2, 1);
                            Tiles[i, j].Ver++;
                        }
                        continue;
                    }
                    continue;
                }
                Debug.Log("2<-");
                blank_Tile.triggerMoving(0, middle);
                blank_Tile.Ver -= middle;
            }
            else if (SPT.Ver - blank_Tile.Ver > 1) 
            {
                int middle = SPT.Ver - blank_Tile.Ver;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (Tiles[i, j].Hor == SPT.Hor && Tiles[i, j].Ver > blank_Tile.Ver && Tiles[i, j].Ver <= SPT.Ver)
                        {
                            Tiles[i, j].triggerMoving(0, 1);
                            Tiles[i, j].Ver--;
                        }
                        continue;
                    }
                    continue;
                }
                blank_Tile.triggerMoving(2, middle);
                blank_Tile.Ver += middle;
            }
        }
        return;
    }

    public void Turn_ON()
    {
        if (EM.Doing_Event)
            return;
        Activated = true;
        SlidepuzzleControl.SetActive(Activated);
        EM.Doing_Event = true;
        return;
    }
    public void Turn_OFF()
    {
        Activated = false;
        SlidepuzzleControl.SetActive(Activated);
        EM.Doing_Event = false;
        return;
    }

    void Check()
    {
        if (Answerd)
            return;

        bool Checking = false;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Checking = Tiles[i, j].Check_Tile();
                if (Checking)
                    continue;
                else
                    return;
            }
        }

        AM.PlaySound(ClearSound);
        Answerd = true;
        FadeImage_outside.StartFade();
        blank_Tile.StartFade(Answerd_Obj);
        blind.PuzzleSolved();
    }
    public void cheat()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {   
                Tiles[i, j].cheat();
            }
        }
    }

}
