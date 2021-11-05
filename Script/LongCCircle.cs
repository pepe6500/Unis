using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongCCircle : LongNote
{
    private UImanager uImanager;
    private NoteManager noteManager;
    private int nCLong;
    RaycastHit2D hit;
    // Use this for initialization

    public override void Initialize()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        nCLong = 0;
        NoteName = "LongCNote";
        uImanager = GameObject.Find("GameMng").GetComponent<UImanager>();
        noteManager = GameObject.Find("GameMng").GetComponent<NoteManager>();
        GreatTime = PerfectTime - 0.25f;
        missTime = GreatTime - 0.5f;
        alphaTimer = 0;
        alpha = 1f;
        difficultyNum = 0;
        MaxPsize = 0.5f;
        MaxCsize = 0.5f;
        Pcolor = Pnote.GetComponent<SpriteRenderer>().color;
        Ccolor = Cnote.GetComponent<SpriteRenderer>().color;
        StartCoroutine(IEsizeUp());
    }

    void Start()
    {
        Initialize();
    }
	
	// Update is called once per frame
	void Update () {
        sizeUp(NoteName);
        TouchJudgmentTime += Time.deltaTime;

        //if (Input.GetMouseButtonDown(0))
        //    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nCLong);

        Touch tempTouch;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                tempTouch = Input.GetTouch(i);
                if (tempTouch.phase == TouchPhase.Began)
                    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nCLong, tempTouch);
                else if(tempTouch.phase == TouchPhase.Moved)
                    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nCLong, tempTouch);
            }
        }
    }

    public IEnumerator IEsizeUp()
    {
        IsaddPnote = true;
        yield return new WaitForSeconds(sizeDelay[difficultyNum]);
        IsaddCnote = true;
    }

    public void sizeUp(string poolName)
    {
        if (IsaddPnote && Pnote.transform.localScale.x <= MaxPsize && Pnote.transform.localScale.y <= MaxPsize)
        {
            Vector2 PnoteVec2 = new Vector2(Pnote.transform.localScale.x + PNScalespeed[difficultyNum] * Time.deltaTime,
                                            Pnote.transform.localScale.y + PNScalespeed[difficultyNum] * Time.deltaTime);
            Pnote.transform.localScale = PnoteVec2;
        }
        if (IsaddCnote && Cnote.transform.localScale.x <= MaxCsize && Cnote.transform.localScale.y <= MaxCsize)
        {
            Vector2 CnoteVec2 = new Vector2(Cnote.transform.localScale.x + CNScalespeed[difficultyNum] * Time.deltaTime,
                                            Cnote.transform.localScale.y + CNScalespeed[difficultyNum] * Time.deltaTime);
            Cnote.transform.localScale = CnoteVec2;
        }
        if (Pnote.transform.localScale.x >= MaxPsize && Pnote.transform.localScale.y >= MaxPsize
            && Cnote.transform.localScale.x >= MaxCsize && Cnote.transform.localScale.y >= MaxCsize)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            //alphaTimer += Time.deltaTime;
            //if (alpha > 0 && alphaTimer >= deleteDelay[difficultyNum])
            //{
            //    alpha -= deleteSpeed[difficultyNum] * Time.deltaTime;
            //    Pcolor = new Color(Pcolor.r, Pcolor.g, Pcolor.b, alpha);
            //    Ccolor = new Color(Ccolor.r, Ccolor.g, Ccolor.b, alpha);
            //    Pnote.GetComponent<SpriteRenderer>().color = Pcolor;
            //    Cnote.GetComponent<SpriteRenderer>().color = Ccolor;
            //}
            //else if (alpha <= 0)
            //{
            //    alphaTimer = 0;
            //    //ObjectPool.instance.PushToPool(poolName, gameObject);
            //}
        }
    }
}
