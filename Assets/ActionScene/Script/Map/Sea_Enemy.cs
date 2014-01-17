﻿using UnityEngine;
using System.Collections;

public class Sea_Enemy : MonoBehaviour
{
    enum STATE
    {
        NORMAL,         //通常時
        ATTACK,         //攻撃時
        NUM             //ステータス数
    }

    public Texture[] m_cTexture = new Texture[(int)STATE.NUM];

    private int m_nNowState;
    private GameObject m_cPlayer;

	// Use this for initialization
	void Start ()
    {
        m_nNowState = (int)STATE.NORMAL;
        renderer.material.mainTexture = m_cTexture[m_nNowState];
        m_cPlayer = GameObject.Find("Player");                                      //ゲームシステムのオブジェクトを取得
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 vMove = Vector3.Normalize(m_cPlayer.transform.position - gameObject.transform.position) * 0.5f;
        gameObject.transform.position += new Vector3(vMove.x,0.0f,0.0f);
	}
}