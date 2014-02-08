using UnityEngine;
using System.Collections;

public class EffectAura : MonoBehaviour {

	public	float	m_fOffSetSpeed;	//	オフセット速度.
	public	int		m_iEffectTime;	//	エフェクト持続時間.
	
	int				m_iTime;		//	タイムカウント.
	GameObject		m_cPlayer;		//	プレイヤー.
	
	MeshRenderer	m_mrMeshRen;
	Material		m_mMaterial;
	Vector2			m_vOffset;		//	オフセット.
	
	void Start ()
	{
		//	座標修正.
		transform.position	+=	new Vector3( 0.0f, 2.0f, 0.0f );
		
		m_cPlayer	=	GameObject.Find("Player");
		
		//	MeshRenderer取得.
		m_mrMeshRen	=	gameObject.GetComponent<MeshRenderer>();
		
		//	マテリアルを取得.
		m_mMaterial	=	m_mrMeshRen.material;
		
		//	offsetを初期化.
		m_vOffset[0]	=	0.0f;
		m_vOffset[1]	=	0.0f;
		
		m_iTime	=	0;
	}
	
	void Update ()
	{
		transform.position	=	new Vector3( m_cPlayer.transform.position.x,
											 transform.position.y,
											 transform.position.z );
		
		m_vOffset[1]	+=	m_fOffSetSpeed;
		
		//	offsetを適用.
		m_mMaterial.SetTextureOffset("_MainTex", m_vOffset);
		
		m_iTime++;
		
		//	目標時間まできたら消す.
		if( m_iTime >= m_iEffectTime )
			Destroy( gameObject );
	}
}
