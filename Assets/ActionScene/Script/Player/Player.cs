using UnityEngine;
using System.Collections;

//===========================
//プレイヤーの制御を行います
//===========================
public class Player : MonoBehaviour
{
    [SerializeField]
    private const float INIT_SPEED = 1.0f;                                //初期速度
    private const float MAX_SPEED = 5.0f;                                //最高速度

    private float fSpeed;                                                 //プレイヤーのスピード
    private int nCenter;                                                  //画面幅の半分
    private float fDistance;                                              //総移動距離

	// Use this for initialization
	void Start()
    {
        fSpeed = INIT_SPEED;                                //初期スピードを設定
        nCenter = Screen.width / 2;                         //画面幅の半分
        fDistance = 0.0f;                                   //総移動距離を初期化
	}
	
	// Update is called once per frame
	void Update()
    {
        //============
	    //操作
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

        //左クリック
        if (Input.GetMouseButton(0) && Camera.main.WorldToScreenPoint(gameObject.transform.position).x > 0)
        {
            //左に移動する
            gameObject.transform.position -= new Vector3(1.0f, 0.0f, 0.0f);
        }

        //右クリック
        if (Input.GetMouseButton(1) && Camera.main.WorldToScreenPoint(gameObject.transform.position).x < Screen.width)
        {
            //右に移動する
            gameObject.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
        }

        //総移動距離を加算
        fDistance += fSpeed;
	}

    //オブジェクトに衝突した瞬間
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            //加速アイテムの場合
            case "item_speed":

                //速度を加算
                if(fSpeed < MAX_SPEED)fSpeed += 0.5f;

                //アイテムを消す
                Destroy(collision.gameObject);

            break;

            //壊れない障害物
            case "obs_static":


            break;

            //壊れる障害物
            case "obs_break":

                //初期速度より遅くならない場合
                if(fSpeed > INIT_SPEED)fSpeed -= 0.5f;

                //消滅させる
                Destroy(collision.gameObject);

            break;
        }
    }

    //スピードを返す
    public float GetSpeed() { return fSpeed; }

    //総移動距離を返す
    public float GetDistance() { return fDistance; }
}
