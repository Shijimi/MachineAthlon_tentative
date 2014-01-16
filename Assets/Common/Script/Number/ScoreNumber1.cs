using UnityEngine;
using System.Collections;

public class ScoreNumber1 : MonoBehaviour {

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
	
	void Update()
	{		
		//	offsetを適用.
		m_material.SetTextureOffset("_MainTex", m_offset);
	}
	
	public static void ScoreSet1(float time){
		//	タイムを反映.
		m_offset[0]	=	time;
	}
}
