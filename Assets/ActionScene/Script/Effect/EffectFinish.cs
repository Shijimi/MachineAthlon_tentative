using UnityEngine;
using System.Collections;

public class EffectFinish : MonoBehaviour {

	public	float	m_fOffSetSpeed;	//	オフセット速度.
	public	int		m_iEffectTime;	//	エフェクト持続時間.
	
	int				m_iTime;		//	タイムカウント.
	GameObject		m_cPlayer;		//	プレイヤー.
	
	MeshRenderer	m_mrMeshRen;
	Material		m_mMaterial;
	Vector2			m_vOffset;		//	オフセット.
	
	void Start ()
	{
		transform.position	=	new Vector3( transform.position.x,
											 2.0f,
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
			//	フェードイン.
			Fade.FadeIn();
		}
		
		//	フェードが終わったらシーン変更.
		if( m_iTime >= (m_iEffectTime + 60.0f) )
		{
			Destroy( gameObject );
			
			//リザルトシーンを開始する
            Application.LoadLevel("ResultScene");
		}
	}
}
