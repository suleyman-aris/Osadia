using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStarter : MonoBehaviour
{
    public Transform startPoint;
    private Vector3 startPosition;
    public Transform playerTransform;
    private void Awake()
    {
        RunManager.isOnRun = false;
        RunEnder.isDead = false;
        RunManager.OnRunning += StartRun;
    }

    private void Start()
    {
       startPosition = startPoint.position;
    }

    public void StartRun(object sender, EventArgs e)
    {
        RunManager.isOnRun=true;
        if (playerTransform != null && startPoint != null)
        { 
            playerTransform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
            Debug.Log("Run Baþladý" + RunManager.isOnRun);
        }
           
    }
}
