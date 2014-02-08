using UnityEngine;
using System.Collections;

public class EffectStart : MonoBehaviour {
	
	public	int		m_iEffectTime;	//	エフェクト持続時間.
	int				m_iTime;		//	タイムカウント.
	
	void Start ()
	{
		transform.position	=	new Vector3( transform.position.x,
											 4.0f,
											 transform.position.z );
		
		m_iTime	=	0;
	}
	
	void Update ()
	{		
		m_iTime++;
		
		//	目標時間まできたらフェード開始.
		if( m_iTime >= m_iEffectTime )
		{
			Destroy( gameObject );
		}
	}
}
