﻿//using UnityEngine;
//using System.Collections;

//public class TimeNumber2 : MonoBehaviour {

//    MeshRenderer	meshRen;
//    Material		material;
//    static	Vector2	m_offset;
	
//    void Start()
//    {
//        //	MeshRenderer取得.		
//        meshRen	=	gameObject.GetComponent<MeshRenderer>();
		
//        //	マテリアルを取得.
//        material	=	meshRen.material;
		
//        //	offsetを初期化.
//        m_offset[0]	=	0.6f;
//        m_offset[1]	=	0.0f;
//    }
	
//    void Update()
//    {
//        //	offsetを適用.
//        material.SetTextureOffset("_MainTex", m_offset);
//    }
	
//    public static void TimeSet2(float time){
//        //	タイムを反映.
//        m_offset[0]	=	time;
//    }
//}
