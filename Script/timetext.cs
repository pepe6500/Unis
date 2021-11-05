using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timetext : MonoBehaviour {

    public AudioSource audioSource;

	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = audioSource.time.ToString();
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(audioSource.time-1f);//1.4
        }
	}
}
