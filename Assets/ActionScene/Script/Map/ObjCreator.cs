using UnityEngine;
using System.Collections;

//================================
//アイテムや障害物を作成します。.
//================================
public class ObjCreator : MonoBehaviour
{
    [SerializeField]
    enum LAND_OBJECT
    {
        LAND_SPEED_UP = 0,
        LAND_SPEED_DOWN,
        NUM
    }

    enum SEA_OBJECT
    {
        SEA_SPEED_UP = 0,
        SEA_SPEED_DOWN,
        SEA_ENEMY,
        NUM
    }

    enum SKY_OBJECT
    {
        SKY_SPEED_UP = 0,
        SKY_SPEED_DOWN,
        SKY_ENEMY,
        SKY_BLIND,
        NUM
    }

    private const int LANE_NUM = 4;                        //オブジェクトを生成するレーンの数
    public GameObject[] m_cLandObj = new GameObject[(int)LAND_OBJECT.NUM];
    public GameObject[] m_cSeaObj = new GameObject[(int)SEA_OBJECT.NUM];
    public GameObject[] m_cSkyObj = new GameObject[(int)SKY_OBJECT.NUM];

    private GameObject m_cGameSystem;                      //ゲームシステム(シーン管理など)
    private Vector3[] m_vPoint= new Vector3[LANE_NUM];
    
	// Use this for initialization
	void Start ()
    {
        //=======================================
        //オブジェクトの生成ポイントを算出する.
        //=======================================

        //ObjCreatorの座標をカメラのY軸上に移動する
        gameObject.transform.position = new Vector3(Camera.main.transform.position.x, gameObject.transform.position.y, Camera.main.transform.position.z);

        float fLaneWidth = (float)(1.0f / LANE_NUM);
        Vector3 vLane;
		
        for (int cnt = 0; cnt < LANE_NUM; cnt++)
        {
            //2D座標から3D座標に変換する.
            vLane.x = (fLaneWidth / 2.0f) + (fLaneWidth * (float)cnt);
            vLane.y = 1.1f;
            vLane.z = Vector3.Distance(gameObject.transform.position, Camera.main.transform.position);

            m_vPoint[cnt] = Camera.main.ViewportToWorldPoint(vLane);
        }

        m_cGameSystem = GameObject.Find("GameSystem");          //ゲームシステムのオブジェクトを取得

	}
	
	// Update is called once per frame
	void Update ()
    {
        //とりあえずランダムに生成しておきます。.
        if (Random.Range(0, 100) < 2)
        {
            GameObject cSetObj = null;

            //ステージによって配置するものを決定する
            switch (m_cGameSystem.GetComponent<GameSystem>().GetStatus())
            {
                //陸ステージ
                case "LAND":
                         cSetObj = m_cLandObj[Random.Range(0, (int)LAND_OBJECT.NUM)];			//障害物を決定する
                    break;

                //海ステージ
                case "SEA":
                         cSetObj = m_cSeaObj[Random.Range(0, (int)SEA_OBJECT.NUM)];				//障害物を決定する
                    break;

                //空ステージ
                case "SKY":
                         cSetObj = m_cSkyObj[Random.Range(0, (int)SKY_OBJECT.NUM)];				//障害物を決定する
                    break;
            }

            int nLane = Random.Range(0, LANE_NUM);		//どのレーンに出すか

            //オブジェクトを生成する
            if(cSetObj != null)Instantiate(cSetObj, m_vPoint[nLane], new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
        }

	}
}
