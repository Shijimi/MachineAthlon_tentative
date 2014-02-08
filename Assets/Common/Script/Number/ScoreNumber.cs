//using UnityEngine;
//using System.Collections;

//public class ScoreNumber : MonoBehaviour {

//    Texture			m_NumTex;
//    static	float[]	m_Ccut = new float[5];
//    static	int		m_Cdigit;
	
//    // Use this for initialization
//    void Start () {
//        m_Cdigit		=	0;
		
//        //	登録されているテクスチャを取得.
//        m_NumTex	=	GetComponent<GUITexture>().texture;
//    }
	
//    void	OnGUI(){		
//        //	Event.current.type が EventType.Repaint の場合に限定.
//        if (Event.current.type == EventType.Repaint){
			
//            for( int i = 0; i < m_Cdigit; i++ ){
//                //	サイズ指定されたテクスチャを描画.
//                Graphics.DrawTexture(
//                    new Rect( 470 - 40 * i, 0, 100, 40 ),
//                    m_NumTex,
//                    new Rect(m_Ccut[i], 0.0f, 0.1f, 1.0f),
//                    0, 0, 0, 0 );
//            }
//        }
//    }
	
//    public	static	void	NumSet( int num ){
		
//        //	桁の計算.
//        m_Cdigit	=	Number.DigitGet( num );
		
//        for( int i = 0; i < m_Cdigit; i++ ){
			
//            m_Ccut[i]	=	Mathf.FloorToInt( ( num / (int)Mathf.Pow(10.0f, i) ) ) * 0.1f;
//        }
//    }
//}
