using UnityEngine;
using System.Collections;

public class ResultScore : MonoBehaviour
{
    [SerializeField]
    private string m_strTag;

    private ScoreBoard m_cScoreBoard;

	// Use this for initialization
	void Start ()
    {
        //総移動距離を取得
        uint nScore = (uint)GameObject.Find("DataManager").GetComponent<DataManager>().GetData(m_strTag);

        m_cScoreBoard = gameObject.GetComponent<ScoreBoard>();

        //5桁のスコアボードの作成
        m_cScoreBoard.Create(5);

        m_cScoreBoard.Set((uint)nScore);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
