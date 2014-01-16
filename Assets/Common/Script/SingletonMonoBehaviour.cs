using UnityEngine;

//シングルトンを実装するための継承用クラス
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_tInstance;

    public static T Instance
    {
        get
        {
            if (m_tInstance == null)
            {
                m_tInstance = (T)FindObjectOfType(typeof(T));
            }

            return m_tInstance;
        }
    }
}
