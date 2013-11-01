using UnityEngine;
using System.Collections;

public class Number : MonoBehaviour {
	
	Texture			m_NumTex;
	static	float[]	m_Tcut = new float[2];
	static	int		m_Tdigit;
	
	// Use this for initialization
	void Start () {
		m_Tdigit		=	0;
		
		//	登録されているテクスチャを取得.
		m_NumTex	=	GetComponent<GUITexture>().texture;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void	OnGUI(){
		
		//	Event.current.type が EventType.Repaint の場合に限定.
		if (Event.current.type == EventType.Repaint){
			
			for( int i = 0; i < m_Tdigit; i++ ){
				//	サイズ指定されたテクスチャを描画.
				Graphics.DrawTexture(
					new Rect( 200 - 40 * i, 0, 100, 40 ),
	 	            m_NumTex,
		            new Rect(m_Tcut[i], 0.0f, 0.1f, 1.0f),
		            0, 0, 0, 0 );
			}
		}	
	}
	
	public	static	void	NumSet( int num ){
		
		//	桁の計算.
		m_Tdigit	=	DigitGet( num );
		
		for( int i = 0; i < m_Tdigit; i++ ){
			if( i == 0 ){
				m_Tcut[i]	=	num % 10 * 0.1f;
			}else{
				m_Tcut[i]	=	num / 10 * 0.1f;
			}
		}
	}
	
	public	static	int	DigitGet( int num ){
		int digit	=	1;
		int	numCase	=	1;
		
		if( num < 10 )
			return digit;
		else
			numCase	=	num - num % 10;
		
		for( float i = 2; numCase != 0; i+=1.0f){
			digit++;
			numCase	-= numCase % (int)Mathf.Pow(10.0f, i);
		}
		
		return digit;
	}
}