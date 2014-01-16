using UnityEngine;
using System.Collections;

//==================================================
//ステージごとのテクスチャ切り替えを行うスクリプト
//==================================================

public class StageTexture : MonoBehaviour
{
    public Texture m_cLand, m_cSea, m_cSky;
    private GameObject m_cGameSystem;

	// Use this for initialization
	void Start ()
    {
		//	ゲームシステムのオブジェクトを取得.
        m_cGameSystem = GameObject.Find("GameSystem");
	}
	
	// Update is called once per frame
	void Update () 
    {
        switch (m_cGameSystem.GetComponent<GameSystem>().GetStatus())
        {
            //陸ステージ
            case "LAND":
	                 gameObject.renderer.material.mainTexture = m_cLand;
                break;

            //海ステージ
            case "SEA":
                gameObject.renderer.material.mainTexture = m_cSea;
                break;

            //空ステージ
            case "SKY":
                gameObject.renderer.material.mainTexture = m_cSky;
                break;
        }
	}
}
