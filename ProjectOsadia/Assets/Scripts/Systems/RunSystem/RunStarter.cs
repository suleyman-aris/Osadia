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
        RunManager.isOnRun = true;
        RunEnder.isDead = false;
        RunManager.OnRunning += StartRun;
    }

    private void Start()
    {
       startPosition = startPoint.position;
    }

    private void StartRun()
    {
        if (playerTransform != null && startPoint != null)
        { 
            playerTransform.localPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z);
            Debug.Log("Run Baþladý" + RunManager.isOnRun);
        }
           
    }
}
