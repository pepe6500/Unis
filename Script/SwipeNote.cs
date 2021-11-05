using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwipeNote : Note
{
    Animator animator;
    GameObject SwipeObject;
    private UImanager uImanager;
    private Vector3 nowSizeVec3;
    private NoteManager noteManager;
    private int nSwipe;
    
    public GameObject[] SwipeWing = new GameObject[2];
    public Vector2[] firstWingVec2 = new Vector2[2];
    public float wingSpeed;
    public bool IsWingPos;
    public bool IsSwipe;
    public bool Isfirst;

    public override void Initialize()
    {
        SwipeWing[0].gameObject.SetActive(true);
        SwipeWing[1].gameObject.SetActive(true);
        nSwipe = 0;
        IsWingPos = false;
        IsSwipe = false;
        NoteName = "SwipeNote";
        SwipeObject = transform.GetChild(0).gameObject;
        uImanager = GameObject.Find("GameMng").GetComponent<UImanager>();
        noteManager = GameObject.Find("GameMng").GetComponent<NoteManager>();
        myParent = GameObject.Find("SwipeMng").GetComponent<Transform>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        GreatTime = PerfectTime - 0.25f;
        missTime = GreatTime - 0.5f;
        TouchJudgmentTime = 0;
        difficultyNum = 0;
        transform.localScale = new Vector2(0, 0);
        SwipeWing[0].transform.localPosition = new Vector2(-0.6f, 0);
        SwipeWing[1].transform.localPosition = new Vector2(0.6f, 0);
        //MaxPsize = 1.8f;
        //MaxCsize = 0.7f;
        StartCoroutine(firstDelay());
    }
    void Start()
    {
        Isfirst = true;
        Initialize();
    }

    public IEnumerator firstDelay()
    {
        yield return new WaitForSeconds(2f);
        Isfirst = false;
    }
    
    void Update()
    {
        //Addsizenote();
        TransWing();
        TouchJudgmentTime += Time.deltaTime;
        if (TouchJudgmentTime >= (PerfectTime))
        {
            if (uImanager.gradeText)
                uImanager.gradeText.text = noteData.noteInfo[2].notegrade.ToString();
            ObjectPool.instance.PushToPool(NoteName, gameObject, myParent);
            //gameObject.SetActive(false);
        }
        Touch tempTouch;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                tempTouch = Input.GetTouch(i);
                if (!IsSwipe)
                {
                    if (tempTouch.phase == TouchPhase.Moved)
                    {
                        noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nSwipe, tempTouch, IsSwipe);
                    }
                }
            }
        }
        //if (Input.GetMouseButtonDown(0) && !IsSwipe)
        //{
        //    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nSwipe, IsSwipe);
        //}

        if (!IsSwipe)
        {
            StaySizeUpNote();
        }
    }
    public void StaySizeUpNote()
    {
        if (transform.localScale.x < 0.8f)
        {
            Vector2 PnoteVec2 = new Vector2(transform.localScale.x + PNScalespeed[difficultyNum] * Time.deltaTime,
                                            transform.localScale.y + PNScalespeed[difficultyNum] * Time.deltaTime);
            transform.localScale = PnoteVec2;
        }
    }

    void TransWing()
    {
        if (SwipeWing[0].transform.localPosition.x >= gameObject.transform.GetChild(0).transform.localPosition.x - 1f && !IsWingPos)
        {
            Vector2 LwingPosVec2 = new Vector2(SwipeWing[0].transform.localPosition.x - wingSpeed * Time.deltaTime, 0);
            Vector2 RwingPosVec2 = new Vector2(SwipeWing[1].transform.localPosition.x + wingSpeed * Time.deltaTime, 0);
            SwipeWing[0].transform.localPosition = LwingPosVec2;
            SwipeWing[1].transform.localPosition = RwingPosVec2;
        }
        else
            IsWingPos = true;

        if (SwipeWing[0].transform.localPosition.x <= gameObject.transform.GetChild(0).transform.localPosition.x - 0.6f && IsWingPos)
        {
            Vector2 LwingPosVec2 = new Vector2(SwipeWing[0].transform.localPosition.x + (wingSpeed + 0.3f) * Time.deltaTime, 0);
            Vector2 RwingPosVec2 = new Vector2(SwipeWing[1].transform.localPosition.x - (wingSpeed + 0.3f) * Time.deltaTime, 0);
            SwipeWing[0].transform.localPosition = LwingPosVec2;
            SwipeWing[1].transform.localPosition = RwingPosVec2;
        }
    }
}












































