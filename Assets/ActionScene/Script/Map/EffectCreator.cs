using UnityEngine;
using System.Collections;

public class EffectCreator : MonoBehaviour {

	//	陸ステージのオブジェクトのタイプ
	enum BREAK
	{
		LAND_SPIN01 = 0,
		SEE_SKY_BLAST,
		ALL_LIGHT,
		ALL_KIRAKIRA,
		ALL_AURA,
		NUM
	}
	
	private int m_nNowStageNum;                            //現在のステージ番号
	
	//オブジェクトと生成個数
	//Unity側で変更できるようにあえて構造体を使用していません
	
	//陸ステージ
	[SerializeField]
	GameObject[] m_cBreakObj;	//	オブジェクト
	
	GameObject m_cGameSystem;                      //ゲームシステム(シーン管理など)
	
	GameObject		m_cPlayer;
	
	GameObject[]	m_cObj;
	
	// Use this for initialization
	void Start ()
	{		
		//----------------------------------------
		//ステージ関連の値を取得
		//----------------------------------------
		m_cGameSystem = GameObject.Find("GameSystem");          //ゲームシステムのオブジェクトを取得
		
		m_cPlayer	=	GameObject.Find("Player");
	}
	
	public void Create( string status )
	{
		
		switch( status )
		{
		case "BREAK":
			//ステージによって配置するものを決定する
			string mapName = m_cGameSystem.GetComponent<GameSystem>().GetStatus();
			switch (mapName)
			{
			//各ステージの開始時
			case "LAND":
				//オブジェクトを生成する
				if(m_cBreakObj[0] != null)GameObject.Instantiate( m_cBreakObj[0], m_cPlayer.transform.position, Quaternion.AngleAxis( 180.0f, new Vector3( 0.0f, 1.0f, 0.0f ) ) );
				
				break;
			case "SEA":
				//オブジェクトを生成する
				if(m_cBreakObj[1] != null)GameObject.Instantiate(m_cBreakObj[1], m_cPlayer.transform.position, new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
				
				break;
			case "SKY":
				//オブジェクトを生成する
				if(m_cBreakObj[1] != null)GameObject.Instantiate(m_cBreakObj[1], m_cPlayer.transform.position, new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
				break;
			}
			break;
			
		case "SPEED_UP":
			//オブジェクトを生成する
			if(m_cBreakObj[5] != null)GameObject.Instantiate(m_cBreakObj[5], m_cPlayer.transform.position, new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
			if(m_cBreakObj[2] != null)GameObject.Instantiate(m_cBreakObj[2], m_cPlayer.transform.position, new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
			
			break;
			
		case "UPGRADE":
			//オブジェクトを生成する
			if(m_cBreakObj[3] != null)GameObject.Instantiate(m_cBreakObj[3], m_cPlayer.transform.position, new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
			
			break;
			
		case "READY":
			//オブジェクトを生成する
			if(m_cBreakObj[6] != null)GameObject.Instantiate(m_cBreakObj[6], Camera.main.transform.position, new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
			
			break;
			
		case "START":
			//オブジェクトを生成する
			if(m_cBreakObj[7] != null)GameObject.Instantiate(m_cBreakObj[7], Camera.main.transform.position, new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
			
			break;
			
		case "FINISH":
			//オブジェクトを生成する
			if(m_cBreakObj[8] != null)GameObject.Instantiate(m_cBreakObj[8], Camera.main.transform.position, new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
			
			break;
		}
	}
}
