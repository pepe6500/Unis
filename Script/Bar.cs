using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {
    public Node_outline node_Outline;
    public ParticleSystem particle_1;
    public ParticleSystem particle_2;
    public Animation effect;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Node"))
        {
            node_Outline.NodeErase(collision.gameObject);
            particle_1.Play();
            particle_2.Play();
            effect.Stop();
            effect.Play();
        }
    }

    public void Pause()
    {

    }
}