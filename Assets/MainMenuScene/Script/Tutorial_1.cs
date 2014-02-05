//using UnityEngine;
//using System.Collections;

//public class Tutorial_1 : MonoBehaviour
//{

//    public float m_SpeedTime;//	移動時間.
//    static int m_Mode;		//	状態(0:待機 1:フレームイン 2:フレームアウト).
//    private const int PAGE_NUM = 3; // ページ数.

//    float m_fTargetPos;		//	目標座標.
//    float m_fProcessCnt;		//	処理回数.
//    float m_fSpeed;			//	移動速度.
//    float m_fMove;			//	移動距離.
//    float[] m_fLimitDistance;	//	移動限界距離.
//    static bool m_bMoveFlg;	//	移動フラグ.

//    void Start()
//    {
//        //	状態は待機.
//        m_Mode = 0;

//        m_bMoveFlg = false;

//        // 自分からカメラまで.
//        m_fMove = Camera.main.transform.position.x - gameObject.transform.position.x;

//        //	移動限界距離を設定.
//        m_fLimitDistance = new float[2];
//        m_fLimitDistance[0] = gameObject.transform.position.x;            // 初期座標.
//        m_fLimitDistance[1] = m_fMove * (float)PAGE_NUM;                             // 最終ページの時の座標.

//        m_fProcessCnt = m_SpeedTime / Time.deltaTime;

//        //	１フレームでの移動距離.
//        m_fSpeed = m_fMove / m_fProcessCnt;
//    }


//    void Update()
//    {
//        switch (m_Mode)
//        {
//            //	待機.
//            case 0:
//                break;

//            //	フレームイン.
//            case 1:
//                //	移動フラグが立っていれば目標座標を作成.
//                if (m_bMoveFlg)
//                {
//                    //	目標値を取得.
//                    m_fTargetPos = gameObject.transform.position.x + m_fMove;
//                    //m_fTargetPos	=	Camera.main.transform.position.x;

//                    //	フラグを消す.
//                    m_bMoveFlg = false;

//                    if (m_fTargetPos <= m_fLimitDistance[1])
//                    {
//                        //	ボタンを表示.
//                        LeftBtn.SetLeftBtn();
//                        RightBtn.SetRightBtn();
//                        //Cancel.SetCanBtn();
//                        m_Mode = 0;
//                        break;
//                    }
//                }

//                //	目標値まで移動.
//                if (gameObject.transform.position.x > m_fTargetPos)
//                {
//                    transform.position += new Vector3(m_fSpeed, 0.0f, 0.0f);
//                }
//                else
//                {
//                    //	移動が終了したらボタンを表示.
//                    LeftBtn.SetLeftBtn();
//                    RightBtn.SetRightBtn();
//                    //Cancel.SetCanBtn();

//                    //	位置を調整.
//                    transform.position = new Vector3(m_fTargetPos, transform.position.y, transform.position.z);

//                    //	待機状態.
//                    m_Mode = 0;
//                }
//                break;

//            //	フレームアウト.
//            case 2:
//                //	移動フラグが立っていれば目標座標を作成.
//                if (m_bMoveFlg)
//                {
//                    //	目標値を取得.
//                    m_fTargetPos = transform.position.x + m_fMove;

//                    m_bMoveFlg = false;

//                    if (m_fTargetPos >= m_fLimitDistance[0])
//                    {
//                        //	ボタンを表示.
//                        LeftBtn.SetLeftBtn();
//                        RightBtn.SetRightBtn();
//                        //Cancel.SetCanBtn();
//                        m_Mode = 0;
//                        break;
//                    }
//                }

//                //	目標値まで移動.
//                if (gameObject.transform.position.x < m_fTargetPos)
//                {
//                    gameObject.transform.position += new Vector3(m_fSpeed, 0.0f, 0.0f);
//                }
//                else
//                {
//                    //	移動が終了したらボタンを表示.
//                    LeftBtn.SetLeftBtn();
//                    RightBtn.SetRightBtn();
//                    //Cancel.SetCanBtn();
//                    //	待機状態.
//                    m_Mode = 0;
//                }
//                break;

//            //	座標初期化.
//            case 3:
//                gameObject.transform.position = new Vector3(m_fLimitDistance[0], 1.2f, -0.026f);
//                break;
//        }
//    }

//    public static void FadeIn()
//    {
//        m_Mode = 1;
//        m_bMoveFlg = true;
//    }

//    public static void FadeOut()
//    {
//        m_Mode = 2;
//        m_bMoveFlg = true;
//    }

//    public static void Init()
//    {
//        m_Mode = 3;
//    }

//    public Vector3 PassPos()
//    {
//        return transform.position;
//    }
//}
