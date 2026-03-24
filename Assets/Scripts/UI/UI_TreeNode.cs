using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UI ui;
    private RectTransform rect;
    private UI_SkillTree skillTree;
    private UI_TreeConnectHandler connectHandler;

    [Header("Unlock details")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] conflictNodes;
    public bool isUnlocked;
    public bool isLocked;

    [Header("Skill details")]
    public Skill_DataSO skillData;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private int skillCost;
    private string lockedColorHex = "#AAA7A7";
    private Color lastColor;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();
        skillTree = GetComponentInParent<UI_SkillTree>();
        connectHandler = GetComponentInParent<UI_TreeConnectHandler>();

        UpdateIconColor(GetColorByHex(lockedColorHex));
    }
    public void Refund()
    {
        isUnlocked = false;
        isLocked = false;
        UpdateIconColor(GetColorByHex(lockedColorHex));

        skillTree.AddSkillPoints(skillData.cost);
        connectHandler.UnlockConnectionImage(false);
    }
    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        LockConflictNodes();

        skillTree.RemoveSkillPoints(skillData.cost);
        connectHandler.UnlockConnectionImage(true);
    }
    private bool CanBeUnlocked()
    {
        if (isLocked || isUnlocked) return false;

        if (!skillTree.EnoughSkillPoints(skillData.cost)) return false;

        foreach (var node in neededNodes)
        {
            if (!node.isUnlocked)
            {
                return false;
            }
        }
        foreach (var node in conflictNodes)
        {
            if (node.isUnlocked)
            {
                return false;
            }
        }

        return true;
    }
    private void LockConflictNodes()
    {
        foreach (var node in conflictNodes)
        {
            node.isLocked = true;
        }
    }
    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null) return;

        lastColor = skillIcon.color;
        skillIcon.color = color;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeUnlocked())
        {
            Unlock();
        }
        else if (isLocked)
        {
            ui.skillToolTip.LockedSkillEffect();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(true, rect, this);

        if (!isUnlocked || !isLocked)
        {
            ToggleNodeHighlight(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.ShowToolTip(false, rect);

        if (!isUnlocked || !isLocked)
        {
            ToggleNodeHighlight(false);
        }
    }
    private void ToggleNodeHighlight(bool highlight)
    {
        Color highlightColor = Color.white * .9f; highlightColor.a = 1;
        Color colorToApply = highlight ? highlightColor : lastColor;

        UpdateIconColor(colorToApply);
    }
    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);

        return color;
    }
    void OnDisable()
    {
        if (isLocked)
        {
            UpdateIconColor(GetColorByHex(lockedColorHex));
        }
        if (isUnlocked)
        {
            UpdateIconColor(Color.white);
        }
    }
    void OnValidate()
    {
        if (skillData == null) return;

        skillName = skillData.displayName;
        skillIcon.sprite = skillData.icon;
        skillCost = skillData.cost;
        gameObject.name = "UI - Tree Node " + skillData.displayName;
    }
}
