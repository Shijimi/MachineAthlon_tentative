using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	float m_fDistance;

    private GameObject m_cDataManager;

	void Start()
	{
		//	総移動距離を取得.
        m_cDataManager = GameObject.Find("DataManager");                                    //データマネージャーのオブジェクトを取得
        m_fDistance = (float)m_cDataManager.GetComponent<DataManager>().GetData("float_total_score");
	}
	
	void Update()
	{
		//	一桁目のスコアを取得.
		ScoreNumber1.ScoreSet1( (int)m_fDistance % 10 * 0.1f );
		
		//	二桁目のスコアを取得.
		ScoreNumber2.ScoreSet2( Mathf.FloorToInt( ( (int)m_fDistance / (int)Mathf.Pow(10.0f, 1) ) ) * 0.1f );
		
		//	三桁目のスコアを取得.
		ScoreNumber3.ScoreSet3( Mathf.FloorToInt( ( (int)m_fDistance / (int)Mathf.Pow(10.0f, 2) ) ) * 0.1f );
		
		//	四桁目のスコアを取得.
		ScoreNumber4.ScoreSet4( Mathf.FloorToInt( ( (int)m_fDistance / (int)Mathf.Pow(10.0f, 3) ) ) * 0.1f );
		
		//	五桁目のスコアを取得.
		ScoreNumber5.ScoreSet5( Mathf.FloorToInt( ( (int)m_fDistance / (int)Mathf.Pow(10.0f, 4) ) ) * 0.1f );
	}
}
