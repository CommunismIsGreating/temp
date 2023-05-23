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
        //垂直同步计数设置为0，才能锁帧，否则锁帧代码无效。
        //QualitySettings.vSyncCount = 0;
        //设置游戏帧数
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
