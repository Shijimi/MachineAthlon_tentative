using UnityEngine;
using System.Collections;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject m_cScoreBoardNum;

    private uint m_nDigit = 0;
    private GameObject[] m_cNums;
    private bool m_bCreated = false;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    //スコアボードの作成
    public void Create(uint _digit)
    {
        //桁数分のオブジェクトを確保
        m_cNums = new GameObject[_digit];

        //桁数を保持
        m_nDigit = _digit;

        if (m_cScoreBoardNum != null)
        {
            Vector3 vPos = gameObject.transform.position;
            Quaternion qRot = gameObject.transform.rotation; 
            Vector3 vScale = gameObject.transform.localScale;

            //オブジェクトを生成する
            for (int cnt = 0; cnt < _digit; cnt++)
            {
                m_cNums[cnt] = (GameObject)Instantiate(m_cScoreBoardNum,vPos + new Vector3(vScale.x * 5 * (_digit - cnt), 0.0f, 0.0f),qRot);
                m_cNums[cnt].transform.localScale = vScale;
                m_cNums[cnt].transform.parent = gameObject.transform;
            }
        }

        m_bCreated = true;              //作成済みフラグ
    }

    //数値の設定
    public void Set(uint _score)
    {
        //ボードが作成されていない場合処理を行わない
        if (!m_bCreated) { return ;}

        uint nScore = _score;

        for (int cnt = 0; cnt < m_nDigit;cnt++ )
        {
            uint nNum = (nScore % 10);        //1桁ずつ取り出す
            nScore /= 10;
            m_cNums[cnt].GetComponent<ScoreBoardNum>().Set(nNum);
        }
    }
}
