using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editer : MonoBehaviour {

    public enum EditMode
    {
        none,
        swipeNote,
        longNode,
        shortNote,
        outNote //2.5+
    }

    public MusicNode musicNode;

    public Slider timeBar;
    public Text timeText;
    public AudioSource audioSource;
    public NodeManager nodeManager;
    public GameObject playButton;
    public GameObject pauseButton;
    public Text stateText;

    private bool longNodePick;
    private List<Vector2> longNodes = new List<Vector2>();
    float longNodeTime;
    bool play;
    EditMode editMode = EditMode.none;


	// Use this for initialization
	void Start () {
        nodeManager.Stop();
        musicNode = nodeManager.musicNode;
        audioSource.clip = musicNode.music;
        nodeManager.editerMode = true;

    }
	
	// Update is called once per frame
	void Update () {
        timeText.text = SecondToTime(audioSource.time);
        if(play)
        {
            timeBar.value = audioSource.time / audioSource.clip.length;
        }
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out raycastHit))
            {
                if(raycastHit.transform.CompareTag("EditBackground"))
                {
                    Debug.Log(audioSource.time);
                    switch(editMode)
                    {
                        case EditMode.shortNote:
                            musicNode.nomalNodes.Add(new NomalNode() { time = audioSource.time, position = raycastHit.point });
                            break;
                        case EditMode.longNode:
                            if(longNodePick)
                            {
                                longNodes.Add(raycastHit.point);
                            }
                            else
                            {
                                longNodePick = true;
                                longNodes.Clear();
                                longNodes.Add(raycastHit.point);
                                longNodeTime = audioSource.time;
                            }
                            break;
                        case EditMode.swipeNote:
                            musicNode.swipeNodes.Add(new SwipeNode() { time = audioSource.time, position = raycastHit.point });
                            break;
                        case EditMode.outNote:
                            break;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (longNodePick)
            {
                longNodePick = false;
                musicNode.longNodes.Add(new LongNode() { time = longNodeTime, position = longNodes.ToArray() });
            }
        }
    }

    void Sort()
    {
        musicNode.nomalNodes.Sort(delegate (NomalNode A, NomalNode B)
        {
            if (A.time == B.time) return 0;
            if (A.time == 0) return 1;
            if (B.time == 0) return -1;
            if (A.time > B.time) return 1;
            else if (A.time < B.time) return -1;
            return 0;
        });

        musicNode.longNodes.Sort(delegate (LongNode A, LongNode B)
        {
            if (A.time == B.time) return 0;
            if (A.time == 0) return 1;
            if (B.time == 0) return -1;
            if (A.time > B.time) return 1;
            else if (A.time < B.time) return -1;
            return 0;
        });

        musicNode.swipeNodes.Sort(delegate (SwipeNode A, SwipeNode B)
        {
            if (A.time == B.time) return 0;
            if (A.time == 0) return 1;
            if (B.time == 0) return -1;
            if (A.time > B.time) return 1;
            else if (A.time < B.time) return -1;
            return 0;
        });

        musicNode.nodes.Sort(delegate (Node A, Node B)
        {
            if (A.time == B.time) return 0;
            if (A.time == 0) return 1;
            if (B.time == 0) return -1;
            if (A.time > B.time) return 1;
            else if (A.time < B.time) return -1;
            return 0;
        });

        nodeManager.SetNowTime();
    }

    public void Play()
    {
        audioSource.Play();
        nodeManager.Play();
        play = true;
        Time.timeScale = 1;
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        Sort();

    }

    public void Pause()
    {
        audioSource.Pause();
        nodeManager.Stop();
        play = false;
        Time.timeScale = 0;
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void Stop()
    {
        audioSource.Stop();
        nodeManager.Stop();
        audioSource.time = 0;
        timeBar.value = 0;
        play = false;
        Time.timeScale = 0;
        playButton.SetActive(true);
        pauseButton.SetActive(false);

    }

    string SecondToTime(float _time)
    {
        string temp = "";
        float time = _time;
        int min = (int)time / 60;
        if (min < 10)
            temp += "0";
        temp += (min + ":");
        time %= 60;
        int sec = (int)time;
        if (sec < 10)
            temp += "0";
        temp += (sec + ":");
        time %= 1;
        int mis = (int)(time * 100);
        if (mis < 10)
            temp += "0";
        temp += mis;
        return temp;
    }
    
    public void SetToBar()
    {
        if(play)
        {
            Pause();
            nodeManager.Stop();
        }
        audioSource.time = timeBar.value * audioSource.clip.length;
        timeText.text = SecondToTime(audioSource.time);
    }
    
    public void PutOutNote(Vector2 mousePosition)
    {
        Vector2 position = mousePosition - new Vector2(960, 476);
        Node node = new Node
        {
            angle = GetAngle(mousePosition),
            time = audioSource.time + 2.5f
        };
    }

    float GetAngle(Vector2 mouse)
    {
        //mouse -= (Vector2)circle.transform.localPosition;

        float radian = Mathf.Atan2(mouse.x, mouse.y);

        float angle = -(radian * 180 / Mathf.PI);

        //Debug.Log(angle);


        //if (angle > 0)
        //{

        //    bar_L.transform.rotation = Quaternion.Euler(0, 0, angle);


        //    Debug.Log((mouse.normalized * r) + (Vector2)circle.transform.localPosition);
        //    Debug.Log(bar_L.GetComponent<RectTransform>().localPosition);

        //    //bar.transform.position = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));

        //    bar_L.GetComponent<RectTransform>().localPosition = (mouse.normalized * r) + (Vector2)circle.transform.localPosition;
        //}
        //else
        //{

        //}
        return angle;
    }

    void SetNoneMode()
    {
        editMode = EditMode.none;
    }

    public void SetEditMode(int _editMode)
    {
        editMode = (EditMode)_editMode;
        Debug.Log("Set Mode : " + editMode);
        stateText.text = editMode.ToString();
    }

    public void PlusTimeALL(GameObject input)
    {
        float timeVal = float.Parse(input.GetComponent<InputField>().text);
        Debug.Log("Time Plus : " + timeVal);
        switch(editMode)
        {
            case EditMode.shortNote:
                for(int i = 0; i < musicNode.nomalNodes.Count; i++)
                {
                    musicNode.nomalNodes[i].time += timeVal;
                }
                break;
            case EditMode.swipeNote:
                for (int i = 0; i < musicNode.swipeNodes.Count; i++)
                {
                    musicNode.swipeNodes[i].time += timeVal;
                }
                break;
            case EditMode.longNode:
                for (int i = 0; i < musicNode.longNodes.Count; i++)
                {
                    musicNode.longNodes[i].time += timeVal;
                }
                break;
            case EditMode.outNote:
                for (int i = 0; i < musicNode.nodes.Count; i++)
                {
                    musicNode.nodes[i].time += timeVal;
                }
                break;
        }
        input.GetComponent<InputField>().text = "";
    }
}
