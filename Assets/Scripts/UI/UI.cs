using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillToolTip skillToolTip;
    public UI_SkillTree skillTree;
    private bool skillTreeEnabled;

    void Awake()
    {
        skillToolTip = GetComponentInChildren<UI_SkillToolTip>();
        skillTree = GetComponentInChildren<UI_SkillTree>(true);
    }
    public void ToggleSkillTreeUI()
    {
        skillTreeEnabled = !skillTreeEnabled;
        skillTree.gameObject.SetActive(skillTreeEnabled);
        skillToolTip.ShowToolTip(false, null);
    }
}
