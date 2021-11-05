using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {

    public GameObject ShortNote;
    public GameObject LpCircle;
    public GameObject SwipeNote;

    public Transform SnoteParent1;
    public Transform LnoteParent1;
    public Transform SwnoteParent1;

    public Transform SnoteParent2;
    public Transform LnoteParent2;
    public Transform SwnoteParent2;

    public Text gradeText;
    public LineMng lineMng;

    public int nCreateLong;
    public int nCreateSwipe;

    public bool isgenerateLine;

    // Use this for initialization
    void Start () {
        nCreateLong = 0;
        nCreateSwipe = 0;
        Screen.SetResolution(1920, 1080, true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //IEnumerator LcCircleCreateDelay(int ncCircle)
    //{
    //    Debug.Log("ffffff");
    //    for (int i = 1; i < ncCircle; i++)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        GameObject LCirclec = Instantiate(LcCircle, LnoteParent);
    //        LCirclec.transform.position = longNote.LCirclepos[i];
    //    }
    //}

    public void CreateShortNote(Vector3 _position)
    {
        GameObject shortnote = ObjectPool.instance.PopFromPool("ShortNote", SnoteParent1, SnoteParent2, ShortNote.transform.GetChild(0).gameObject.GetComponent<Animator>());
        if (!shortnote.GetComponent<ShortNote>().Isfirst)
            shortnote.GetComponent<ShortNote>().Initialize();
        ////단노트1(날개 마름모)////////
        //if (shortnote.GetComponent<ShortNote>().IsWingPos)
        //    shortnote.GetComponent<ShortNote>().Initialize();
        shortnote.transform.localPosition = _position;
    }

    public void CreateLongNote(Vector2[] _positions)
    {
        nCreateLong += 1;
        LineRenderer Line = Instantiate(lineMng.LNLine, LnoteParent2);
        //Line.gameObject.GetComponent<LongLine>().Initialize();
        Line.gameObject.name = "LongLine" + nCreateLong.ToString();
        Line.GetComponent<LongLine>().vectors = _positions;
        GameObject LCirclep = ObjectPool.instance.PopFromPool("LongPNote", Line.transform, Line.transform, null);
        //LCirclep.GetComponent<LongPCircle>().Initialize();
        LCirclep.transform.localPosition = _positions[0];
        LCirclep.GetComponent<LongPCircle>().vectors = _positions;
        LCirclep.GetComponent<LongPCircle>().ncCircle = _positions.Length;
        //Debug.Log(LCirclep.transform.localPosition);
    }

    public void CreateSwipeNote(Vector2 _vector2)
    {
        nCreateSwipe += 1;
        GameObject SwCircle = ObjectPool.instance.PopFromPool("SwipeNote", SwnoteParent1, SwnoteParent2, SwipeNote.transform.GetChild(0).gameObject.GetComponent<Animator>());
        if (!SwipeNote.GetComponent<SwipeNote>().Isfirst)
            SwCircle.GetComponent<SwipeNote>().Initialize();
        SwCircle.transform.localPosition = _vector2;
    }

    public void Button1()
    {
        CreateShortNote(new Vector2(2, 2));
        CreateShortNote(new Vector2(4, 2));
    }

    public void Button2()
    {
        List<Vector2> vectors = new List<Vector2>
        {
            new Vector2(2, 2),
            new Vector2(2, 0),
            new Vector2(0, 0),
            new Vector2(0, 2),
            new Vector2(-2, 2),
            new Vector2(-4, 2),
            new Vector2(-4, 0),
            new Vector2(-4, -2)
        };
        CreateLongNote(vectors.ToArray());
    }

    public void Button3()
    {
        CreateSwipeNote(new Vector2(3, 3));
        CreateSwipeNote(new Vector2(0, 3));
    }
}