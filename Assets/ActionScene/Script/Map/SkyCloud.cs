using UnityEngine;
using System.Collections;

public class SkyCloud : MonoBehaviour
{
    private float m_fSpeed;
	// Use this for initialization
	void Start ()
    {
        renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        m_fSpeed = Random.Range(1, 5) * 0.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position -= new Vector3(0.0f, 0.0f, m_fSpeed);
	}
}
