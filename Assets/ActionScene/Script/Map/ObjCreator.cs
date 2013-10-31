using UnityEngine;
using System.Collections;

//================================
//アイテムや障害物を作成します。.
//================================
public class ObjCreator : MonoBehaviour
{
    [SerializeField]
    private const int LANE_NUM = 4;     //オブジェクトを生成するレーンの数.

    public GameObject[] m_cObj = new GameObject[10];
    private Vector3[] m_vPoint;
    
	// Use this for initialization
	void Start ()
    {
        //=======================================
        //オブジェクトの生成ポイントを算出する.
        //=======================================
        m_vPoint = new Vector3[LANE_NUM];

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
	}
	
	// Update is called once per frame
	void Update ()
    {
        //とりあえずランダムに生成しておきます。.
        if(Random.Range(0,100) < 2)
        {
            int nType = Random.Range(0, 2);				//どの障害物を出すか
            int nLane = Random.Range(0,LANE_NUM);		//どのレーンに出すか
			
			//オブジェクトを生成する
            Instantiate(m_cObj[nType], m_vPoint[nLane], new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
        }
	}
}
