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
        Vector3 vDistance = m_cPlayer.transform.position - gameObject.transform.position;
        Vector3 vMove = Vector3.Normalize(vDistance) * 0.5f;

        if (Mathf.Abs(vDistance.z) < ATTACK_DISTANCE)
        {
            m_nNowState = (int)STATE.ATTACK;
            vMove *= 2.0f;
        }

        if (m_nNowState == (int)STATE.NORMAL)
        {
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Sin(Time.time * 10.0f));
        }
        else
        {
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        gameObject.transform.position += new Vector3(vMove.x, 0.0f, 0.0f);
    }
}
