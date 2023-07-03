using UnityEngine;

public class RunManager : MonoBehaviour
{
    [Header("RUN DATA")]
    public static bool isOnRun;
    public delegate void isRunning();
    public static event isRunning OnRunning;
    public static event isRunning OnDamaged;
    public static event isRunning OnHealed;
    public static event isRunning OnShowHealth;

    //bu metodda baya if var, ama büyük çoðunluðu button çalýþmasý için...
    private void OnGUI()
    {
        if (isOnRun)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Caný Azalt"))
            {
                if (RunManager.OnRunning != null && !RunEnder.isDead)
                {
                    OnDamaged();
                }
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 50, 35, 100, 30), "Caný Arttýr"))
            {
                if (RunManager.OnRunning != null)
                {
                    OnHealed();
                }
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 50, 95, 100, 30), "Caný Göster"))
            {
                if (RunManager.OnRunning != null)
                {
                    OnShowHealth();
                }
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 50, 65, 100, 30), "Run'ý baþlat"))
            {
                if (RunManager.OnRunning != null)
                {
                    OnRunning();
                }
            }

        }

    }
       
}
