using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(Button))]
public class SkillUI : MonoBehaviour
{
    public const float SIZE_MULTIPLER = 1.12f;
    public delegate void OnClick();

    [SerializeField]
    private GameObject effect;

    [SerializeField]
    private Sprite defaultSprite;     // a image to show when the skill doesn't have its image
    

    private SpriteRenderer myRenderer;
    private Button myButton;

    public bool Selected {get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
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

    public void SetSkillImage(Sprite image) {
        if (image == null)
            myRenderer.sprite = defaultSprite;
        else
            myRenderer.sprite = image;        
    }

    public void AddListener(UnityAction callback) {
        myButton.onClick.AddListener(callback);
    }

}
