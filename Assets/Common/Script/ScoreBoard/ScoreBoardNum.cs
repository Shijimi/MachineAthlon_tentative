using UnityEngine;
using System.Collections;

public class ScoreBoardNum : MonoBehaviour
{
    Material m_cMaterial;

    // Use this for initialization
    void Awake()
    {
        //マテリアルを取得
        m_cMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    public void Set(uint _num)
    {
        //オフセットを設定
        m_cMaterial.SetTextureOffset("_MainTex", new Vector2((float)_num / 10.0f, 0));
    }
}
