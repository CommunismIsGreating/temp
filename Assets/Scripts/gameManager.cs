using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    [SerializeField]private Canvas canvas;
    public bool isESC=false;
    private void Awake()
    {
        if(instance == null)
        instance = this;
        //��ֱͬ����������Ϊ0��������֡��������֡������Ч��
        //QualitySettings.vSyncCount = 0;
        //������Ϸ֡��
        //Application.targetFrameRate = 60;
    }
    private void Start()
    {
        canvas.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            CanvasONorOFF();
        }
    }
    void CanvasONorOFF()
    {
        if(canvas != null)
        {
            if (!canvas.enabled)
            {
                canvas.enabled = true;
                isESC = true;
            } 
            else
        {
            canvas.enabled=false;
                isESC=false;
        }
        }
       
    }
}
