using UnityEngine;
using System.Collections;

public class TimeBar : MonoBehaviour {
	
	public	float	m_fSpeed;
	MeshRenderer	m_meshRen;
	Material		m_material;
	static	Vector2	m_offset;
	
	void Start()
	{
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
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			m_offset[0]	+=	m_fSpeed;
		}
		
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			m_offset[0]	-=	m_fSpeed;
		}
		
		//	offsetを適用.
		m_material.SetTextureOffset("_MainTex", m_offset);
	}
	
	public static void TimeSet(float time){
		//	タイムを反映.
		m_offset[0]	=	time;
	}
}
