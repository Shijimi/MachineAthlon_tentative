using UnityEngine;
using System.Collections;

//================================
//アイテムや障害物を作成します。
//================================
public class ObjCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Obj = new GameObject[10];
    
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        //とりあえずランダムに生成しておきます。
        if(Random.Range(0,100) < 2)
        {
            int Num = Random.Range(0, 2);
            Instantiate(Obj[Num], new Vector3(Random.Range(-33,33), 0.1f, 120.0f),new Quaternion(0.0f,180.0f,0.0f,0.0f));
        }
	}
}
