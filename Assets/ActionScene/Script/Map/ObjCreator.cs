using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//================================
//アイテムや障害物を作成します。.
//================================
public class ObjCreator : MonoBehaviour
{
    //ステージ
    enum STAGE
    {
        LAND = 0,
        SEA,
        SKY,
        NUM
    }

    //陸ステージのオブジェクトのタイプ
    enum LAND_OBJECT
    {
        LAND_SPEED_UP = 0,
        LAND_SPEED_DOWN,
        NUM
    }

    //海ステージのオブジェクトのタイプ
    enum SEA_OBJECT
    {
        SEA_SPEED_UP = 0,
        SEA_SPEED_DOWN,
        SEA_ENEMY,
        NUM
    }

    //空ステージのオブジェクトのタイプ
    enum SKY_OBJECT
    {
        SKY_SPEED_UP = 0,
        SKY_SPEED_DOWN,
        SKY_ENEMY,
        SKY_BLIND,
        NUM
    }

    private const int LANE_NUM = 4;                        //オブジェクトを生成するレーンの数
    private int m_nCreateObjID;                            //生成するオブジェクトのID
    private int m_nNowStageNum;                            //現在のステージ番号
    private float m_fStageTime;                            //1ステージあたりの時間
    private float m_fStageStartTime;                       //ステージの開始時間
    private float m_fCreateTime;                           //ステージ開始を0としてオブジェクトを生成可能な秒数
    private float m_fCreateBet;                             //オブジェクトを生成する間隔
    private float m_fPreCreateTime;                          //前回オブジェクトを生成した時間

    //オブジェクトと生成個数
    //Unity側で変更できるようにあえて構造体を使用していません

    //陸ステージ
    public GameObject[] m_cLandObj = new GameObject[(int)LAND_OBJECT.NUM];  //オブジェクト
    public int[] m_nLandObjNum = new int[(int)LAND_OBJECT.NUM];             //個数

    //海ステージ
    public GameObject[] m_cSeaObj = new GameObject[(int)SEA_OBJECT.NUM];    //オブジェクト
    public int[] m_nSeaObjNum = new int[(int)SEA_OBJECT.NUM];               //個数

    //空ステージ
    public GameObject[] m_cSkyObj = new GameObject[(int)SKY_OBJECT.NUM];    //オブジェクト
    public int[] m_nSkyObjNum = new int[(int)SKY_OBJECT.NUM];               //個数


    private GameObject m_cGameSystem;                      //ゲームシステム(シーン管理など)
    private Vector3[] m_vPoint= new Vector3[LANE_NUM];

    private int[][] m_nObjList;
    private int[] m_nTotalObjNum;
    private GameObject[][] m_cObj;

	// Use this for initialization
	void Start ()
    {
        //---------------------------------------
        //オブジェクトの生成ポイントを算出する.
        //---------------------------------------

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

        //----------------------------------------
        //ステージ関連の値を取得
        //----------------------------------------
        m_cGameSystem = GameObject.Find("GameSystem");          //ゲームシステムのオブジェクトを取得
        m_fStageTime = m_cGameSystem.GetComponent<GameSystem>().GetStageTime();
        m_fCreateTime = m_fStageTime - 3.0f;

        //---------------------------------------
        //オブジェクトリストを作成する
        //---------------------------------------
        m_nObjList = new int[(int)STAGE.NUM][];
        m_nTotalObjNum = new int[(int)STAGE.NUM];
        m_cObj = new GameObject[(int)STAGE.NUM][];

        //各ステージのオブジェクトリストを取得
        m_nObjList[(int)STAGE.LAND] = this.GetObjList(m_nLandObjNum, (int)LAND_OBJECT.NUM);
        m_nObjList[(int)STAGE.SEA] = this.GetObjList(m_nSeaObjNum, (int)SEA_OBJECT.NUM);
        m_nObjList[(int)STAGE.SKY] = this.GetObjList(m_nSkyObjNum, (int)SKY_OBJECT.NUM);

        //各ステージのオブジェクトの総数を取得する
        m_nTotalObjNum[(int)STAGE.LAND] = this.GetTotal(m_nLandObjNum);
        m_nTotalObjNum[(int)STAGE.SEA] = this.GetTotal(m_nSeaObjNum);
        m_nTotalObjNum[(int)STAGE.SKY] = this.GetTotal(m_nSkyObjNum);

        //オブジェクト
        m_cObj[(int)STAGE.LAND] = m_cLandObj;
        m_cObj[(int)STAGE.SEA] = m_cSeaObj;
        m_cObj[(int)STAGE.SKY] = m_cSkyObj;

	}
	
	// Update is called once per frame
	void Update ()
    {
            GameObject cSetObj = null;

            //ステージによって配置するものを決定する
            switch (m_cGameSystem.GetComponent<GameSystem>().GetStatus())
            {
                //各ステージの開始時
                case "LAND_START":
                case "SEA_START":
                case "SKY_START":

                    //ステージ番号を取得
                    m_nNowStageNum =  m_cGameSystem.GetComponent<GameSystem>().GetNowStageNum();
                    m_fStageStartTime = m_fStageTime * (float)m_nNowStageNum;

                    m_nCreateObjID = 0;
                    m_fPreCreateTime = 0.0f;
                    m_fCreateBet = m_fCreateTime / m_nTotalObjNum[m_nNowStageNum];

                    break;
            }

            float fNowTime = m_cGameSystem.GetComponent<GameSystem>().GetTime() - m_fStageStartTime;

            if (m_nTotalObjNum[m_nNowStageNum] > m_nCreateObjID && fNowTime - m_fPreCreateTime > m_fCreateBet)
            {
                    cSetObj = m_cObj[m_nNowStageNum][m_nObjList[m_nNowStageNum][m_nCreateObjID]];
                    m_fPreCreateTime = fNowTime;
                    m_nCreateObjID++;
            }

            int nLane = UnityEngine.Random.Range(0, LANE_NUM);		//どのレーンに出すか

            //オブジェクトを生成する
            if(cSetObj != null)Instantiate(cSetObj, m_vPoint[nLane], new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));

	}

    //==========================
    //オブジェクトリストの取得
    //==========================
    int[] GetObjList(int[] _nNum,int _nObjNum)
    {
        //オブジェクト分の配列を確保
        int[] nObjList = new int[this.GetTotal(_nNum)];

        int nOffset = 0;

        //配列にオブジェクト番号を格納していく
        for (int cnt = 0; cnt < _nObjNum;cnt++ )
        {
            for (int cnt2 = 0; cnt2 < _nNum[cnt]; cnt2++,nOffset++)
            {
                nObjList[nOffset] = cnt;
            }
        }

        //配列をシャッフル
        nObjList = nObjList.OrderBy(i => Guid.NewGuid()).ToArray();

        return nObjList;
    }

    //============================
    //int型配列の総和を求める
    //============================
    int GetTotal(int[] _nArray)
    {
        int nTotal = 0;

        foreach(int nNum in _nArray)
        {
            nTotal += nNum;
        }

        return nTotal;
    }
}
