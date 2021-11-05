using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPCircle : LongNote
{
    private LongLine longline;
    private UImanager uImanager;
    private NoteManager noteManager;
    private int nPLong;
    private bool IsPsize;
    private Animator animator;

    public Vector2[] vectors;
    public GameObject LcCircle;
    public int ncCircle;
    public float followTime;
    public float CreatecCircleTime;
    public float followSpeed;
    RaycastHit2D hit;

    public GameObject[] LCirclec;

    private bool IsEndFollow;


    public override void Initialize()
    {
        LCirclec = new GameObject[vectors.Length - 1];
        IsPsize = false;
        IsEndFollow = false;
        nPLong = 0;
        NoteName = "LongPNote";
        GreatTime = PerfectTime - 0.25f;
        missTime = GreatTime - 0.5f;
        alphaTimer = 0;
        alpha = 1f;
        difficultyNum = 0;
        MaxPsize = 0.7f;
        MaxCsize = 0.67f;
        Pcolor = Pnote.GetComponent<SpriteRenderer>().color;
        Ccolor = Cnote.GetComponent<SpriteRenderer>().color;
        animator = gameObject.GetComponent<Animator>();
        noteManager = GameObject.Find("GameMng").GetComponent<NoteManager>();
        uImanager = GameObject.Find("GameMng").GetComponent<UImanager>();
        longline = GameObject.Find("LongLine" + uImanager.nCreateLong).GetComponent<LongLine>();
        StartCoroutine(IEsizeUp(hit));
        StartCoroutine(LcCircleCreateDelay(ncCircle));
        StartCoroutine(Set());
    }

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        sizeUp(NoteName, hit);
        TouchJudgmentTime += Time.deltaTime;
        if (longline.IsFollowCircle)
            StartCoroutine(FollowLine());

        //if (Input.GetMouseButtonDown(0))
        //    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nPLong);

        Touch tempTouch;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                tempTouch = Input.GetTouch(i);
                if (tempTouch.phase == TouchPhase.Began)
                {
                    noteManager.Clicknote(NoteName, PerfectTime, GreatTime, missTime, TouchJudgmentTime, noteData, nPLong, tempTouch);
                    if(IsPsize)
                    {
                        animator.SetBool("Click", true);
                    }
                }
                if (tempTouch.phase == TouchPhase.Ended)
                    animator.SetBool("Click", false);
            }
        }
    }

    public IEnumerator IEsizeUp(RaycastHit2D hit)
    {
        IsaddPnote = true;
        yield return new WaitForSeconds(sizeDelay[difficultyNum]);
        IsaddCnote = true;
    }

    public void sizeUp(string poolName, RaycastHit2D hit)
    {
        if (IsaddPnote && Pnote.transform.localScale.x <= MaxPsize && Pnote.transform.localScale.y <= MaxPsize)
        {
            Vector2 PnoteVec2 = new Vector2(Pnote.transform.localScale.x + PNScalespeed[difficultyNum] * Time.deltaTime,
                                            Pnote.transform.localScale.y + PNScalespeed[difficultyNum] * Time.deltaTime);
            Pnote.transform.localScale = PnoteVec2;
        }
        else
            IsPsize = true;
        if (IsaddCnote && Cnote.transform.localScale.x <= MaxCsize && Cnote.transform.localScale.y <= MaxCsize && IsPsize)
        {
            Vector2 CnoteVec2 = new Vector2(Cnote.transform.localScale.x + CNScalespeed[difficultyNum] * Time.deltaTime,
                                            Cnote.transform.localScale.y + CNScalespeed[difficultyNum] * Time.deltaTime);
            Cnote.transform.localScale = CnoteVec2;
        }
        if (IsEndFollow)
        {
            alphaTimer += Time.deltaTime;
            if (alpha > 0 && alphaTimer >= deleteDelay[difficultyNum])
            {
                alpha -= deleteSpeed[difficultyNum] * Time.deltaTime;
                Pcolor = new Color(Pcolor.r, Pcolor.g, Pcolor.b, alpha);
                Ccolor = new Color(Ccolor.r, Ccolor.g, Ccolor.b, alpha);
                Pnote.GetComponent<SpriteRenderer>().color = Pcolor;
                Cnote.GetComponent<SpriteRenderer>().color = Ccolor;
            }
            else if (alpha <= 0)
            {
                alphaTimer = 0;
                //ObjectPool.instance.PushToPool(poolName, gameObject, gameObject.transform);
            }
        }
    }


    IEnumerator Set()
    {
        yield return new WaitForSeconds(followTime);
        longline.IsFollowCircle = true;
        longline.IsFollowLine = true;
    }


    IEnumerator FollowLine()
    {
        longline.IsFollowCircle = false;
        //transform.localPosition = vectors[0];
        Vector3 newpos;
        for (int i = 0; i < vectors.Length - 1; i++)
        {
            float t = 0;
            float time = 1f;
            Vector3 orig = transform.localPosition;
            Vector3 orig2 = vectors[i + 1];
            for (; t < time; t += Time.deltaTime * followSpeed)
            {
                newpos = Vector3.Lerp(orig, orig2, t / time);
                transform.localPosition = newpos;
                yield return null;
            }
        }
        IsEndFollow = true;
    }

    IEnumerator LcCircleCreateDelay(int ncCircle)
    {
        for (int i = 1; i < ncCircle; i++)
        {
            yield return new WaitForSeconds(CreatecCircleTime);
            LCirclec[i - 1] = ObjectPool.instance.pCPopFromPool("LongCNote", i, gameObject.transform.parent.gameObject.transform, gameObject.transform.parent.gameObject.transform, null);
            //LCirclec[i - 1].gameObject.GetComponent<LongCCircle>().Initialize();
            LCirclec[i - 1].transform.position = vectors[i];
            Debug.Log(LCirclec[i - 1].transform.localPosition.x);
        }
    }
}
