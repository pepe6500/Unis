using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Note : MonoBehaviour {
    
    public float[] PNScalespeed = new float[3], CNScalespeed = new float[3];
    public float[] sizeDelay = new float[3];
    public float[] deleteSpeed = new float[3], deleteDelay = new float[3];
    public NoteData noteData;
    public int difficultyNum;
    public float PerfectTime;
    public float TouchJudgmentTime;
    public string NoteName;
    public Transform myParent;

    protected float GreatTime, missTime;

    // Use this for initialization
    void Start () {
        ChangeStringE();
    }
	
	// Update is called once per frame
	void Update () {

    }

    //public abstract void Addsizenote(RaycastHit2D hit);
    //public abstract IEnumerator IEAddsizenote(RaycastHit2D hit);
    //public abstract void Clicknote();

    public abstract void Initialize();

    void ChangeStringE()
    {
        for (int i = 0; i < noteData.noteInfo.Length; i++)
        {
            StringEnum.GetStringValue(noteData.noteInfo[i].notegrade);
        }
    }

    //public abstract void InstantiateParticle(ParticleSystem particle, Vector3 particlePos);
    //public abstract void DestroyNote(GameObject note);
}
