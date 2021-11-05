using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMng : MonoBehaviour {

    public LineRenderer LNLine;
    public LongNote longNote;
    // Use this for initialization
    void Start () {
        SetLNLine();
    }
	
	// Update is called once per frame
	void Update () {

    }
    
    void SetLNLine()
    {
        LNLine.positionCount = longNote.LCirclepos.Count;
    }
}
