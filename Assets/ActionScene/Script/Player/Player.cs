using UnityEngine;
using System.Collections;

//===========================
//プレイヤーの制御を行います.
//===========================
public class Player : MonoBehaviour
{
    //ステージの状態
    enum STAGE_STATE
    {
        LAND = 0,
        SEA,
        SKY,
        NUM
    }

    //プレイヤーの状態
    enum PLAYER_STATE
    {
        FIRST = 0,
        SECOND,
        THIRD,
        NUM
    }

    //プレイヤーに使用するオーディオの種類
    enum PLAYER_AUDIO
    {
        SPEED_UP = 0,
        SPEED_DOWN,
        CHANGE,
        NUM
    }

    private const float INIT_SPEED = 1.0f;                                   //初期速度
    private const float MAX_SPEED = 4.0f;	                                 //最高速度
    private const float MIN_SPEED = 0.0f;                           	     //最低速度

    public float m_fSpeed;	                                                 //プレイヤーのスピード
    private float m_fTragetSpeed;                                   	     //速度の目標値
    private float m_fTotalDistance;	                                         //総移動距離
    private float m_fPreDistance;                                            //前ステージまでの移動距離

    private int m_nLevel = 0;                                                //加速レベル
    private int[] m_nScore = new int[(int)STAGE_STATE.NUM];                  //スコア保存用

    private bool m_AniFlg;              	//毎フレームごとのアニメーションの状態

    //オーディオクリップ
    [SerializeField]private AudioClip[] m_cAudio = new AudioClip[(int)PLAYER_AUDIO.NUM]; 

	//	テクスチャ.
	public Texture[] m_cLand = new Texture[(int)PLAYER_STATE.NUM];
    public Texture[] m_cSea = new Texture[(int)PLAYER_STATE.NUM];
    public Texture[] m_cSky = new Texture[(int)PLAYER_STATE.NUM];

    private Texture[,] m_cTexture = new Texture[(int)STAGE_STATE.NUM, (int)PLAYER_STATE.NUM];

    //プレイヤーの移動関連の情報
    private float m_fPreSwipePos;           //1フレーム前の指のX座標
    private float m_fNowSwipePos;           //現在の指のX座標
    private float m_fSwipe;                 //スワイプした移動量

    private float m_fLeftLimitPos;          //左の移動限界
    private float m_fRightLimitPos;         //右の移動限界

    //ゲームオブジェクト
    private GameObject m_cGameSystem;       //ゲームシステム(シーン管理など)
    private GameObject m_cDataManager;      //データ管理(シーン間のデータの受け渡し)

    //状態管理
    private int m_nNowPlayer;               //現在のプレイヤーの状態
    private int m_nNowStage;                //現在のステージの状態
    private int m_nPrePlayer;               //1フレーム前のプレイヤーの状態
    private int m_nPreStage;                //1フレーム前のステージの状態

