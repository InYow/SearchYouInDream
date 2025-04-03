using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SkillDataPair
{
    public int skillID;
    public bool isAvailable;
    public string skillStatePath;
    public Sprite skillIcon;
}

public class SkillDataBase : MonoBehaviour
{
    private static SkillDataBase m_Instance;
    public static SkillDataBase instance => m_Instance;

    [SerializeField]
    public SkillDataPair[] skillDataBase;
    
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        m_Instance = this;
    }

    public SkillDataPair[] GetAllSkills()
    {
        return skillDataBase;
    }

    public List<int> GetAllAvailableSkills()
    {
        var allSkills = skillDataBase.ToList();
        List<int> availableSkillsId = new List<int>();
        foreach (var skill in skillDataBase)
        {
            if (skill.isAvailable)
            {
                availableSkillsId.Add(skill.skillID);
            }
        }
        return availableSkillsId;
    }
}
