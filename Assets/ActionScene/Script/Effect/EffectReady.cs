using UnityEngine;
using System.Collections;

public class EffectReady : MonoBehaviour {

	public	float	m_fOffSetSpeed;	//	オフセット速度.
	public	int		m_iEffectTime;	//	エフェクト持続時間.
	
	int				m_iTime;		//	タイムカウント.
	
	MeshRenderer	m_mrMeshRen;
	Material		m_mMaterial;
	Vector2			m_vOffset;		//	オフセット.
	
	void Start ()
	{
		transform.position	=	new Vector3( transform.position.x,
											 4.0f,
											 transform.position.z );
		
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
		if( m_vOffset[0] < 0.5f )
			m_vOffset[0]	+=	m_fOffSetSpeed;
		
		//	offsetを適用.
		m_mMaterial.SetTextureOffset("_MainTex", m_vOffset);
		
		m_iTime++;
		
		//	目標時間まできたらフェード開始.
		if( m_iTime >= m_iEffectTime )
		{
			Destroy( gameObject );
		}
	}
}
