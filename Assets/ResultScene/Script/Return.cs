﻿using UnityEngine;
using System.Collections;

public class Return : MonoBehaviour {

	//	レンダラー設定用テクスチャ
	public	Texture	Return_Off;
	public	Texture	Return_On;
	
	int		m_fadeTime;		//	フェードの時間を確保.
	bool	m_fadeFlg;		//	フェード中か？.
	
	void	Start()
	{
		//	レンダラーをOffに変更.
		renderer.material.mainTexture	=	Return_Off;
		
		//	変数を初期化.
		m_fadeTime	=	60;
		m_fadeFlg	=	false;
	}
	
	void	Update()
	{
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    //	光線が当たった時の情報を取得.
        //    RaycastHit ray_hit;
        //    //	光線(カメラのヒット情報を取得).
        //    Ray	ray	=	Camera.main.ScreenPointToRay(Input.mousePosition);
			
        //    //	タッチ情報を処理.
        //    if( Physics.Raycast(ray, out ray_hit, 100 ) )
        //    {
        //        //	光線がヒットしたオブジェクトを取得
        //        GameObject	game_object	=	ray_hit.collider.gameObject;
	
        //        //	ヒットしたオブジェクトがスタートボタンか？.
        //        if( game_object == gameObject )
        //        {
        //            //	クリックされたらレンダラーをOnに変更.
        //            renderer.material.mainTexture	=	Return_On;
					
        //            //	フェード開始.
        //            Fade.FadeIn();
        //            m_fadeFlg	=	true;
        //        }
        //    }
        //}

        if (gameObject.GetComponent<Button>().GetClick())
        {
            //タップ音を再生
            audio.PlayOneShot(gameObject.GetComponent<AudioSource>().clip);

            //	クリックされたらレンダラーをOnに変更.
            renderer.material.mainTexture = Return_On;

            //	フェード開始.
            Fade.FadeIn();
            m_fadeFlg = true;
        }

		//	フェードフラグが立っていたらフェードタイムを減らす.
		if( m_fadeFlg )
			m_fadeTime--;
		
		//	フェードが終了したらシーン移動.
        if (m_fadeTime <= 0)
        {
            //入力目的を"メイン"に設定
            gameObject.GetComponent<Button>().SetTarget("main");

            //	タイトルシーンを開始する.
            Application.LoadLevel("MainMenuScene");
        }
	}
}
