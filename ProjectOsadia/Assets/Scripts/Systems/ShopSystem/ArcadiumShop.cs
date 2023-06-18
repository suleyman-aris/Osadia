using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadiumShop : MonoBehaviour
{
    //shop elementler
    //shop fiyatlar
    //paran varmý
    //helva yapsana
    private FPS_MovementScript playerMovement;
    [SerializeField] private int cost = 5;

    private void Start()
    {
        playerMovement = FindObjectOfType<FPS_MovementScript>();
    }
    private void OnGUI()
    {
        if(GUI.Button(new Rect(50, 50, 150, 50), "Hýz'ý arttýr " + "Fiyatý : " + cost))
        {
            if(cost <= PlayerData.arcadiumCount)
            {
               PlayerData.arcadiumCount -= cost;
                playerMovement.moveSpeed *= 1.5f;
                Debug.Log("Güçlendirme Satýn alýndý!");
            }
            else
            {
                Debug.Log("Yetersiz Arcadium. Gereken miktar: " + (cost - PlayerData.arcadiumCount));
            }
        }
    }
   
}
