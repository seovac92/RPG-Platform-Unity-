using TMPro;
using UnityEngine;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirements;

    public override void ShowToolTip(bool show, RectTransform targetRect)
    {
        base.ShowToolTip(show, targetRect);
    }
    public void ShowToolTip(bool show, RectTransform targetRect, Skill_DataSO skillData)
    {
        base.ShowToolTip(show, targetRect);

        if (!show) return;

        skillName.text = skillData.displayName;
        skillDescription.text = skillData.description;
        skillRequirements.text = "Requirements: \n" + skillData.cost;
    }
}
