using UnityEngine;
using TMPro;


public class HealthSystem : MonoBehaviour
{
    private int currentHp;
    private int maxHp;
    public HealthBar healthBar;

    public ParticleSystem healVfx;
    public ParticleSystem damagedVfx;

    [SerializeField]
    private TextMeshProUGUI healthNumber;

    public int CurrentHp
    {
        get
        {
            return currentHp;
        }
        set
        {
            if (0 <= value && value <= maxHp)
                currentHp = value;
        }
    }

    public int MaxHp
    {
        get
        {
            return maxHp;
        }
        set
        {
            if (0 <= value && value <= maxHp)
                maxHp = value;
        }
    }

    public bool IsAlive
    {
        get
        {
            return currentHp > 0;
        }
        set
        {
            if (value)
                currentHp = (int)Mathf.Round(maxHp * 0.5f);
            else
                currentHp = 0;
        }
    }

    public void Initialize(int maxHp)
    {
        this.maxHp = maxHp;
        this.currentHp = maxHp;
        healthBar.Reset();
        healthNumber.SetText(maxHp.ToString());
    }

    public void TakeDamage(int damage)
    {
        damagedVfx.Play();
        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
        }

        float fill = currentHp / (float)maxHp;
        healthBar.SetFill(fill);
        healthNumber.SetText(currentHp.ToString());

    }

    public void Heal(int healAmount)
    {
        healVfx.Play();
        currentHp += healAmount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        float fill = currentHp / (float)maxHp;
        healthBar.SetFill(fill);
        healthNumber.SetText(currentHp.ToString());
    }


    public void HideHpBar()
    {
        healthBar.gameObject.SetActive(false);
    }

    public void ShowHpBar()
    {
        healthBar.gameObject.SetActive(true);
    }

}
