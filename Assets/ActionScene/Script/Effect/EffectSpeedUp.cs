using UnityEngine;
using System.Collections;

public class EffectSpeedUp : MonoBehaviour {
	
	public	float	m_fUpSpeed;		//	上昇速度.
	public	float	m_fPosition;	//	座標修正値.
	public	int		m_iEffectTime;	//	エフェクト持続時間.
	
	int				m_iTime;		//	タイムカウント.
	GameObject		m_cPlayer;		//	プレイヤー.
	
	void Start ()
	{
		//	座標修正.
		transform.position	+=	new Vector3( 0.0f, 3.0f, m_fPosition );
		
		m_cPlayer	=	GameObject.Find("Player");
	}
	
	void Update ()
	{
		transform.position	=	new Vector3( m_cPlayer.transform.position.x,
											 transform.position.y,
											 transform.position.z );
		
		//	座標移動.
		transform.position += new Vector3( 0.0f, 0.0f, m_fUpSpeed );
		
		m_iTime++;
		
		//	目標時間まできたら消す.
		if( m_iTime >= m_iEffectTime )
			Destroy( gameObject );
	}
}
