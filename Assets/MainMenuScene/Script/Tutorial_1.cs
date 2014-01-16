using UnityEngine;
using System.Collections;

public class Tutorial_1 : MonoBehaviour {
	
	public	float	m_Speed;	//	移動速度.
	public	float	m_OutPosX;	//	フレームアウト目標座標.
	static	int		m_Mode;		//	状態(0:待機 1:フレームイン 2:フレームアウト).
	
	float	m_fMovePosX;		//	移動距離.
	float	m_fTargetPos;		//	目標座標.
	static	bool	m_bMoveFlg;	//	移動フラグ.

	void Start()
	{
		//	状態は待機.
		m_Mode	=	0;
		
		m_fMovePosX	=	0.6f;
		
		m_bMoveFlg	=	false;
	}
	
	void Update()
	{
		switch( m_Mode )
		{
		//	待機.
		case 0:
			break;

		//	フレームイン.
		case 1:
			//	移動フラグが立っていれば目標座標を作成.
			if( m_bMoveFlg )
			{
				m_fTargetPos	=	gameObject.transform.position.x - m_fMovePosX;
				m_bMoveFlg		=	false;
				
				if( m_fTargetPos <= -1.8f )
				{
					//	ボタンを表示.
					LeftBtn.SetLeftBtn();
					RightBtn.SetRightBtn();
					Cancel.SetCanBtn();
					m_Mode	=	0;
					break;
				}
			}
			
			//	目標値まで移動.
			if( gameObject.transform.position.x > m_fTargetPos )
			{
				transform.position	-=	new Vector3( m_Speed, 0.0f, 0.0f );
			}else
			{
				//	移動が終了したらボタンを表示.
				LeftBtn.SetLeftBtn();
				RightBtn.SetRightBtn();
				Cancel.SetCanBtn();
				//	待機状態.
				m_Mode	=	0;
			}
			break;

		//	フレームアウト.
		case 2:
			//	移動フラグが立っていれば目標座標を作成.
			if( m_bMoveFlg )
			{
				m_fTargetPos	=	gameObject.transform.position.x + m_fMovePosX;
				m_bMoveFlg		=	false;
				
				if( m_fTargetPos >= 0.61f )
				{
					//	ボタンを表示.
					LeftBtn.SetLeftBtn();
					RightBtn.SetRightBtn();
					Cancel.SetCanBtn();
					m_Mode	=	0;
					break;
				}
			}
			
			//	目標値まで移動.
			if( gameObject.transform.position.x < m_fTargetPos )
			{
				transform.position	+=	new Vector3( m_Speed, 0.0f, 0.0f );
			}else
			{
				//	移動が終了したらボタンを表示.
				LeftBtn.SetLeftBtn();
				RightBtn.SetRightBtn();
				Cancel.SetCanBtn();
				//	待機状態.
				m_Mode	=	0;
			}
			break;
		
		//	座標初期化.
		case 3:
			transform.position	=	new Vector3( 0.612f, 1.2f, -0.026f );
			break;
		}
	}
	
	public static void FadeIn()
	{
		m_Mode		=	1;
		m_bMoveFlg	=	true;
	}

	public static void FadeOut()
	{
		m_Mode	=	2;
		m_bMoveFlg	=	true;
	}
	
	public static void Init()
	{
		m_Mode	=	3;
	}
}
