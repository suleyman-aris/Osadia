using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skillere olan olaylarý yöneten class bu : 
public class SkillManager : MonoBehaviour
{
    public SkillBase Skills;
    public HealthSystemComponent hSysComponent;
    private HealthSystem healthSystem;
    //Skillere olan olaylarý yöneten class bu

    private void Awake()
    {
        Skills = new SkillBase();
        Skills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        hSysComponent = GetComponent<HealthSystemComponent>();
        healthSystem = hSysComponent.GetHealthSystem();
    }

    //burdaki logic skillere göre düzenlenebilir.
    private void PlayerSkills_OnSkillUnlocked(object sender, SkillBase.OnSkillUnlockedEventArgs e)
    {
        switch (e.skillType)
        {
            case SkillBase.SkillType.Speed_1:
                SetMovementSpeed(65f);
                break;
            case SkillBase.SkillType.Speed_2:
                SetMovementSpeed(80f);
                break;
            case SkillBase.SkillType.HealthMax_1:
                SetHealthAmountMax(12);
                break;
            case SkillBase.SkillType.HealthMax_2:
                SetHealthAmountMax(15);
                break;
        }
    }

    private void SetHealthAmountMax(int healthAmount)
    {
        //Caný ayarla   
        healthSystem.Heal(healthAmount);
    }

    private void SetMovementSpeed(float movementSpeed)
    {
        //Hýzý ayarla
        //Hareket kodu tamamlanýnca burayý düzenleyebiliriz.
    }
}
