//using UnityEngine;
//using System.Collections;

//public class ResultMenu : MonoBehaviour
//{
//    int		m_fadeTime;		//	フェードの時間を確保
//    int		m_next;			//	次のシーン
//    bool	m_fadeFlg;		//	フェード中か？
//    const	int	NEXT_ACTION	=	1;		
	
//    void Start()
//    {
//        m_fadeTime	=	60;
//        Fade.FadeOut();
//        m_fadeFlg	=	false;
//        m_next		=	0;
//    }
	
//    void Update ()
//    {
//        if( m_fadeFlg )
//            m_fadeTime--;
		
//        if( m_fadeTime <= 0 )
//        {
//            if( m_next == NEXT_ACTION )
//               //	アクションシーンを開始する.
//               Application.LoadLevel("ActionScene");
//            else
//                //	タイトルを表示する.
//                Application.LoadLevel("MainMenuScene"); 
//        }
//    }

//    void OnGUI()
//    {
//        if (GUI.Button(new Rect(210, Screen.height / 2, 300, 40), "リトライ"))
//        {
//            Fade.FadeIn();
//            m_fadeFlg	=	true;
//            m_next	=	NEXT_ACTION;
//        }

//        if (GUI.Button(new Rect(210, Screen.height / 2 + 60, 300, 40), "タイトルに戻る"))
//        {
//            Fade.FadeIn();
//            m_fadeFlg	=	true;
//        }
//    }
//}
