using UnityEngine;
using System.Collections;

//=========================================
//データを管理するスクリプトです。
//シーン間の値の受け渡しなどに使用します。
//このスクリプトを適用させたオブジェクトは
//シーンが切り替わっても削除されません。
//=========================================

public class DataManager: SingletonMonoBehaviour<DataManager>
{
    //データ格納用の連想配列
    private Hashtable m_cTable;

    void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);          //シーンが変わっても削除しない
        m_cTable = new Hashtable();
	}

    //データの設定
    //tag:データを識別するタグ。データを呼び出す際に使用します。
    //data:保存するデータ。object型なのでどんな型でも格納できます。
    //tagは型が判別できるように先頭に『(型名)_』をつけましょう。
    public void SetData(string tag,object data)
    {
        m_cTable[tag] = data;
    }

    //データの取得
    //戻り値はobject型なのでキャストが必要
    public object GetData(string tag)
    {
        if (m_cTable[tag] == null) { return 0; }

        return m_cTable[tag];
    }

}
