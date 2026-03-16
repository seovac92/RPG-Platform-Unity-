using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
    public int skillPoints;

    public bool EnoughSkillPoints(int cost) => skillPoints >= cost;
    public void RemoveSkillPoints(int cost) => skillPoints -= cost;
}
