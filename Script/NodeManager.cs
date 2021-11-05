using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public AudioSource audioSource;
    public MusicNode musicNode;
    public Node_outline node_Outline;
    public UImanager uiManager;
    int count = 0;
    int nomalCount = 0;
    int longCount = 0;
    int swipeCount = 0;
    bool play = true;
    public bool editerMode;
    public GameObject nodeNumber;
    // Use this for initialization
    void Start()
    {
        audioSource.clip = musicNode.music;
        StartCoroutine(StartMusic());
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            if (count < musicNode.nodes.Count)
            {
                if (audioSource.time >= musicNode.nodes[count].time)
                {
                    if (musicNode.nodes[count].time != 0)
                    {
                        node_Outline.InstanceNode(musicNode.nodes[count].angle, audioSource.time - musicNode.nodes[count].time);
                    }
                    else
                        musicNode.nodes.Remove(musicNode.nodes[count]);
                    count++;
                }
            }

            if (nomalCount < musicNode.nomalNodes.Count)
            {
                if (audioSource.time >= musicNode.nomalNodes[nomalCount].time)
                {
                    if (uiManager)
                    {
                        if (musicNode.nomalNodes[nomalCount].time != 0)
                        {
                            uiManager.CreateShortNote(musicNode.nomalNodes[nomalCount].position);
                            if (editerMode)
                            {
                                GameObject temp = Instantiate(nodeNumber, transform);
                                temp.transform.position = musicNode.nomalNodes[nomalCount].position;
                                temp.GetComponent<TMPro.TextMeshProUGUI>().text = nomalCount.ToString();
                            }
                        }
                        nomalCount++;
                    }
                }
            }

            if (longCount < musicNode.longNodes.Count)
            {
                if (audioSource.time >= musicNode.longNodes[longCount].time)
                {
                    if (uiManager)
                    {
                        if (musicNode.longNodes[longCount].time != 0)
                        {
                            uiManager.CreateLongNote((Vector2[])musicNode.longNodes[longCount].position.Clone());
                            if (editerMode)
                            {
                                GameObject temp = Instantiate(nodeNumber, transform);
                                temp.transform.position = musicNode.longNodes[longCount].position[0];
                                temp.GetComponent<TMPro.TextMeshProUGUI>().text = longCount.ToString();
                            }
                        }
                        longCount++;
                    }
                }
            }

            if (swipeCount < musicNode.swipeNodes.Count)
            {
                if (audioSource.time >= musicNode.swipeNodes[swipeCount].time)
                {
                    if (uiManager)
                    {
                        if (musicNode.swipeNodes[swipeCount].time != 0)
                        {
                            uiManager.CreateSwipeNote(musicNode.swipeNodes[swipeCount].position);
                            if (editerMode)
                            {
                                GameObject temp = Instantiate(nodeNumber, transform);
                                temp.transform.position = musicNode.swipeNodes[swipeCount].position;
                                temp.GetComponent<TMPro.TextMeshProUGUI>().text = swipeCount.ToString();
                            }
                        }
                        Debug.Log(musicNode.swipeNodes[swipeCount].position);
                        swipeCount++;
                    }
                }
            }
        }
    }

    public void SetNowTime()
    {
        count = 0;
        nomalCount = 0;
        longCount = 0;
        swipeCount = 0;
        
        while (true)
        {
            if (count < musicNode.nodes.Count)
            {
                if (audioSource.time >= musicNode.nodes[count].time)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }

        }

        while (true)
        {
            if (nomalCount < musicNode.nomalNodes.Count)
            {
                if (audioSource.time >= musicNode.nomalNodes[nomalCount].time)
                {
                    nomalCount++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            if (longCount < musicNode.longNodes.Count)
            {
                if (audioSource.time >= musicNode.longNodes[longCount].time)
                {
                    longCount++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }

        while (true)
        {
            if (swipeCount < musicNode.swipeNodes.Count)
            {
                if (audioSource.time >= musicNode.swipeNodes[swipeCount].time)
                {
                    swipeCount++;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }

    public void Play()
    {
        SetNowTime();
        play = true;
    }

    public void Stop()
    {
        play = false;
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(3);
        if (play && !audioSource.isPlaying)
        {
            Debug.Log("ASd");
            audioSource.Play();
        }
    }
}
