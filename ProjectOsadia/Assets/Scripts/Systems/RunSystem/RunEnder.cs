using System;
using UnityEngine;
public class RunEnder : MonoBehaviour
{
    public static bool isDead;
    [SerializeField]private GameObject player;
    public Vector3 deathPos;
    public EventHandler OnRunEnd;
    
    private void Start()
    {
        
    }
    private void Death()
    {
        isDead = true;
        RunManager.isOnRun = false;
        
        player.GetComponent<FPS_MovementScript>().enabled = false; //ragdoll ekleyebiliriz aslýnda

        //log
        Debug.Log("Öldük " + isDead);
        Debug.Log("Run Bitti " + RunManager.isOnRun);
        //log

        deathPos = player.transform.position;
        OnRunEnd.Invoke(this, EventArgs.Empty);
    }
}
