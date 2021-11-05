using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    //public Node(float time_, float angle_)
    //{
    //    time = time_;
    //    angle = angle_;
    //}
    public float time;
    public float angle;
}

[System.Serializable]
public class NomalNode
{
    public float time;
    public Vector2 position;
}

[System.Serializable]
public class LongNode
{
    public float time;
    public Vector2[] position;
}

[System.Serializable]
public class SwipeNode
{
    public float time;
    public Vector2 position;
}

[CreateAssetMenu(menuName = "Example/Create ExampleAsset Instance")]
public class MusicNode : ScriptableObject
{
    public AudioClip music;
    public List<Node> nodes = new List<Node>();
    public List<NomalNode> nomalNodes = new List<NomalNode>();
    public List<LongNode> longNodes = new List<LongNode>();
    public List<SwipeNode> swipeNodes = new List<SwipeNode>();
}
