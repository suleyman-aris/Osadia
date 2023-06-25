using System;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    [Header("RUN DATA")]
    public static bool isOnRun;
    public static event EventHandler OnDamaged;
    public static event EventHandler OnHealed;
    public static event EventHandler OnRunning;
    public static event EventHandler OnShowHealth;
    RunStarter runStarter;


    //bu metot ekrandaki tuþlarýn bir kýsmýndan sorumlu, run, can götürme gibi.
    //Silinebilir ama eventler düzenlenmeden silinmesini tavsiye etmem.
    private void OnGUI()
    {
        if (isOnRun)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, 65, 100, 30), "Caný Azalt"))
            {
                if (!RunEnder.isDead)
                {
                    OnDamaged?.Invoke(this, EventArgs.Empty);
                }
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 50, 35, 100, 30), "Caný Arttýr"))
            {
                if (!RunEnder.isDead)
                {
                    OnHealed?.Invoke(this, EventArgs.Empty); 
                }
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 50, 95, 100, 30), "Caný Göster"))
            {
                OnShowHealth?.Invoke(this, EventArgs.Empty);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Run'ý baþlat"))
            {
                OnRunning?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private void Start()
    {
        
    }

    public void HandleRun()
    {

    }
}
