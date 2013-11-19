using UnityEngine;
using System.Collections;

//=====================================
//背景のスクロールや切り替えを行います
//=====================================
public class BackGround : MonoBehaviour {
	
	public	Texture		m_LandTex;
	public	Texture		m_SeeTex;
	public	Texture		m_SkyTex;
	
	public	Material[]	materials;
	
    [SerializeField]
    private Vector2 vPos;
    private Vector2 vMove = new Vector2(0,0);

    private GameObject cPlayer;
    private GameObject cGameSystem;

	// Use this for initialization.
	void Start()
    {
		//	プレイヤーのオブジェクトを取得.
        cPlayer = GameObject.Find("Player");
		//	ゲームシステムのオブジェクトを取得.
        cGameSystem = GameObject.Find("GameSystem");
		
		//	レンダラーを陸に変更.
		renderer.material.mainTexture	=	m_LandTex;
	}
	
	// Update is called once per frame.
	void Update()
    {
        vMove = new Vector2(0, -(cPlayer.GetComponent<Player>().GetSpeed())*0.0085f);

        //移動ベクトルを座標に加算.
        vPos += vMove;

        //テクスチャを動かす.
        renderer.material.mainTextureOffset = vPos;

        //ステージのステータスから切り替えを行う.
        switch(cGameSystem.GetComponent<GameSystem>().GetStatus())
        {
            //陸ステージ開始.
            case "LAND_START":

            break;

            //陸ステージ中の処理.
            case "LAND":
            break;

            //陸ステージの終了.
            case "LAND_END":

            break;

            //海ステージ開始.
            case "SEA_START":

                //海のテクスチャに変更.
				renderer.material.mainTexture	=	m_SeeTex;

            break;

            //海ステージ中の処理.
            case "SEA":
            break;

            //海ステージの終了.
            case "SEA_END":


            break;

            //空ステージ開始.
            case "SKY_START":

                //空のテクスチャに変更.
				renderer.material.mainTexture	=	m_SkyTex;

            break;

            //空ステージ中の処理.
            case "SKY":
            break;

            //空ステージの終了.
            case "SKY_END":

            break;
            
        }
    }
}
