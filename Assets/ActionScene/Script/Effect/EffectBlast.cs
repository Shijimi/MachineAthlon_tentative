using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectBlast : MonoBehaviour {
	
	public	float	m_fScaleAdd;
	
	float	m_fScale;
	int		m_iTimeCnt;		//	カウント.
	int		m_listNum	=	0;
	List<Vector2> 	m_vMove	=	new List<Vector2>();
	
	void Start ()
	{
		m_fScale	=	0.0f;
		
		m_vMove.Add( new Vector2( -2.0f, -2.0f ) );
		m_vMove.Add( new Vector2( 4.0f, 4.0f ) );
		m_vMove.Add( new Vector2( 0.0f, -8.0f ) );
		m_vMove.Add( new Vector2( -8.0f, 8.0f ) );
		m_vMove.Add( new Vector2( 6.0f, -4.0f ) );
		
		transform.position	=	new Vector3( transform.position.x + m_vMove[m_listNum].x,
											 transform.position.y,
											 transform.position.z + m_vMove[m_listNum].y );
		
		m_listNum++;
	}
	
	void Update ()
	{
		m_fScale += m_fScaleAdd;
		
		transform.localScale	=	new Vector3( m_fScale, 1.0f, m_fScale );
		
		if( m_fScale >= 1.6f )
		{
			m_fScale	=	0.0f;
			transform.position	=	new Vector3( transform.position.x + m_vMove[m_listNum].x,
												 transform.position.y,
												 transform.position.z + m_vMove[m_listNum].y );
			
			m_listNum++;
		}
		
		if( m_listNum == 5 )
		{
			Destroy(gameObject);
		}
	}
}
