using System.Text;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : UI_ToolTip
{
    private UI_SkillTree skillTree;

    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirements;

    [Space]
    [SerializeField] private string metConditionHex;
    [SerializeField] private string notMetConditionHex;
    [SerializeField] private string importantInfoHex;
    [SerializeField] private string lockedSkillText = "You've taken a different path - this skill is now locked.";

    protected override void Awake()
    {
        base.Awake();

        skillTree = GetComponentInParent<UI_SkillTree>();
    }
    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);
    }
    public void ShowToolTip(bool show, RectTransform targetRect, UI_TreeNode node)
    {
        base.ShowToolTip(show, targetRect);

        if (!show) return;

        skillName.text = node.skillData.displayName;
        skillDescription.text = node.skillData.description;

        string skillLockedText = $"<color={importantInfoHex}>{lockedSkillText}</color>";
        string requirements = node.isLocked ? skillLockedText : GetRequirements(node.skillData.cost, node.neededNodes, node.conflictNodes);

        skillRequirements.text = requirements;
    }
    private string GetRequirements(int skillCost, UI_TreeNode[] neededNodes, UI_TreeNode[] conflictNodes)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Requirements: ");

        string costColor = skillTree.EnoughSkillPoints(skillCost) ? metConditionHex : notMetConditionHex;

        sb.AppendLine($"<color={costColor}>- {skillCost} skill point(s) </color>");
        foreach (var node in neededNodes)
        {
            string nodeColor = node.isUnlocked ? metConditionHex : notMetConditionHex;
            sb.AppendLine($"<color={nodeColor}>- {node.skillData.displayName}</color>");
        }

        if (conflictNodes.Length <= 0)
        {
            return sb.ToString();
        }
        sb.AppendLine();
        sb.AppendLine($"<color={importantInfoHex}>- Locks out:</color>");
        foreach (var node in conflictNodes)
        {
            sb.AppendLine($"<color={importantInfoHex}>- {node.skillData.displayName}</color>");
        }
        return sb.ToString();
    }
}
