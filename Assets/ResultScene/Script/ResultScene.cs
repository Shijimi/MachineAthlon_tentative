using UnityEngine;
using System.Collections;

public class ResultScene : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_cBackGrounds = new GameObject[BG_NUM];
    [SerializeField]
    private GameObject[] m_cScoreBoards = new GameObject[BG_NUM];

    private const int BG_NUM = 4;
    private const float FADE_TIME = 2.0f;
    private float m_fStartTime = 0.0f;

	// Use this for initialization
	void Start ()
    {
        m_fStartTime = Time.time;           //リザルトの開始時間
	}
	
	// Update is called once per frame
	void Update ()
    {
        float fTime = Time.time - m_fStartTime;

        //背景の最後の1枚は透明化しないので　BG_NUM - 1
        for (int cnt = 0; cnt < BG_NUM - 1; cnt++)
        {
            if (cnt == (int)(fTime / FADE_TIME))
            {
                //透明化アニメーション
                m_cBackGrounds[cnt].renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f - ((fTime / FADE_TIME) - (float)cnt));
            }
        }

        //if (fTime / FADE_TIME > (float)BG_NUM - 1.0f)
        //{
        //    GameObject.Find("ButtonManager").GetComponent<ButtonManager>().SetTarget("result_end");
        //}
	}
}
