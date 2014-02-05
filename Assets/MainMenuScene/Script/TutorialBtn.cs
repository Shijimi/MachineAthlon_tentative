   using UnityEngine;
using System.Collections;

public class TutorialBtn : MonoBehaviour
{

    //	レンダラー設定用テクスチャ.
    public Texture Tutorial_Off;
    public Texture Tutorial_On;
    static bool m_bInitFlg;	//	初期化フラグ.

    void Start()
    {
        //	レンダラーをOffに変更.
        renderer.material.mainTexture = Tutorial_Off;
        //	フラグを初期化.
        m_bInitFlg = false;
    }

    void Update()
    {
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

        //        //	ヒットしたオブジェクトがスタートボタンか？.
        //        if (game_object == gameObject)
        //        {
        //            //	クリックされたらレンダラーをOnに変更.
        //            renderer.material.mainTexture = Tutorial_On;

        //            //	フェードイン開始.
        //            Tutorial_1.FadeIn();

        //            //	初期化フラグを消す.
        //            m_bInitFlg = false;
        //        }
        //    }
        //}

        //クリックされた場合
        if (gameObject.GetComponent<Button>().GetClick())
        {
            //	クリックされたらレンダラーをOnに変更.
            renderer.material.mainTexture = Tutorial_On;

            //	フェードイン開始.
            //Tutorial_1.FadeIn();

            //	初期化フラグを消す.
            m_bInitFlg = false;

            //入力目的を"チュートリアル"に変更.
            gameObject.GetComponent<Button>().SetTarget("tutorial");

            //チュートリアル開始
            GameObject.Find("Tutorial").GetComponent<Tutorial>().Set(true);
        }

        //	フラグをが立っていればレンダラーを初期化.
        if (m_bInitFlg)
            renderer.material.mainTexture = Tutorial_Off;
    }

    //	初期化フラグ用関数
    public static void Init()
    {
        m_bInitFlg = true;
    }
}
