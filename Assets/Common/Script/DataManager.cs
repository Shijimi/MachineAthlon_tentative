using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//=========================================
//データを管理するスクリプトです。
//シーン間の値の受け渡しなどに使用します。
//このスクリプトを適用させたオブジェクトは
//シーンが切り替わっても削除されません。
//=========================================

public class DataManager: SingletonMonoBehaviour<DataManager>
{
    //object型版(今回はfloatのみなので使わない)
    ////データ格納用の連想配列
    //private Hashtable m_cTable;

    //void Awake()
    //{
    //    if (this != Instance)
    //    {
    //        Destroy(gameObject);
    //    }
    //    m_cTable = new Hashtable();
    //}

    //// Use this for initialization
    //void Start ()
    //{
    //    DontDestroyOnLoad(gameObject);          //シーンが変わっても削除しない

    //}

    ////データの設定
    ////tag:データを識別するタグ。データを呼び出す際に使用します。
    ////data:保存するデータ。object型なのでどんな型でも格納できます。
    ////tagは型が判別できるように先頭に『(型名)_』をつけましょう。
    //public void SetData(string tag,object data)
    //{
    //    m_cTable[tag] = data;
    //}

    ////データの取得
    ////戻り値はobject型なのでキャストが必要
    //public object GetData(string tag)
    //{
    //    if (!m_cTable.ContainsKey(tag))
    //   {

    //    }

    //    return m_cTable[tag];
    //}

    //データ格納用の連想配列
    private List<DataTable> m_cTable;

    void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
        }

        m_cTable = new List<DataTable>();
    }

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);          //シーンが変わっても削除しない
    }

    //データの設定
    public void SetData(string _tag, int _data)
    {
        //同名のタグがあれば上書きを行う
        foreach (DataTable cTable in m_cTable)
        {
            if (cTable.GetTag() == _tag)
            {
                cTable.SetValue(_data);
                return;
            }
        }

        //同名がなければ追加
        m_cTable.Add(new DataTable(_tag,_data));
    }

    //データの取得
    public int GetData(string _tag)
    {
        //同名のタグがあれば上書きを行う
        foreach (DataTable cTable in m_cTable)
        {
            if (cTable.GetTag() == _tag)
            {
                return cTable.GetValue();
            }
        }

        //データなし
        return 0;
    }
}

public class DataTable
{
    private string m_strTag;
    private int m_nValue;

    //コンストラクタ
    public DataTable(string _tag, int _value)
    {
        m_strTag = _tag;
        m_nValue = _value;
    }

    public string GetTag() { return m_strTag; }
    public int GetValue() { return m_nValue; }
    public void SetValue(int _value) { m_nValue = _value; }
}
