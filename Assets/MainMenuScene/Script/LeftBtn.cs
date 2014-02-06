using UnityEngine;
using System.Collections;

public class LeftBtn : MonoBehaviour
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
        //            LeftBtn.ReleaseLeftBtn();
        //            RightBtn.ReleaseRightBtn();
        //            //Cancel.ReleaseCanBtn();

        //            //	フェードイン開始.
        //            Tutorial_1.FadeIn();
        //        }
        //    }
        //}

        if (gameObject.GetComponent<Button>().GetClick())
        {
            //入力目的を"なし"に設定
            gameObject.GetComponent<Button>().SetTarget("tutorial_scroll");

            //前のページ
            if(GameObject.Find("Tutorial").GetComponent<Tutorial>().Page(-1))
            {
                //ページめくりに成功するとSEを再生
                audio.PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
            }
        }

    }

    ////	レフトボタンを画面内に表示する.
    //public static void SetLeftBtn()
    //{
    //    //	セットする.
    //    m_bLeftSetFlg = true;
    //}

    ////	レフトボタンを画面外に表示する.
    //public static void ReleaseLeftBtn()
    //{
    //    m_bLeftSetFlg = false;
    //}
}