	// Use this for initialization
	void Start()
	{
        m_AniFlg = false;		            //フラグを初期化
        m_fSpeed = 0.0f;	            	//初期スピードを設定
        m_fTragetSpeed = INIT_SPEED;		//速度の目標値を初期化
        m_fTotalDistance = 0.0f;		    //総移動距離を初期化
        m_fPreDistance = 0.0f;              //前ステージまでの移動距離

        //状態管理の初期化
        m_nNowStage = (int)STAGE_STATE.LAND;
        m_nPreStage = (int)STAGE_STATE.LAND;
        m_nNowPlayer = (int)PLAYER_STATE.FIRST;
        m_nPrePlayer = (int)PLAYER_STATE.FIRST;

        //テクスチャ配列を結合
        m_cTexture[(int)STAGE_STATE.LAND, (int)PLAYER_STATE.FIRST] = m_cLand[(int)PLAYER_STATE.FIRST];
        m_cTexture[(int)STAGE_STATE.LAND, (int)PLAYER_STATE.SECOND] = m_cLand[(int)PLAYER_STATE.SECOND];
        m_cTexture[(int)STAGE_STATE.LAND, (int)PLAYER_STATE.THIRD] = m_cLand[(int)PLAYER_STATE.THIRD];

        m_cTexture[(int)STAGE_STATE.SEA, (int)PLAYER_STATE.FIRST] = m_cSea[(int)PLAYER_STATE.FIRST];
        m_cTexture[(int)STAGE_STATE.SEA, (int)PLAYER_STATE.SECOND] = m_cSea[(int)PLAYER_STATE.SECOND];
        m_cTexture[(int)STAGE_STATE.SEA, (int)PLAYER_STATE.THIRD] = m_cSea[(int)PLAYER_STATE.THIRD];

        m_cTexture[(int)STAGE_STATE.SKY, (int)PLAYER_STATE.FIRST] = m_cSky[(int)PLAYER_STATE.FIRST];
        m_cTexture[(int)STAGE_STATE.SKY, (int)PLAYER_STATE.SECOND] = m_cSky[(int)PLAYER_STATE.SECOND];
        m_cTexture[(int)STAGE_STATE.SKY, (int)PLAYER_STATE.THIRD] = m_cSky[(int)PLAYER_STATE.THIRD];
		
		//初期テクスチャを設定
		renderer.material.mainTexture = m_cTexture[m_nNowStage,m_nNowPlayer];

        //指の座標を初期化
        m_fPreSwipePos = 0.0f;
        m_fNowSwipePos = 0.0f;
        m_fSwipe = 0.0f;

        //画面外判定で使用する座標を算出
        float fDistance = Camera.main.transform.position.y - gameObject.transform.position.y;
        m_fLeftLimitPos = Camera.main.ViewportToWorldPoint(new Vector3(0.0f,0.0f,fDistance)).x;
        m_fRightLimitPos = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, fDistance)).x;


        m_cGameSystem = GameObject.Find("GameSystem");                                      //ゲームシステムのオブジェクトを取得
        m_cDataManager = GameObject.Find("DataManager");                                    //データマネージャーのオブジェクトを取得
	}
	
	// Update is called once per frame
	void Update()
    {		
        //ステージの状態に関連する処理
        this.StageState();

        //プレイヤーの状態に関連する処理
        this.PlayerState();

        //ステージ/プレイヤーの状態からテクスチャを設定する
        this.SetTexture();

        //スピードが0でない場合
        if (m_fTragetSpeed != MIN_SPEED)
        {
            //左クリックが押された瞬間
            if (Input.GetMouseButtonDown(0))
            {
                m_fNowSwipePos = Input.mousePosition.x;
                m_fPreSwipePos = m_fNowSwipePos;
            }

            //左クリックが押されている場合
            if (Input.GetMouseButton(0))
            {
                m_fPreSwipePos = m_fNowSwipePos;                                     //前のフレームの指の位置を保持
                m_fNowSwipePos = Input.mousePosition.x;                              //現在の指の位置を取得
            }

            m_fSwipe = (m_fNowSwipePos - m_fPreSwipePos);
            gameObject.transform.position += new Vector3(m_fSwipe * 0.025f, 0.0f, 0.0f);

            //点滅アニメーションの取り消し
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            //速度が0の場合はスワイプの勢いを無効にする
            m_fPreSwipePos = m_fNowSwipePos;
            m_fSwipe = 0.0f;

            //ダメージ中は点滅アニメーションを行う
            renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.2f - Mathf.Sin(Time.time * 50.0f));
        }

        //プレイヤーが画面外に出ている場合
        if (gameObject.transform.position.x < m_fLeftLimitPos)
        {
            gameObject.transform.position = new Vector3(m_fLeftLimitPos, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.x > m_fRightLimitPos)
        {
            gameObject.transform.position = new Vector3(m_fRightLimitPos, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (gameObject.animation.isPlaying == false && m_AniFlg == true)
        {
            //	速度の目標値を初期化.
            m_fTragetSpeed = INIT_SPEED;
        }

        //	アニメーションの状態を取得.
        m_AniFlg = gameObject.animation.isPlaying;

        //	速度を目標速度に近づける.
        m_fSpeed = Mathf.Lerp(m_fSpeed, m_fTragetSpeed, 0.05f);
	
		//	ステージステータスがリザルトの時は移動しない.
		if( m_cGameSystem.GetComponent<GameSystem>().GetStatus() != "RESULT" &&
			m_cGameSystem.GetComponent<GameSystem>().GetStatus() != "LAND_START" )
		{
	        //	総移動距離を加算.
	        m_fTotalDistance += m_fSpeed;
		}
    }

    //	オブジェクトに衝突した瞬間.
	private void OnCollisionEnter(Collision collision)
	{
		//	スリップ中は処理を中止.
		if( m_fTragetSpeed != MIN_SPEED )
		{
			switch(collision.gameObject.tag)
			{
				//	加速アイテムの場合.
				case "item_speed":

					//	速度を加算.
					if( m_fTragetSpeed < MAX_SPEED )m_fTragetSpeed += 1.0f;

                    //加速レベルを加算
                    m_nLevel++;
	
					//	アイテムを消す.
                    Destroy(collision.gameObject);

                    //加速サウンドの再生
                    audio.PlayOneShot(m_cAudio[(int)PLAYER_AUDIO.SPEED_UP]);
				
					//	加速エフェクトを生成.
					GetComponent<EffectCreator>().Create("SPEED_UP");
				
				    break;
	
	            //	妨害アイテムの場合.
				case "obs_break":
				
					//	アニメーションの再生
					gameObject.animation.Play();
	
					//	目標速度を0にする.
					m_fTragetSpeed	=	MIN_SPEED;

                    //加速レベルをリセット
                    m_nLevel = 0;
	
					//	アイテムを消す.
					Destroy( collision.gameObject );

                    //減速サウンドの再生
                    audio.PlayOneShot(m_cAudio[(int)PLAYER_AUDIO.SPEED_DOWN]);
				
					//	衝突エフェクトを生成.
					GetComponent<EffectCreator>().Create("BREAK");
	
				    break;
			}
		}
	}

	//	スピードを返す.
	public float GetSpeed() { return m_fSpeed; }

    //スワイプの移動量を返す
    public float GetSwipe() { return m_fSwipe; }

	//	総移動距離を返す.
    public float GetDistance() { return m_fTotalDistance; }

    //ステージの状態に関連する処理
    private void StageState()
    {
        //1フレーム前のステージの状態を保持
        m_nPreStage = m_nNowStage;

        switch (m_cGameSystem.GetComponent<GameSystem>().GetStatus())
        {
            //陸ステージの開始
            case "LAND_START":
                m_nNowStage = (int)STAGE_STATE.LAND;
                m_nLevel = 0;
                break;

            //海ステージの開始
            case "SEA_START":
                m_nNowStage = (int)STAGE_STATE.SEA;
                m_nLevel = 0;
                break;

            //空ステージの開始
            case "SKY_START":
                m_nNowStage = (int)STAGE_STATE.SKY;
                m_nLevel = 0;
                break;

            //陸ステージの終了
            case "LAND_END":

                //陸ステージのスコアを設定
                m_nScore[(int)STAGE_STATE.LAND] = (int)m_fTotalDistance;
                m_cDataManager.GetComponent<DataManager>().SetData("land_score", m_nScore[(int)STAGE_STATE.LAND]);
                m_fPreDistance = m_fTotalDistance;
                m_fTragetSpeed = INIT_SPEED;                         //速度の目標値を初期化
                m_nNowPlayer = (int)PLAYER_STATE.FIRST;              //プレイヤーの状態を初期化
                break;

            //海ステージの終了
            case "SEA_END":

                //海ステージのスコアを設定
                m_nScore[(int)STAGE_STATE.SEA] = (int)(m_fTotalDistance - m_fPreDistance);
                m_cDataManager.GetComponent<DataManager>().SetData("sea_score", m_nScore[(int)STAGE_STATE.SEA]);
                m_fPreDistance = m_fTotalDistance;

                m_fTragetSpeed = INIT_SPEED;                         //速度の目標値を初期化
                m_nNowPlayer = (int)PLAYER_STATE.FIRST;              //プレイヤーの状態を初期化
                break;

            //空ステージの終了
            case "SKY_END":

                //空ステージのスコアを設定
                m_nScore[(int)STAGE_STATE.SKY] = (int)(m_fTotalDistance - m_fPreDistance);
                m_cDataManager.GetComponent<DataManager>().SetData("sky_score", m_nScore[(int)STAGE_STATE.SKY]);

                //トータルスコアを設定
                int nTotal = 0;
                for (int cnt = 0; cnt < (int)STAGE_STATE.NUM; cnt++){nTotal += m_nScore[cnt];}
                m_cDataManager.GetComponent<DataManager>().SetData("total_score", nTotal);

                m_fTragetSpeed = INIT_SPEED;                         //速度の目標値を初期化
                m_nNowPlayer = (int)PLAYER_STATE.FIRST;              //プレイヤーの状態を初期化
                break;
        }
    }

    //プレイヤーの状態に関連する処理
    private void PlayerState()
    {
		//1フレーム前のプレイヤーの状態を保持
        m_nPrePlayer = m_nNowPlayer;
		
        //1段階目
        if (m_nLevel < 2)
        {
            m_nNowPlayer = (int)PLAYER_STATE.FIRST;
        }
        //2段階目
        else if (m_nLevel < 5)
        {
            m_nNowPlayer = (int)PLAYER_STATE.SECOND;
        }
        //3段階目
        else
        {
            m_nNowPlayer = (int)PLAYER_STATE.THIRD;
        }

        //段階が上がっていた場合
        if (m_nPrePlayer < m_nNowPlayer)
        {
            //サウンドの再生
            audio.PlayOneShot(m_cAudio[(int)PLAYER_AUDIO.CHANGE]);

            //	アップグレードエフェクトを生成.
            GetComponent<EffectCreator>().Create("UPGRADE");
        }
    }

    //プレイヤーのテクスチャ設定
    private void SetTexture()
    {
        //状態が変化している場合テクスチャを変更する
        if (m_nPrePlayer != m_nNowPlayer || m_nPreStage != m_nNowStage)
        {
            renderer.material.mainTexture = m_cTexture[m_nNowStage,m_nNowPlayer];
        }
    }
}
