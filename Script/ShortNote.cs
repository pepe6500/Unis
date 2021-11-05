using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShortNote : Note
{
    public GameObject[] Circles = new GameObject[2];
    public float MaxPsize, MaxCsize;
    public bool Isfirst;


    private float alpha;
    private Color[] CircleColors = new Color[2];
    private bool IsaddPnote, IsaddCnote;
    private UImanager uImanager;
    private NoteManager noteManager;
    private int nShort;


    public override void Initialize()
    {
        alpha = 0;
        for(int i = 0; i < CircleColors.Length; i++)
        {
            CircleColors[i] = Circles[i].GetComponent<SpriteRenderer>().color;
            CircleColors[i] = new Color(CircleColors[i].r, CircleColors[i].g, CircleColors[i].b, alpha);
            Circles[i].GetComponent<SpriteRenderer>().color = CircleColors[i];
        }
        NoteName = "ShortNote";
        MaxPsize = 1f;
        MaxCsize = 0.97f;
        nShort = 0;
        noteManager = GameObject.Find("GameMng").GetComponent<NoteManager>();
        uImanager = GameObject.Find("GameMng").GetComponent<UImanager>();
        myParent = GameObject.Find("ShortMng").GetComponent<Transform>();
        GreatTime = PerfectTime - 0.25f;
        missTime = GreatTime - 0.5f;
        TouchJudgmentTime = 0;
        difficultyNum = 0;
        for(int i = 0; i < Circles.Length; i++)
            Circles[i].transform.localScale = new Vector2(0, 0);
        IsaddPnote = false;
        IsaddCnote = false;
        StartCoroutine(IEAddsizenote());
    }

    //private float MaxPsize;
    // Use this for initialization
    void Start () {
        Isfirst = true;
        Initialize();
    }
	
	// Update is called once per frame
	void Update () {
        Addsizenote();
        TouchJudgmentTime += Time.deltaTime;
        if (TouchJudgmentTime >= (PerfectTime))
        {
            ObjectPool.instance.PushToPool(NoteName, gameObject, myParent);
            //gameObject.SetActive(false);
        }

        //if (Input.GetMouseButtonDown(0))
        //    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nShort);


        Touch tempTouch;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                tempTouch = Input.GetTouch(i);
                if (tempTouch.phase == TouchPhase.Began)
                {
                    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nShort, tempTouch);
                }
            }
        }
    }

    public IEnumerator IEAddsizenote()
    {
        IsaddPnote = true;
        yield return new WaitForSeconds(sizeDelay[difficultyNum]);
        Isfirst = false;
        IsaddCnote = true;
    }


    public void Addsizenote()
    {
        ChangeAlpha();
        if (IsaddPnote && Circles[1].transform.localScale.x <= MaxPsize && Circles[1].transform.localScale.y <= MaxPsize)
        {
            Vector2 PnoteVec2 = new Vector2(Circles[1].transform.localScale.x + PNScalespeed[difficultyNum] * Time.deltaTime,
                                            Circles[1].transform.localScale.y + PNScalespeed[difficultyNum] * Time.deltaTime);
            Circles[1].transform.localScale = PnoteVec2;
        }
        if (IsaddCnote && Circles[0].transform.localScale.x <= MaxCsize && Circles[0].transform.localScale.y <= MaxCsize)
        {
            Vector2 CnoteVec2 = new Vector2(Circles[0].transform.localScale.x + CNScalespeed[difficultyNum] * Time.deltaTime,
                                            Circles[0].transform.localScale.y + CNScalespeed[difficultyNum] * Time.deltaTime);
            Circles[0].transform.localScale = CnoteVec2;
        }
    }

    public void ChangeAlpha()
    {
        alpha += Time.deltaTime;
        if (alpha < 1)
        {
            for (int i = 0; i < CircleColors.Length; i++)
            {
                CircleColors[i] = new Color(CircleColors[i].r, CircleColors[i].g, CircleColors[i].b, alpha);
                //Debug.Log(Pcolor.a);
                Circles[i].GetComponent<SpriteRenderer>().color = CircleColors[i];
            }
        }
    }
}









