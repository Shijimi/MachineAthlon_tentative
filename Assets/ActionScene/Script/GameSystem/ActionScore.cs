using UnityEngine;
using System.Collections;

public class ActionScore : MonoBehaviour
{
    private ScoreBoard m_cScoreBoard;
    private Player m_cPlayer;

	// Use this for initialization
	void Start ()
    {
        //総移動距離を取得
        m_cPlayer = GameObject.Find("Player").GetComponent<Player>();

        m_cScoreBoard = gameObject.GetComponent<ScoreBoard>();

        //5桁のスコアボードの作成
        m_cScoreBoard.Create(5);

        m_cScoreBoard.Set(8888);
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_cScoreBoard.Set((uint)m_cPlayer.GetDistance());
	}
}
