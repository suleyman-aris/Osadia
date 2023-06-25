using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IGetHealthSystem    
{
    [SerializeField]private int playerHealth;
    //public int arcadiumAmount;

    private HealthSystem healthSystem;
    private FPS_MovementScript movement;

    public HealthSystem GetHealthSystem()
    {
        return this.healthSystem;
    }

    private void Awake()
    {
        healthSystem= new HealthSystem(playerHealth); 
        movement = GetComponent<FPS_MovementScript>();

    }
    private void Start()
    {
       
    }


}
