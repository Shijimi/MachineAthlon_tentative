using UnityEngine;
using System.Collections;

public class Sky_Enemy : MonoBehaviour
{
    enum STATE
    {
        NORMAL,         //通常時
        ATTACK,         //攻撃時
        NUM             //ステータス数
    }

    private int m_nNowState;
    private GameObject m_cPlayer;

    private static float ATTACK_DISTANCE = 8.0f;

    // Use this for initialization
    void Start()
    {
        m_nNowState = (int)STATE.NORMAL;
        m_cPlayer = GameObject.Find("Player");                                      //ゲームシステムのオブジェクトを取得
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーまでのベクトルを求める
        Vector3 vDistance = m_cPlayer.transform.position - gameObject.transform.position;
        Vector3 vMove = Vector3.Normalize(vDistance) * 0.5f;

        //攻撃範囲内に入った場合
        if (Mathf.Abs(vDistance.z) < ATTACK_DISTANCE)
        {
            m_nNowState = (int)STATE.ATTACK;        //ステートを攻撃に変更
            vMove *= 3.0f;                          //追尾速度を上げる
        }

        //通常時
        if (m_nNowState == (int)STATE.NORMAL)
        {
            //透明化アニメーション
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Sin(Time.time * 10.0f));
        }
        //攻撃時
        else
        {
            //透明化解除
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        //座標を加算
        gameObject.transform.position += new Vector3(vMove.x, 0.0f, 0.0f);
    }
}
