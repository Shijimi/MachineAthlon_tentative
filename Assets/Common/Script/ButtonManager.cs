using UnityEngine;
using System.Collections;


//=========================================
//ボタンの入力目的を管理するスクリプトです。
//このスクリプトを適用させたオブジェクトは
//シーンが切り替わっても削除されません。
//=========================================

public class ButtonManager : SingletonMonoBehaviour<ButtonManager>
{
    public string m_strTarget = "main";         //入力目的

    void Awake()
    {
        //重複する場合自滅させる
        if (this != Instance){Destroy(gameObject);}
    }

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);          //シーンが変わっても削除しない
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    //入力目的の取得
    public string GetTarget(){return m_strTarget;}

    //入力目的の設定
    public void SetTarget(string _target) { m_strTarget = _target; }

    //オブジェクトがクリックされたかどうかを判定する
    public bool Check(GameObject _obj)
    {
        RaycastHit[] sRayHit;

        Ray sRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //レイと当たるものすべて取得
        sRayHit = Physics.RaycastAll(sRay);

        //当たったもの中にオブジェクトがあるか
        foreach (RaycastHit sHit in sRayHit)
        {
            //レイと当たっていた場合
            if (sHit.collider.gameObject == _obj)
            {
                return true;
            }
        }

        return false;
    }
}
