using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    private const int PAGE_NUM = 3;     //ページ数

    private int m_nPage = 0;
    private int m_nPrePage = -1;

    private bool m_bScroll = false;     //スクロールフラグ
    private bool m_bEnable = false;     //チュートリアル中フラグ

    private GameObject m_cButtonManager;

    public GameObject[] m_cPages = new GameObject[PAGE_NUM];            //ページの作成元オブジェクト
    private GameObject[] m_cCreatePages = new GameObject[PAGE_NUM];     //作成されたページのオブジェクト

    public float m_fScrollSpeed = 1.0f;                         //スクロール速度

	// Use this for initialization
	void Start ()
    {
        m_cButtonManager = GameObject.Find("ButtonManager");
        m_nPage = 0;
        m_nPrePage = -1;
        m_bScroll = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //ページが変わっていたら
        if (m_bEnable && m_nPage != m_nPrePage) { m_bScroll = true; }

        //スクロール処理
        if(m_bScroll)
        {
            //次のページ
            if(m_nPage > m_nPrePage)
            {
                //ページが中央に達していない場合
                if (m_cCreatePages[m_nPage].transform.position.x > Camera.main.transform.position.x)
                {
                    for (int cnt = 0; cnt < PAGE_NUM; cnt++)
                    {
                        m_cCreatePages[cnt].transform.position += new Vector3(-m_fScrollSpeed, 0.0f, 0.0f);
                    }
                }
                else
                {
                    m_nPrePage = m_nPage;       //前のページ番号を更新
                    m_bScroll = false;          //スクロール終了
                    m_cButtonManager.GetComponent<ButtonManager>().SetTarget("tutorial");
                }
            }
            //前のページ
            else if (m_nPage < m_nPrePage)
            {
                //ページが中央に達していない場合
                if (m_cCreatePages[m_nPage].transform.position.x < Camera.main.transform.position.x)
                {
                    for (int cnt = 0; cnt < PAGE_NUM; cnt++)
                    {

                        m_cCreatePages[cnt].transform.position += new Vector3(m_fScrollSpeed, 0.0f, 0.0f);
                    }
                }
                //移動が完了した
                else
                {
                    m_nPrePage = m_nPage;       //前のページ番号を更新
                    m_bScroll = false;          //スクロール終了
                    m_cButtonManager.GetComponent<ButtonManager>().SetTarget("tutorial");

                }
            }
        }
	}

    //チュートリアルのON/OFF設定
    public void Set(bool _enable)
    {
        for (int cnt = 0; cnt < PAGE_NUM;cnt++ )
        {
            if(_enable)
            {
                if (m_cPages[cnt] != null)
                {
                    //ページをすべて生成
                    m_cCreatePages[cnt] = (GameObject)Instantiate(m_cPages[cnt]);
                }
            }
            else
            {
                //ページをすべて削除
                Destroy(m_cCreatePages[cnt]);
            }
        }

        m_bEnable = _enable;    //チュートリアルON/OFF
        this.Start();           //初期化
    }

    //ページめくり
    public void Page(int _page)
    {
        //スクロール中でない
        if (!m_bScroll)
        {
            int nCheck = m_nPage + _page;

            //ページが範囲外でなければ
            if (nCheck >= 0 && nCheck < PAGE_NUM)
            {
                m_nPrePage = m_nPage;
                m_nPage += _page;            //ページを設定
            }
            //ページめくりに失敗した場合
            else
            {
                //入力目的を"チュートリアル"に設定
                m_cButtonManager.GetComponent<ButtonManager>().SetTarget("tutorial");
            }
        }
    }
}
