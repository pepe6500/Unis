using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node_outline : MonoBehaviour
{
    public GameObject node_Origin;
    public float NodeSpeed;
    public GameObject bar_L;
    public GameObject bar_R;
    public GameObject circle;
    public Transform ui;
    int TouchBar_L_Num = -1;
    int TouchBar_R_Num = -1;
    List<GameObject> nodes = new List<GameObject>();

    float r;
    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        r = (circle.GetComponent<RectTransform>().rect.width * circle.transform.localScale.x) * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        NodeMove();
        if(bar_L != null && bar_R != null)
        TouchBar();
    }

    public void InstanceNode(float angle , float delatime)
    {
        GameObject temp = Instantiate(node_Origin, Vector2.zero, Quaternion.Euler(0, 0, angle), ui);
        temp.transform.localPosition = (Vector2)circle.transform.localPosition;
        temp.transform.GetChild(0).GetComponent<Image>().color = new Color(255,255,255,0f);
        temp.transform.Translate(temp.transform.up * delatime * NodeSpeed, Space.World);
        temp.transform.GetChild(0).GetComponent<Image>().color += new Color(0, 0, 0, delatime * 0.8f);
        nodes.Add(temp);
    }
    
    void NodeMove()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].transform.Translate(nodes[i].transform.up * Time.deltaTime * NodeSpeed, Space.World);
            nodes[i].transform.GetChild(0).GetComponent<Image>().color += new Color(0, 0, 0, Time.deltaTime * 0.8f);
            if(nodes[i].transform.GetChild(0).GetComponent<Image>().color.a >= 2)
            {
                NodeErase(nodes[i]);
            }
        }
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

    void SetBarAngle_L()
    {
        Touch tempTouchs;
        tempTouchs = Input.GetTouch(TouchBar_L_Num);
        tempTouchs.position -= new Vector2(Screen.width * 0.5f, Screen.height * 0.5f) + (Vector2)circle.transform.localPosition;
        float angle = GetAngle(tempTouchs.position);

        if (angle > 65 && angle < 115)
        {

            bar_L.transform.rotation = Quaternion.Euler(0, 0, GetAngle(tempTouchs.position));


            //Debug.Log("L_po" + (TouchBar_L.position.normalized * r) + (Vector2)circle.transform.localPosition);
            //Debug.Log("L_ro" + bar_L.GetComponent<RectTransform>().localPosition);

            //bar.transform.position = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));

            bar_L.GetComponent<RectTransform>().localPosition = (tempTouchs.position.normalized * r) + (Vector2)circle.transform.localPosition;
        }

    }

    void SetBarAngle_R()
    {
        Touch tempTouchs;
        tempTouchs = Input.GetTouch(TouchBar_R_Num);
        tempTouchs.position -= new Vector2(Screen.width * 0.5f, Screen.height * 0.5f) + (Vector2)circle.transform.localPosition;
        float angle = GetAngle(tempTouchs.position);
        if (angle < -65 && angle > -115)
        {

            bar_R.transform.rotation = Quaternion.Euler(0, 0, angle);


            //Debug.Log("R_po" + (TouchBar_R.position.normalized * r) + (Vector2)circle.transform.localPosition);
            //Debug.Log("R_ro" + bar_R.GetComponent<RectTransform>().localPosition);

            //bar.transform.position = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));

            bar_R.GetComponent<RectTransform>().localPosition = (tempTouchs.position.normalized * r) + (Vector2)circle.transform.localPosition;
        }

    }

    void TouchBar()
    {
        Touch tempTouchs;
        if (Input.touchCount > 0)
        {

            for (int i = 0; i < Input.touchCount; i++)
            {
                tempTouchs = Input.GetTouch(i);
                tempTouchs.position -= new Vector2(Screen.width * 0.5f, Screen.height * 0.5f) + (Vector2)circle.transform.localPosition;
                float angle = GetAngle(tempTouchs.position);


                if (tempTouchs.phase == TouchPhase.Began)
                {
                    if(i == 0 && Input.touchCount > 1)
                    {
                        Debug.Log("개판이군;;;;;;;;;;;;;;;;");
                        if (TouchBar_L_Num != -1)
                            TouchBar_L_Num++;
                        if (TouchBar_R_Num != -1)
                            TouchBar_R_Num++;
                    }
                    if (tempTouchs.position.magnitude < r - 100)
                    {
                        continue;
                    }
                    if (angle < 0)
                    {
                        TouchBar_R_Num = i;
                        SetBarAngle_R();
                    }
                    else
                    {
                        TouchBar_L_Num = i;
                        SetBarAngle_L();
                    }
                    Debug.Log("Init : " + i + " -------------------------------------- ");
                }
                else if (tempTouchs.phase == TouchPhase.Moved)
                {
                    if (i == TouchBar_R_Num)
                    {
                        SetBarAngle_R();
                        Debug.Log("Move : " + i + " : R");
                    }
                    else if (i == TouchBar_L_Num)
                    {
                        SetBarAngle_L();
                        Debug.Log("Move : " + i + " : L");
                    }
                }
                else if (tempTouchs.phase == TouchPhase.Ended)
                {
                    Debug.Log("End : " + i + "==========");
                    if (i == TouchBar_R_Num)
                    {
                        TouchBar_R_Num = -1;
                    }
                    else if (i == TouchBar_L_Num)
                    {
                        TouchBar_L_Num = -1;
                    }
                    if (i < TouchBar_R_Num)
                    {
                        TouchBar_R_Num--;
                    }
                    if (i < TouchBar_L_Num)
                    {
                        TouchBar_L_Num--;
                    }
                }
            }
        }
    }

    public void NodeErase(GameObject node)
    {
        nodes.Remove(node);
        Destroy(node);
    }
    //void CheckNode()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector2 mouse = Camera.main.WorldToScreenPoint(Input.mousePosition);
    //        RaycastHit2D raycastHit2D = Physics2D.Raycast(mouse, Vector2.zero);
    //        if (raycastHit2D.collider != null)
    //        {
    //            for (int i = 0; i < nodes.Count; i++)
    //            {
    //                if(nodes[i] == raycastHit2D.collider.gameObject)
    //                {

    //                }
    //            }
    //        }
    //    }
    //}
}
