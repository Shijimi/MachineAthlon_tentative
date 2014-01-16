using UnityEngine;
using System.Collections;

//==================================================
//ステージごとのテクスチャ切り替えを行うスクリプト
//==================================================

public class Handle : MonoBehaviour
{
    public Texture m_cLand, m_cSea, m_cSky;
    private GameObject m_cGameSystem,m_cPlayer;

    // Use this for initialization
    void Start()
    {
        //	ゲームシステムのオブジェクトを取得.
        m_cGameSystem = GameObject.Find("GameSystem");
        //プレイヤーのオブジェクトを取得する.
        m_cPlayer = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_cGameSystem.GetComponent<GameSystem>().GetStatus())
        {
            //各ステージの開始時
            case "LAND_START":
            case "SEA_START":
            case "SKY_START":

                //ハンドルの向きを初期位置に戻す
                gameObject.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

                break;

            //陸ステージ
            case "LAND":
                gameObject.renderer.material.mainTexture = m_cLand;

                //速さに応じてハンドルを回転させる.
                gameObject.transform.Rotate(new Vector3(0.0f, m_cPlayer.GetComponent<Player>().GetSwipe() * 0.1f, 0.0f));

                break;

            //海ステージ
            case "SEA":
                gameObject.renderer.material.mainTexture = m_cSea;

                //速さに応じてハンドルを回転させる.
                gameObject.transform.Rotate(new Vector3(0.0f, m_cPlayer.GetComponent<Player>().GetSwipe() * 0.1f, 0.0f));

                break;

            //空ステージ
            case "SKY":
                gameObject.renderer.material.mainTexture = m_cSky;

                //速さに応じてハンドルを回転させる.
                gameObject.transform.Rotate(new Vector3(0.0f, m_cPlayer.GetComponent<Player>().GetSwipe() * 0.05f, 0.0f));

                //空ステージのハンドル(飛行機のハンドル)は回転し続けると不自然なので角度制限を行う
                if (gameObject.transform.localRotation.eulerAngles.y <135.0f)
                {
                    gameObject.transform.eulerAngles = new Vector3(0.0f, 135.0f, 0.0f);
                }
                else if (gameObject.transform.localRotation.eulerAngles.y > 225.0f)
                {
                    gameObject.transform.eulerAngles = new Vector3(0.0f, 225.0f, 0.0f);
                }

                break;
        }
    }
}