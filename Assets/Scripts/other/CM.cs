using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCameraBase[] cm=new CinemachineVirtualCameraBase[4];
    [SerializeField]

    CinemachineVirtualCameraBase m_Camera;
    [SerializeField]PlayerInput input;
    [SerializeField] int pos_index = 0;
    private void Start()
    {
        if (cm[0])
        {
            cm[0].MoveToTopOfPrioritySubqueue();
        }
    }
    public void ChangePos()
    {
        cm[(++pos_index)%4].MoveToTopOfPrioritySubqueue();
    }
    public void MView()
    {
        if(m_Camera != null)
        {
            PlayerControllor.Instance.WalkV = 0;
            m_Camera.MoveToTopOfPrioritySubqueue();
        }
    }
    public void ReturnPlayer()
    {
        cm[pos_index].MoveToTopOfPrioritySubqueue();
        PlayerControllor.Instance.WalkV = 10;
    }
    private void Update()
    {
        if (m_Camera.enabled)
        {
            if(Input.GetKey(KeyCode.A)) {
                m_Camera.transform.position = new Vector3(m_Camera.transform.position.x+Time.deltaTime*10, m_Camera.transform.position.y, m_Camera.transform.position.z);
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_Camera.transform.position = new Vector3(m_Camera.transform.position.x - Time.deltaTime * 10, m_Camera.transform.position.y, m_Camera.transform.position.z);
            }
            if (Input.GetKey(KeyCode.W))
            {
                m_Camera.transform.position = new Vector3(m_Camera.transform.position.x , m_Camera.transform.position.y, m_Camera.transform.position.z+Time.deltaTime*10);
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_Camera.transform.position = new Vector3(m_Camera.transform.position.x , m_Camera.transform.position.y , m_Camera.transform.position.z- Time.deltaTime * 10);
            }
            
        }
        else
        {
            PlayerControllor.Instance.WalkV = 10;
        }
    }
}
