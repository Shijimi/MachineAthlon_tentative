using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public string m_strTarget;                      //入力目的
    public bool m_bDisenable = false;               //入力目的ではない場合描画しないフラグ

    private bool m_bClick = false;          //クリックされたかどうか
    private GameObject m_cButtonManager;    //ボタンマネージャーのゲームオブジェクト


	// Use this for initialization
	void Start ()
    {
        m_bClick = false;
        m_cButtonManager = GameObject.Find("ButtonManager");        //ボタンマネージャーのオブジェクトを取得
	}
	
	// Update is called once per frame
	void Update ()
    {
        //左クリック(タップ)された　かつ　入力目的が一致した場合 かつ　自分がクリックされた
        if (Input.GetKeyDown(KeyCode.Mouse0) 
            && m_cButtonManager.GetComponent<ButtonManager>().GetTarget() == m_strTarget
            && m_cButtonManager.GetComponent<ButtonManager>().Check(gameObject))
        {m_bClick = true;}
        else{m_bClick = false;}

        //入力目的が異なる場合非表示にする　かつ　入力目的が異なる場合
        if (m_bDisenable && m_cButtonManager.GetComponent<ButtonManager>().GetTarget() != m_strTarget)
        {
            //非表示
            gameObject.renderer.enabled = false;
        }
        else
        {
            //表示
            gameObject.renderer.enabled = true;
        }
	}

    //クリックが離されたとき
    void OnMouseUp()
    {
        m_bClick = false;
    }

    //クリックされたかどうかを返す
    public bool GetClick(){return m_bClick;}

    //入力目的を設定する
    public void SetTarget(string _target) { m_cButtonManager.GetComponent<ButtonManager>().SetTarget(_target); }
}
