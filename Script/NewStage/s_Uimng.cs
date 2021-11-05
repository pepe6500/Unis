using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class s_Uimng : MonoBehaviour
{
    public SongList SongCtrl;

    public void SceneChange(string Name)//씬전환
    {
        SceneManager.LoadScene(Name);
    }
    public void Screen(GameObject obj)
    {
        if (obj.activeSelf)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }
    }
    public void InfOut(Image img)
    {
        img.gameObject.SetActive(false);
        SongCtrl.Thm_infisBle = false;
    }

    public void Onsel()
    {

    }


    public void Res()
    {
       
    }
}
