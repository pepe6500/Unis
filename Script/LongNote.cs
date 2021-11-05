using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : Note
{
    public List<Vector3> LCirclepos;
    public GameObject Pnote, Cnote;
    protected Color Pcolor, Ccolor;
    protected float MaxPsize, MaxCsize;
    protected bool IsaddPnote, IsaddCnote;
    protected float alpha;
    protected float alphaTimer;

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
