﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class infMng : MonoBehaviour
{

    public SongList songCtrl;
    public Image _1;
    public Image _2;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (songCtrl.infMod == true)
        {
            adsfdgg();
            songCtrl.infMod = false;
        }        
    }


    public void adsfdgg() //함수명이 뭐라고 내 시간을 잡아먹는지 아파 죽겠구먼;;;;ㅅㅂ;
    {
        iTween.ValueTo(this.gameObject, iTween.Hash(
    "from", 0,
    "to", 1,
    "time", 0.5f,

    "onUpdate", "val_0"
    ));

        iTween.ValueTo(this.gameObject, iTween.Hash(
"from", 0,
"to", 1,
"time", 0.5f,

"onUpdate", "val_1"
));
    }
    void val_0(float val)
    {
        _1.color = new Vector4(255, 255, 255, val);
    }
    void val_1(float val)
    {
        _2.color = new Vector4(255, 255, 255, val);
    }

    public void deswfrsghjk()
    {
        songCtrl.Currentsong.rectTransform.localPosition = new Vector3(-605, 965, 0);
        songCtrl.SelThm.rectTransform.sizeDelta = new Vector2(450, 550);
        songCtrl.NextSelThm = null;
        songCtrl.SelThm = null;
        songCtrl.StopSel = false;
        songCtrl.SelThmIsble = false;
        songCtrl.NextSelIsble = false;
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", 1,
            "to", 0,
            "time", 0.5f,
            "onUpdate", "val_",
            "oncomplete", "qewrty"
         ));
    }
    void val_(float val)
    {
        _1.color = new Vector4(255, 255, 255, val);
        _2.color = new Vector4(255, 255, 255, val);
        songCtrl.Inf_Img.color = new Vector4(255, 255, 255, val);
    }
    void qewrty()
    {
        songCtrl.Inf_Img.gameObject.SetActive(false);
    }

}

