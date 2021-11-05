using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Number : MonoBehaviour {
    public float deleteTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        deleteTime -= Time.deltaTime;
        if(deleteTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
