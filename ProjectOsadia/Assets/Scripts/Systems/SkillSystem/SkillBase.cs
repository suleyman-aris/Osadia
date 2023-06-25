using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase 
{
    public event EventHandler OnSkillPointsChanged;
    public event EventHandler<OnSkillUnlockedEventArgs> OnSkillUnlocked;
    public class OnSkillUnlockedEventArgs : EventArgs
    {
        public SkillType skillType;
    }
    //bu enumdaki "SkillA_1,2" ve "SkillB_1,2" uygun isimlerle deðiþtirilebilir\\
    //veya SkillC vs eklenebilir ama eðer index kullanacaksanýz sýraya dikkat edin\\
    public enum SkillType
    {
        None,
        SkillA_1,
        SkillA_2,
        SkillB_1,
        SkillB_2,
        Movement_1,
        Movement_2,
        Movement_3,
        HealthMax_1,
        HealthMax_2,
        HealthMax_3,
        Speed_1,
        Speed_2,
        Speed_3,
    };

    private List<SkillType> unlockedSkillsList;
    private int skillPoints;

    
    public SkillBase()
    { 
        unlockedSkillsList = new List<SkillType>();
    }

    public void AddSkillPoint()
    {
        skillPoints++;
        OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetSkillPoints()
    {
        return skillPoints;
    }

    public SkillType GetSkillRequirement(SkillType skillType)
    {
        switch (skillType)
        {
            //Canýn skill tree'deki requirement'larý
            case SkillType.HealthMax_2: return SkillType.HealthMax_1;
            case SkillType.HealthMax_3: return SkillType.HealthMax_2;
            //Hýzýn " " " " "
            case SkillType.Speed_2: return SkillType.Speed_1;
            case SkillType.Speed_3: return SkillType.Speed_3;
            //Aynýsý SkillA ve SkillB için.
            case SkillType.SkillA_2: return SkillType.SkillA_1;
            case SkillType.SkillB_2: return SkillType.SkillB_1;

        }
        return SkillType.None;
    }

    public bool CanUnlock(SkillType skillType)
    {
        SkillType skillRequirement = GetSkillRequirement(skillType);

        if (skillRequirement != SkillType.None) //Yetenek aðacýnda açýk olmasý gereken önceki bir skill varsa, o skill açýk mý deðil mi kontrol ediyor
        {
            if (IsSkillUnlocked(skillRequirement)) // Bahsi geçen skill açýk ise true döndürüyor deðilse false...
            {
                return true;
            }
            else return false;
        }
        else return true; //Açýk olmasý gereken bir önceki skill yoksa, true döndürüyor.
    }

    public void UnlockSkill(SkillType skillType)
    {
        if(!unlockedSkillsList.Contains(skillType))
         unlockedSkillsList.Add(skillType);
        OnSkillUnlocked?.Invoke(this, new OnSkillUnlockedEventArgs { skillType = skillType });
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillsList.Contains(skillType);
    }

    public bool TryUnlockSkill(SkillType skillType) // bir skill açmayý deniyor, açabiliyorsa açýyor ve true döndürüyor yoksa false "
    {
        if (CanUnlock(skillType)) // skill açýlabiliyor mu
        {
            if (skillPoints > 0) // skill puaný var mý
            {
                skillPoints--;
                OnSkillPointsChanged?.Invoke(this, EventArgs.Empty);
                UnlockSkill(skillType);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

}

