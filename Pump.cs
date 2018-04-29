using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pump : MonoBehaviour
{
    public bool Active;
    bool sizing = false;
    IEnumerator CoroutineforPump;
    public bool isActive = false;
    public bool pumping = false;
    public GameObject pumpobject;

    private void Awake()
    {
        CoroutineforPump = Pumping();
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(CoroutineforPump);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartPump()
    {
        StartCoroutine(CoroutineforPump);
        pumpobject.gameObject.SetActive(true);
    }

    public void EndPump()
    {
        StopCoroutine(CoroutineforPump);
        pumpobject.gameObject.SetActive(false);
    }
    public void Trun(bool turn)
    {
        pumpobject.gameObject.SetActive(turn);
    }
    IEnumerator Pumping()
    {
        pumping = true;
        while (true)
        {
            if (sizing)
            {
                pumpobject.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
                if (pumpobject.gameObject.transform.localScale.x > 1.5)
                    sizing = false;
            }
            else
            {
                pumpobject.transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
                if (pumpobject.gameObject.transform.localScale.x < 1.0)
                    sizing = true;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
