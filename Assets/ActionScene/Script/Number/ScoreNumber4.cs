﻿using UnityEngine;
using System.Collections;

public class ScoreNumber4 : MonoBehaviour {

	MeshRenderer	m_meshRen;
	Material		m_material;
	static	Vector2	m_offset;
	
	// Use this for initialization
	void Start () {
		//	MeshRenderer取得.		
		m_meshRen	=	gameObject.GetComponent<MeshRenderer>();
		
		//	マテリアルを取得.
		m_material	=	m_meshRen.material;
		
		//	offsetを初期化.
		m_offset[0]	=	0.0f;
		m_offset[1]	=	0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			m_offset[0]	+=	0.1f;
		}
		
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			m_offset[0]	-=	0.1f;
		}
		
		//	offsetを適用.
		m_material.SetTextureOffset("_MainTex", m_offset);
	}
	
	public static void ScoreSet4(float time){
		//	タイムを反映.
		m_offset[0]	=	time;
	}
}
