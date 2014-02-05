using UnityEngine;
using System.Collections;

public class Cancel : MonoBehaviour
{

    //Vector3 m_vInitPos;			//	初期座標.
    //static bool m_bLeftSetFlg;	//	セットフラグ.
    //public Vector3 m_vSetPos;	//	画面内座標.

    void Start()
    {
        ////	最初はセットしない.
        //m_bLeftSetFlg = false;

        ////	画面外座標を設定.
        //m_vInitPos = new Vector3(0.0f, -0.1f, 0.0f);
    }

    void Update()
    {
        //if (m_bLeftSetFlg)
        //    //	レフトボタンを画面内へ移動.
        //    transform.position = m_vSetPos;
        //else
        //    //	レフトボタンを画面外へ移動.
        //    transform.position = m_vInitPos;

        ////	マウスクリック.
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    //	光線が当たった時の情報を取得.
        //    RaycastHit ray_hit;
        //    //	光線(カメラのヒット情報を取得).
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    //	タッチ情報を処理.
        //    if (Physics.Raycast(ray, out ray_hit, 100))
        //    {
        //        //	光線がヒットしたオブジェクトを取得
        //        GameObject game_object = ray_hit.collider.gameObject;

        //        //	ヒットしたオブジェクトが自分か？.
        //        if (game_object == gameObject)
        //        {
        //            //	ボタンを画面外へ移動.
        //            Cancel.ReleaseCanBtn();
        //            RightBtn.ReleaseRightBtn();
        //            LeftBtn.ReleaseLeftBtn();

        //            //	チュートリアル画面を初期化.
        //            Tutorial_1.Init();

        //            //	チュートリアルボタンを初期化.
        //            TutorialBtn.Init();
        //        }
        //    }
        //}

        //クリックされた場合
        if (gameObject.GetComponent<Button>().GetClick())
        {
            //タップ音を再生
            audio.PlayOneShot(gameObject.GetComponent<AudioSource>().clip);

            //入力目的を"メイン"に変更
            gameObject.GetComponent<Button>().SetTarget("main");

            //チュートリアル終了
            GameObject.Find("Tutorial").GetComponent<Tutorial>().Set(false);
        }
    }

    ////	レフトボタンを画面内に表示する.
    //public static void SetCanBtn()
    //{
    //    //	セットする.
    //    m_bLeftSetFlg = true;
    //}

    ////	レフトボタンを画面外に表示する.
    //public static void ReleaseCanBtn()
    //{
    //    m_bLeftSetFlg = false;
    //}
}
