using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
    [SerializeField] private int skillPoints;
    [SerializeField] private UI_TreeConnectHandler[] parentNodes;
    public Player_SkillManager skillManager { get; private set; }

    void Awake()
    {
        skillManager = FindAnyObjectByType<Player_SkillManager>();
    }
    void Start()
    {
        UpdateAllConnections();
    }
    [ContextMenu("Reset skill tree")]
    public void RefundAllSkills()
    {
        UI_TreeNode[] skillNodes = GetComponentsInChildren<UI_TreeNode>();

        foreach (var node in skillNodes)
        {
            node.Refund();
        }
    }

    public bool EnoughSkillPoints(int cost) => skillPoints >= cost;
    public void RemoveSkillPoints(int cost) => skillPoints -= cost;
    public void AddSkillPoints(int points) => skillPoints += points;

    [ContextMenu("UpdateAllConnections")]
    public void UpdateAllConnections()
    {
        foreach (var node in parentNodes)
        {
            node.UpdateAllConnections();
        }
    }
}
