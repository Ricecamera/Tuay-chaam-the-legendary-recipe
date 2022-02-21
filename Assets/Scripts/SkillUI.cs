using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


[RequireComponent(typeof(Image), typeof(Button))]
public class SkillUI : MonoBehaviour
{
    public const float SIZE_MULTIPLER = 1.12f;
    public delegate void OnClick();

    [SerializeField]
    private GameObject effect;

    [SerializeField]
    private TextMeshProUGUI cooldown;

    private Image myImage;
    private Button myButton;

    public bool Selected {get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
        Selected = false;
        effect.SetActive(false);
    }

    // Update is called once per frame

    public void SetSelect(bool value) {
        if (value) {
            transform.localScale = Vector3.one * SIZE_MULTIPLER;
            effect.SetActive(true);
        }
        else {
            transform.localScale = Vector3.one;
            effect.SetActive(false);
        }
        
        Selected = value;
    }

    public void SetSkill(Skill skill) {
        if (skill == null) {
            myImage.sprite = null;
            gameObject.SetActive(false);
            myButton.interactable = false;
        }
        else {
            myImage.sprite = skill.Icon;        
            gameObject.SetActive(true);
            bool show = skill.Cooldown == 0;
            myButton.interactable = show;
            cooldown.gameObject.SetActive(!show);
            cooldown.text = skill.Cooldown.ToString();
        }
    }

    public void AddListener(UnityAction callback) {
        myButton.onClick.AddListener(callback);
    }

}
