using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : UI_ToolTip
{
    private UI ui;
    private UI_SkillTree skillTree;

    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirements;

    [Space]
    [SerializeField] private string metConditionHex;
    [SerializeField] private string notMetConditionHex;
    [SerializeField] private string importantInfoHex;
    [SerializeField] private string lockedSkillText = "You've taken a different path - this skill is now locked.";

    private Coroutine textEffectCo;

    protected override void Awake()
    {
        base.Awake();
        ui = GetComponentInParent<UI>();
        skillTree = ui.GetComponentInChildren<UI_SkillTree>(true);
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

        string skillLockedText = GetColoredText(importantInfoHex, lockedSkillText);
        string requirements = node.isLocked ? skillLockedText : GetRequirements(node.skillData.cost, node.neededNodes, node.conflictNodes);

        skillRequirements.text = requirements;
    }
    public void LockedSkillEffect()
    {
        if (textEffectCo != null)
        {
            StopCoroutine(textEffectCo);
        }
        textEffectCo = StartCoroutine(TextBlinkEffectCo(skillRequirements, .15f, 3));
    }
    private IEnumerator TextBlinkEffectCo(TextMeshProUGUI text, float blinkInterval, int blinkCount)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            text.text = GetColoredText(notMetConditionHex, lockedSkillText);
            yield return new WaitForSeconds(blinkInterval);
            text.text = GetColoredText(importantInfoHex, lockedSkillText);
            yield return new WaitForSeconds(blinkInterval);
        }
    }
    private string GetRequirements(int skillCost, UI_TreeNode[] neededNodes, UI_TreeNode[] conflictNodes)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Requirements: ");

        string costColor = skillTree.EnoughSkillPoints(skillCost) ? metConditionHex : notMetConditionHex;
        string costText = $"- {skillCost} skill point(s)";
        string finalCostText = GetColoredText(costColor, costText);

        sb.AppendLine(finalCostText);
        foreach (var node in neededNodes)
        {
            string nodeColor = node.isUnlocked ? metConditionHex : notMetConditionHex;
            string nodeText = $"- {node.skillData.displayName}";
            string finalNodeText = GetColoredText(nodeColor, nodeText);

            sb.AppendLine(finalNodeText);
        }

        if (conflictNodes.Length <= 0)
        {
            return sb.ToString();
        }
        sb.AppendLine();
        sb.AppendLine(GetColoredText(importantInfoHex, "Locks out: "));
        foreach (var node in conflictNodes)
        {
            string nodeText = $"- {node.skillData.displayName}";
            string finalNodeText = GetColoredText(importantInfoHex, nodeText);

            sb.AppendLine(finalNodeText);
        }
        return sb.ToString();
    }

}
