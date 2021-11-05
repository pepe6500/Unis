using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScripts : MonoBehaviour {

    public MusicNode a;


	// Use this for initialization
	void Start () {
        for(int i = 26; i < 36; i++)
        {
            a.nodes[i].time = 43f + ((i - 26) * 0.2f);
            a.nodes[i].angle = 110f - (((i - 26f) / 10f) * 40f);
        }
        for (int i = 36; i < 46; i++)
        {
            a.nodes[i].time = 43f + ((i - 36) * 0.2f);
            a.nodes[i].angle = -110f + (((i - 36f) / 10f) * 40f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
