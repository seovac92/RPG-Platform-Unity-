using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillToolTip skillToolTip;

    void Awake()
    {
        skillToolTip = GetComponentInChildren<UI_SkillToolTip>();
    }
}