/////////////////////////////////////////스와이프노트 애니메이션 only ///////////////////////////////////////////////////
//public class SwipeNote : Note
//{
//    Animator animator;
//    GameObject SwipeObject;
//    private UImanager uImanager;
//    private Vector3 nowSizeVec3;
//    private NoteManager noteManager;
//    private int nSwipe;

//    public bool IsSwipe;
//    public bool Isfirst;

//    public override void Initialize()
//    {
//        nSwipe = 0;
//        IsSwipe = false;
//        NoteName = "SwipeNote";
//        SwipeObject = transform.GetChild(0).gameObject;
//        uImanager = GameObject.Find("GameMng").GetComponent<UImanager>();
//        noteManager = GameObject.Find("GameMng").GetComponent<NoteManager>();
//        myParent = GameObject.Find("SwipeMng").GetComponent<Transform>();
//        animator = transform.GetChild(0).GetComponent<Animator>();
//        GreatTime = PerfectTime - 0.25f;
//        missTime = GreatTime - 0.5f;
//        TouchJudgmentTime = 0;
//        difficultyNum = 0;
//        transform.GetChild(0).localScale = new Vector2(0, 0);
//        //MaxPsize = 1.8f;
//        //MaxCsize = 0.7f;
//        StartCoroutine(firstDelay());
//    }

//    //private float MaxPsize;
//    // Use this for initialization
//    void Start()
//    {
//        Isfirst = true;
//        Initialize();
//    }

//    public IEnumerator firstDelay()
//    {
//        yield return new WaitForSeconds(2f);
//        Isfirst = false;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //Addsizenote();
//        TouchJudgmentTime += Time.deltaTime;
//        if (TouchJudgmentTime >= (PerfectTime))
//        {
//            if (uImanager.gradeText)
//                uImanager.gradeText.text = noteData.noteInfo[2].notegrade.ToString();
//            ObjectPool.instance.PushToPool(NoteName, gameObject, myParent);
//            //gameObject.SetActive(false);
//        }

//        //if (Input.GetMouseButtonDown(0))
//        //    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nSwipe);
//        Touch tempTouch;
//        if (Input.touchCount > 0)
//        {
//            for (int i = 0; i < Input.touchCount; i++)
//            {
//                tempTouch = Input.GetTouch(i);
//                if (!IsSwipe)
//                {
//                    if (tempTouch.phase == TouchPhase.Moved)
//                        noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nSwipe, tempTouch, IsSwipe);
//                }
//            }
//        }
//        //if (Input.GetMouseButtonDown(0) && !IsSwipe)
//        //{
//        //    Debug.Log("ddddddd " + nowSizeVec3.x);
//        //    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nSwipe, IsSwipe);
//        //}

//        if (!IsSwipe)
//        {
//            StaySizeUpNote();
//        }
//    }
//    public void StaySizeUpNote()
//    {
//        if (transform.GetChild(0).localScale.x < 0.5f && transform.GetChild(0).localScale.x < 0.5f)
//        {
//            Vector2 PnoteVec2 = new Vector2(transform.GetChild(0).localScale.x + PNScalespeed[difficultyNum] * Time.deltaTime,
//                                            transform.GetChild(0).localScale.y + PNScalespeed[difficultyNum] * Time.deltaTime);
//            transform.GetChild(0).localScale = PnoteVec2;
//        }
//    }
//}