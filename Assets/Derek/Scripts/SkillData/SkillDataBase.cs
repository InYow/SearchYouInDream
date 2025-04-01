using System;
using UnityEngine;

[Serializable]
public class SkillDataPair
{
    public int skillID;
    public string skillStatePath;
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
}
