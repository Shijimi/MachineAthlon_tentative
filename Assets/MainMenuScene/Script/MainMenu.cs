using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	//	フェードの時間を確保.
	int		m_fadeTime;
	//	フェード中か？.
	bool	m_fadeFlg;
	
    void Start()
    {
		m_fadeTime	=	60;
		Fade.FadeOut();
		m_fadeFlg	=	false;
    }
	
	void Update ()
    {
		if( m_fadeFlg )
			m_fadeTime--;
		
		if( m_fadeTime <= 0 )
	           //アクションシーンを開始する.
     	       Application.LoadLevel("ActionScene");
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(210,Screen.height/2, 300, 40), "ゲームスタート"))
        {
			Fade.FadeIn();
			m_fadeFlg	=	true;
        }

        if (GUI.Button(new Rect(210, Screen.height / 2 + 60,300, 40), "操作説明"))
        {
           //操作説明を表示する.
        }
    }
}
