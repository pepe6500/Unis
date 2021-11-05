using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour {

    private UImanager uImanager;
    private Transform SnoteParent;
    private Transform LnoteParent;
    private Transform SwnoteParent;

    private void Start()
    {
        uImanager = GameObject.Find("GameMng").GetComponent<UImanager>();
        SnoteParent = GameObject.Find("ShortMng").GetComponent<Transform>();
        LnoteParent = GameObject.Find("LnoteMng").GetComponent<Transform>();
        SwnoteParent = GameObject.Find("SwipeMng").GetComponent<Transform>();
    }

    public void Clicknote(string poolName, float PerfectTime, float GreatTime, float missTime, float TouchJudgmentTime,
                          NoteData noteData, int nPrefab, Touch tempTouch, bool IsClick = false)
    {
        //Vector2 pos = Camera.main.ScreenToWorldPoint(tempTouch.position);
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(pos, Vector2.zero);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider.CompareTag(poolName))
            {
                if (GreatTime <= TouchJudgmentTime && TouchJudgmentTime < PerfectTime)
                {
                    uImanager.gradeText.text = noteData.noteInfo[0].notegrade.ToString();
                    //noteData.noteInfo[0].notegrade = Notegrade.great;
                }
                else if (missTime <= TouchJudgmentTime && TouchJudgmentTime < GreatTime)
                {
                    uImanager.gradeText.text = noteData.noteInfo[1].notegrade.ToString();
                    //noteData.noteInfo[0].notegrade = Notegrade.great;
                }
                else if (missTime - 0.25f <= TouchJudgmentTime && TouchJudgmentTime < missTime)
                {
                    uImanager.gradeText.text = noteData.noteInfo[2].notegrade.ToString();
                    //noteData.noteInfo[0].notegrade = Notegrade.great;
                }
                else
                {
                    uImanager.gradeText.text = noteData.noteInfo[2].notegrade.ToString();
                    //noteData.noteInfo[0].notegrade = Notegrade.miss;
                }
                if (poolName == "SwipeNote")
                    Addsizenote(poolName, hits[i], IsClick, hits[i].collider.gameObject.GetComponent<Animator>(), nPrefab);
                else if (poolName == "LongCNote")
                {
                    InstantiateParticle(hits[i]);
                    StartCoroutine(DelayParticle(poolName, hits[i]));
                    ObjectPool.instance.PushToPool(poolName, hits[i].collider.gameObject.transform.parent.gameObject);
                }
                else if (poolName == "ShortNote")
                {
                    InstantiateParticle(hits[i]);
                    StartCoroutine(DelayParticle(poolName, hits[i], SnoteParent));
                }
                else if (poolName == "LongPNote")
                {
                    InstantiateParticle(hits[i]);
                }
            }
        }
    }

    private void Addsizenote(string poolName, RaycastHit2D hit, bool IsClick, Animator animator, int nPrefab)
    {
        IsClick = true;
        animator.SetTrigger("Swipe");
        SwAddsizenote(poolName, hit, nPrefab);
    }

    private void SwAddsizenote(string poolName, RaycastHit2D hit, int nPrefab)
    {
        nPrefab++;
        hit.collider.gameObject.transform.parent.gameObject.name = poolName + nPrefab.ToString();
        if (hit.collider.gameObject.transform.parent.gameObject.name == poolName + nPrefab.ToString())
        {
            InstantiateParticle(hit);
            hit.collider.gameObject.transform.parent.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            hit.collider.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            hit.collider.gameObject.transform.parent.gameObject.name = poolName;
        }
        StartCoroutine(SwDelayParticle(poolName, hit, SwnoteParent));
    }
    
    private void InstantiateParticle(RaycastHit2D hit)
    {
        Debug.Log("Particle");
        hit.collider.gameObject.transform.parent.gameObject.transform.GetChild(3).GetComponent<ParticleSystem>().Play();
    }

    private IEnumerator DelayParticle(string poolName,RaycastHit2D hit, Transform Parents = null)
    {
        for (int i = 0; i < 3; i++)
            hit.collider.gameObject.transform.parent.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 3; i++)
            hit.collider.gameObject.transform.parent.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        ObjectPool.instance.PushToPool(poolName, hit.collider.gameObject.transform.parent.gameObject, Parents);
    }

    private IEnumerator SwDelayParticle(string poolName, RaycastHit2D hit, Transform Parents = null)
    {
        for (int i = 1; i < 3; i++)
            hit.collider.gameObject.transform.parent.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        hit.collider.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 3; i++)
            hit.collider.gameObject.transform.parent.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        ObjectPool.instance.PushToPool(poolName, hit.collider.gameObject.transform.parent.gameObject, Parents);
    }
}