//////////////////////////////////////////////////////////////////////////단노트 1번(날개달린 마름모)///////////////////////////////////////////////////////////
//public class ShortNote : Note
//{
//    public GameObject bottomCicle;
//    public GameObject[] ShortWing = new GameObject[2];
//    public Vector2[] firstWingVec2 = new Vector2[2];
//    public float wingSpeed;
//    public bool IsWingPos;


//    private UImanager uImanager;
//    private NoteManager noteManager;
//    private int nShort;
//    private float alpha;
//    private Color[] wingColor = new Color[2];
//    private float alphaTimer;

//    public override void Initialize()
//    {
//        Debug.Log(ShortWing[0].transform.localPosition.y);
//        alpha = 0;
//        nShort = 0;
//        NoteName = "ShortNote";
//        wingSpeed = 0.9f;
//        for (int i = 0; i < ShortWing.Length; i++)
//        {
//            //ShortWing[i] = gameObject.transform.GetChild(i + 1).gameObject;
//            wingColor[i] = ShortWing[i].GetComponent<SpriteRenderer>().color;
//        }
//        for (int i = 0; i < wingColor.Length; i++)
//        {
//            wingColor[i] = new Color(wingColor[i].r, wingColor[i].g, wingColor[i].b, alpha);
//            ShortWing[i].GetComponent<SpriteRenderer>().color = wingColor[i];
//        }
//        noteManager = GameObject.Find("GameMng").GetComponent<NoteManager>();
//        uImanager = GameObject.Find("GameMng").GetComponent<UImanager>();
//        myParent = GameObject.Find("ShortMng").GetComponent<Transform>();
//        GreatTime = PerfectTime - 0.25f;
//        missTime = GreatTime - 0.5f;
//        TouchJudgmentTime = 0;
//        difficultyNum = 0;
//        if (IsWingPos)
//        {
//            for (int i = 0; i < ShortWing.Length; i++)
//                ShortWing[i].transform.localPosition = firstWingVec2[i];
//            IsWingPos = false;
//        }
//    }

//    //private float MaxPsize;
//    // Use this for initialization
//    void Start () {
//        Initialize();
//    }
	
//	// Update is called once per frame
//	void Update () {
//        AlphaWing();
//        //Addsizenote();
//        TouchJudgmentTime += Time.deltaTime;
//        if (TouchJudgmentTime >= (PerfectTime))
//        {
//            ObjectPool.instance.PushToPool(NoteName, gameObject, myParent);
//            //gameObject.SetActive(false);
//        }

//        //if(Input.GetMouseButtonDown(0))
//        //    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nShort);
//        Touch tempTouch;
//        if (Input.touchCount > 0)
//        {
//            for (int i = 0; i < Input.touchCount; i++)
//            {
//                tempTouch = Input.GetTouch(i);
//                if (tempTouch.phase == TouchPhase.Began)
//                    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nShort);
//            }
//        }
//    }

//    void TransWing()
//    {

//        if (ShortWing[0].transform.position.x < gameObject.transform.position.x - 0.5f)
//        {
//            Vector2 LwingPosVec2 = new Vector2(ShortWing[0].transform.localPosition.x + wingSpeed * Time.deltaTime, 0);
//            Vector2 RwingPosVec2 = new Vector2(ShortWing[1].transform.localPosition.x - wingSpeed * Time.deltaTime, 0);
//            ShortWing[0].transform.localPosition = LwingPosVec2;
//            ShortWing[1].transform.localPosition = RwingPosVec2;
//            if(!IsWingPos)
//                IsWingPos = true;
//        }
//    }

//    void AlphaWing()
//    {
//        alpha += Time.deltaTime;
//        for (int i = 0; i < wingColor.Length; i++)
//        {
//            wingColor[i] = new Color(wingColor[i].r, wingColor[i].g, wingColor[i].b, alpha);
//            //Debug.Log(Pcolor.a);
//            ShortWing[i].GetComponent<SpriteRenderer>().color = wingColor[i];
//        }
//        TransWing();
//    }
//}
