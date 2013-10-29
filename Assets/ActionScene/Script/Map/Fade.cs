using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	public	Texture2D	m_FadeTex;
	public	float		m_alpha;
	static	float		m_add;
	static	bool		m_fadeFlg;
	
	const	float		FADE_IN	=	0.02f;
	const	float		FADE_OUT=	-0.02f;
	
	// Use this for initialization
	void Start () {
		m_add		=	FADE_OUT;
		m_fadeFlg	=	true;
	}
	
	void	OnGUI(){
		
		//	アルファ値を計算.
		m_alpha	+=	m_add;
		
		if( m_alpha <= 0.0f && m_add == FADE_OUT )
		{
			m_add		=	0.0f;
			m_alpha		=	0.0f;
		}
			
		if(	m_alpha >= 1.0f && m_add == FADE_IN )
		{
			m_add		=	0.0f;
			m_alpha		=	1.0f;
			m_fadeFlg	=	false;
		}
		
		//	アルファ値を適用.
		GUI.color = new Color(1, 1, 1, m_alpha );
		
		//	画像を反映.
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), m_FadeTex );
	}
	
	//	フェードイン.
	public	static	void	FadeIn(){
		m_add		=	FADE_IN;
		m_fadeFlg	=	true;
	}
	
	//	フェードアウト.
	public	static	void	FadeOut(){
		m_add		=	FADE_OUT;
	}
	
	//	フェード中かを返す.
	public	static	bool	FadeFlg(){
		return	m_fadeFlg;
	}
}
