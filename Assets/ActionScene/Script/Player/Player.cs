using UnityEngine;
using System.Collections;

//===========================
//プレイヤーの制御を行います.
//===========================
public class Player : MonoBehaviour
{
    [SerializeField]
	//	初期速度.
    private const float INIT_SPEED	=	1.0f;
	//	最高速度.
    private const float MAX_SPEED	=	5.0f;
	//	最低速度.
	private	const float	MIN_SPEED	=	0.0f;
	
	//	プレイヤーのスピード.
    private float fSpeed;
	//	速度の目標値.
	private	float	m_fTragetSpeed;
	//	画面幅の半分.
    //private int nCenter;
	//	総移動距離.
    private float fDistance;
	//	毎フレームごとのアニメーションの状態.
	private	bool	m_AniFlg;

	// Use this for initialization
	void Start()
    {
		//	フラグを初期化.
		m_AniFlg	=	false;
		
		//	初期スピードを設定.
        fSpeed			=	0.0f;
		//	速度の目標値を初期化.
		m_fTragetSpeed	=	INIT_SPEED;
		
		//	画面幅の半分.
        //nCenter = Screen.width / 2;
		
		//	総移動距離を初期化.
        fDistance	=	0.0f;
	}
	
	// Update is called once per frame
	void Update()
    {
        //============
	    //操作.
        //============

        //画面がタップされている
        //if(Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    //画面の右半分タップされている間
        //    if(Input.GetTouch(0).position.x > nCenter)
        //    {
        //        //右に移動する
                
        //    }

        //    //画面の左半分がタップされている間
        //    if (Input.GetTouch(0).position.x < nCenter)
        //    {
        //        //左に移動する
        //    }
        //}

        gameObject.transform.rotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);

        //	左クリック.
		//	???
        if ( Input.GetMouseButton(0) && Camera.main.WorldToScreenPoint(gameObject.transform.position).x > 0 &&
			 m_fTragetSpeed != MIN_SPEED )
        {
            //	左に移動する.
            gameObject.transform.position -= new Vector3(1.0f, 0.0f, 0.0f);
        }

        //	右クリック.
        if ( Input.GetMouseButton(1) && Camera.main.WorldToScreenPoint(gameObject.transform.position).x < Screen.width &&
			 m_fTragetSpeed != MIN_SPEED )
        {
            //	右に移動する.
            gameObject.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
        }
		
		if( gameObject.animation.isPlaying == false && m_AniFlg == true )
		{
			//	速度の目標値を初期化.
			m_fTragetSpeed	=	INIT_SPEED;
		}

		//	アニメーションの状態を取得.
		m_AniFlg	=	gameObject.animation.isPlaying;
		
		//	速度を目標速度に近づける.
		fSpeed	=	Mathf.Lerp( fSpeed, m_fTragetSpeed, 0.05f );
		
        //	総移動距離を加算.
        fDistance += fSpeed;
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
	                if( m_fTragetSpeed < MAX_SPEED )
						m_fTragetSpeed += 0.5f;
	
	                //	アイテムを消す.
	                Destroy( collision.gameObject );
	
	            break;
	
	            //	壊れない障害物.
	            case "obs_static":
	
	
	            break;
	
	            //	妨害アイテムの場合.
	            case "obs_break":
				
					//	アニメーションの再生
					gameObject.animation.Play();
	
	                //	目標速度を0にする.
					m_fTragetSpeed	=	MIN_SPEED;
	
	                //	アイテムを消す.
	                Destroy( collision.gameObject );
	
	            break;
	        }
		}
    }

    //	スピードを返す.
    public float GetSpeed() { return fSpeed; }

    //	総移動距離を返す.
    public float GetDistance() { return fDistance; }
}
