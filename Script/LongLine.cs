using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongLine : MonoBehaviour {
    
    public bool IsFollowCircle;
    public bool IsFollowLine;
    public float LineSpeed;
    private string LineName;
    private float counter;
    private float[] dist;
    private Transform myParent;


    private LineRenderer myLine;
    public Vector2[] vectors;

    public void Initialize()
    {
        IsFollowCircle = false;
        IsFollowLine = false;
        LineName = "LongLine";
        myParent = GameObject.Find("LnoteMng").GetComponent<Transform>();
        myLine = gameObject.GetComponent<LineRenderer>();
        dist = new float[vectors.Length];
        for (int i = 0; i < vectors.Length - 1; i++)
        {
            dist[i] = Vector3.Distance(vectors[i], vectors[i + 1]);
        }
        StartCoroutine(LineDraw());
    }

    // Use this for initialization
    void Start () {
        Initialize();
    }
	
	// Update is called once per frame
	void Update () {
        if (IsFollowLine)
            StartCoroutine(LineErase());
        //GenerateLine();

    }

    //void GenerateLine()
    //{
    //    counter += 1f / LineDrawSpeed * Time.deltaTime;

    //    for (int i = 0; i < vectors.Count - 2; i++)
    //    {
    //        x[i] = Mathf.Lerp(dist[i], dist[i + 1], counter);
    //        if (i == 0)
    //        {
    //            pointAlongLine[i] = x[i] * Vector3.Normalize(vectors[i] - Vector3.zero) + vectors[i];
    //        }
    //        else
    //        {
    //            pointAlongLine[i] = x[i] * Vector3.Normalize(vectors[i + 1] - vectors[i]) + longNote.LCirclepos[i];
    //        }
    //        myLine.SetPosition(i, pointAlongLine[i]);
    //    }
    //}

    IEnumerator LineDraw()
    {
        Vector3 newpos;
        myLine.positionCount = 2;
        for (int i = 0; i < myLine.positionCount - 1; i++)
        {
            float t = 0;
            float time = 1f;
            Vector3 orig = vectors[i];
            Vector3 orig2 = vectors[i + 1];
            myLine.SetPosition(i, orig);
            for (; t < time; t += Time.deltaTime * LineSpeed)
            {
                newpos = Vector3.Lerp(orig, orig2, t / time);
                myLine.SetPosition(i + 1, newpos);
                yield return null;
            }
            myLine.SetPosition(i + 1, orig2);
            if (i < vectors.Length - 2)
                myLine.positionCount = i + 3;
        }
        //IsFollowCircle = true;
        //IsFollowLine = true;
    }

    IEnumerator LineErase()
    {
        IsFollowLine = false;
        Vector3 newpos;
        for (int i = 0; i < myLine.positionCount - 1; i++)
        {
            float t = 0;
            float time = 1f;
            // i -> 0 , 0, 1
            // i -> 0 , 0, 2, 1, 2
            Vector3 orig = vectors[0];
            Vector3 orig2 = vectors[1];
            for (; t < time; t += Time.deltaTime * LineSpeed)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    orig = vectors[j];
                    orig2 = vectors[i + 1];
                    newpos = Vector3.Lerp(orig, orig2, t / time);
                    myLine.SetPosition(j, newpos);
                }
                yield return null;
            }
            for (int k = 0; k < i + 1; k++)
                vectors[k] = vectors[i + 1];
        }
        ObjectPool.instance.PushToPool(LineName, gameObject, myParent);
    }
}
