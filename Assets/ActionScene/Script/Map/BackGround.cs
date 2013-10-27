using UnityEngine;
using System.Collections;

//=====================================
//背景のスクロールや切り替えを行います
//=====================================
public class BackGround : MonoBehaviour {

    [SerializeField]
    private Vector2 vPos;
    private Vector2 vMove = new Vector2(0,0);

    private GameObject cPlayer;
    private GameObject cGameSystem;

	// Use this for initialization
	void Start()
    {
        cPlayer = GameObject.Find("Player");            //プレイヤーのオブジェクトを取得
        cGameSystem = GameObject.Find("GameSystem");    //ゲームシステムのオブジェクトを取得
	}
	
	// Update is called once per frame
	void Update()
    {
        vMove = new Vector2(0, -(cPlayer.GetComponent<Player>().GetSpeed())*0.004f);

        //移動ベクトルを座標に加算
        vPos += vMove;

        //テクスチャを動かす
        renderer.material.mainTextureOffset = vPos;

        //ステージのステータスから切り替えを行う
        switch(cGameSystem.GetComponent<GameSystem>().GetStatus())
        {
            //陸ステージ開始
            case "LAND_START":
                
                //陸のテクスチャに変更

            break;

            //陸ステージ中の処理
            case "LAND":
            break;

            //陸ステージの終了
            case "LAND_END":

            break;

            //海ステージ開始
            case "SEA_START":

                //海のテクスチャに変更

            break;

            //海ステージ中の処理
            case "SEA":
            break;

            //海ステージの終了
            case "SEA_END":


            break;

            //空ステージ開始
            case "SKY_START":

                //空のテクスチャに変更

            break;

            //空ステージ中の処理
            case "SKY":
            break;

            //空ステージの終了
            case "SKY_END":

            break;
            
        }
    }
}
