using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera m_Camera;
    public Transform m_Transform;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera =  GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        m_Camera.transform.localPosition = new Vector3( m_Transform.position.x,m_Transform.position.y,0);
    }
}
