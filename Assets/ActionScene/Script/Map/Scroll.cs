using UnityEngine;
using System.Collections;

/*
=====================================
スクロール
=====================================
このスクリプトを適用させると
プレイヤーの速さに応じて移動します。
=====================================
*/

public class Scroll : MonoBehaviour
{
    [SerializeField]
    private GameObject cPlayer;

	//オブジェクトが消滅するときのZ座標.
	private const float OBJ_REMOVE_POS_Z = -60;

	// Use this for initialization
	void Start ()
    {
		//プレイヤーのゲームオブジェクトを取得する
        cPlayer = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
		//プレイヤーの速さに応じて座標を移動させる.
        gameObject.transform.position -= new Vector3(0.0f, 0.0f, cPlayer.GetComponent<Player>().GetSpeed());
		
		//画面外に出た場合はオブジェクトを削除する.
        if (gameObject.transform.position.z < OBJ_REMOVE_POS_Z){Destroy(gameObject);}

        //フェード中の場合オブジェクトを削除する
        if (Fade.FadeFlg())
        {
            Destroy(gameObject);
        }
	}
}
