using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
using System;
using System.Numerics;
using Unity.VisualScripting;
using Vector3 = UnityEngine.Vector3;
//[System.Serializable]//δ�̳�monobehavior���಻��ʾ,Ҫ���л�
//public class EventV3:UnityEvent<Vector3> { }
public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;

    //public event Action<UnityEngine.Vector3> MouseClick;
    public event Action<Transform,Transform> MouseClickWithInteract;
    public Transform playerPosition;
    [SerializeField]private CatchDect catchD;
    [SerializeField]private Catch catchT;
    [SerializeField] private Tick tick;
    [SerializeField] private CM camera1;
    [SerializeField] private bool IsM=false;
    [Range(0f,2f)]public float QE_TimeSpeed = 0.5f;
    private bool canFire=false;
    private float timer;
    //public event Action<Transform, Transform> MouseClickWithInteractOnly;


    //�洢����ײ����������Ϣ
    public RaycastHit hit;
    //public Vector3 Dir=>(hit.point-PlayerControllor.Instance.transform.position).normalized;

    Collider temp;


    //��Ծ,Ͷ���߶�ϵ��
    [SerializeField] private float Highcoefficient = 1.75f;

    public void move(Transform Throw,Vector3 point)
    {
        Throw.position=new Vector3(point.x, point.y+Highcoefficient, point.z);
        Throw.gameObject.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
    }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        MouseManager.instance.MouseClickWithInteract += ChangePosition;
        temp = null;
       
    }
    private void Update()
    {
        SetCursorTexture();
        MouseControl();
        playerPosition = PlayerControllor.Instance.transform;
        timer += Time.deltaTime;
    }

    void SetCursorTexture()
    {
        //Debug.Log("SetCursorTexture");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit,Mathf.Infinity,~(1<<7)))
        {
            //�л������ͼ
        }
    }
    void MouseControl()
    {
        Time.timeScale = 1f;
        //Debug.Log("on");
        if (Input.GetMouseButtonDown(0) && hit.collider != null)
        {
            //if (hit.collider.gameObject.CompareTag("Ground")|| hit.collider.gameObject.CompareTag("NoMovableInt") || hit.collider.gameObject.CompareTag("Catch"))
            //{
                //Debug.Log("�ƶ�");
                //MouseClick?.Invoke(hit.point);

            //}
            //��ť����
            if (hit.collider.gameObject.CompareTag("NoMovableIntForever"))
            {
                Debug.Log("����������ť");
            }
        }
        //����������任λ��
        if (PlayerControllor.Instance.isQ && Input.GetMouseButtonDown(0) && (hit.collider.gameObject.CompareTag("Interact")|| hit.collider.gameObject.CompareTag("Catch")) &&PlayerControllor.Instance.IsInterAct(hit.collider.transform,playerPosition))
        {
            Debug.Log("���λ�ñ任");
            MouseClickWithInteract?.Invoke(hit.collider.gameObject.transform,PlayerControllor.Instance.transform);
            //PlayerControllor.Instance.StopSelf();
        }
        //����任λ��
        if (PlayerControllor.Instance.isE && Input.GetMouseButtonDown(0) && (hit.collider.gameObject.CompareTag("Interact") || hit.collider.gameObject.CompareTag("Catch")) && PlayerControllor.Instance.IsInterAct(hit.collider.transform, playerPosition))
        {
            if (temp == null) {
                temp = hit.collider;
                return;
            }
            else if (temp != hit.collider)
            {
                PosionChange(temp, hit.collider);
                //PlayerControllor.Instance.StopSelf();
                Debug.Log("����λ�ñ任");
                temp = null;
            }
            else
            {
                Debug.Log("ȡ������λ�ñ任");
                temp = null;
            }
        }
        //Debug.Log("" + Input.GetKey(KeyCode.R)  +" "+ hit.collider.gameObject.CompareTag("Catch") +" "+ PlayerControllor.Instance.IsCatch( hit.collider));
        //ץȡ;
        if (PlayerControllor.Instance.isR  && hit.collider.gameObject.CompareTag("Catch") && PlayerControllor.Instance.IsCatch( hit.collider))
        {
            Debug.Log("ץȡ");
            catchT = hit.collider.gameObject.GetComponent<Catch>();
            catchT.isCatch = true;
            timer = 0;
            //StartCoroutine(BeziesCurvor(hit.collider.transform, catchD.transform.position));
        }
        //Ͷ��
        if (PlayerControllor.Instance.isR && Input.GetMouseButtonDown(0) && hit.collider != null && !PlayerControllor.Instance.IsCatch(hit.collider)&&timer>2f)
        {
            Debug.Log("on");
            Vector3 temp = PlayerControllor.Instance.transform.position - hit.point;
            if (temp.magnitude > PlayerControllor.Instance.ThrowDistance_Max) return;
            Debug.Log("Ͷ��");
            catchT.isCatch = false;
            //StartCoroutine(BeziesCurvor(catchT.transform,hit.point));
            move(catchT.transform,hit.point);
            catchT=null;
        }
        //����
        if (PlayerControllor.Instance.isR && hit.collider.gameObject.CompareTag("Interact")&&!canFire)
        {
            Debug.Log("��ȡ");
            tick = hit.collider.gameObject.GetComponent<Tick>();
            tick.On = true;
            canFire = true;
        }
        if (!PlayerControllor.Instance.isR&&canFire&&Input.GetMouseButtonDown(1))
        {
            Debug.Log("����");
            tick.On = false;
            canFire=false;
            tick.Fire(hit.point-playerPosition.position);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(camera1 != null)
            {
                Debug.Log("ת��");
                PlayerControllor.Instance.angle += 90;
                camera1.ChangePos();
            }
        }
        if (Input.GetKeyDown(KeyCode.M)&&!IsM)
        {
            Debug.Log("M");
            camera1.MView();
            IsM = true;
        }else if(Input.GetKeyDown(KeyCode.M) && IsM)
        {
            camera1.ReturnPlayer();
            IsM = false;
        }
    }

    void PosionChange(Collider one, Collider two)
    {
        UnityEngine.Vector3 temp = one.gameObject.transform.position;
        one.gameObject.transform.position = two.gameObject.transform.position;
        two.gameObject.transform.position = temp;
    }
    public void ChangePosition(Transform one, Transform two)
    {
        UnityEngine.Vector3 temp = one.gameObject.transform.position;
        one.gameObject.transform.position = two.gameObject.transform.position;
        two.gameObject.transform.position = temp;
    }
}
