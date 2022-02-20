using UnityEngine;

public class ChaamRender : MonoBehaviour
{
    public Chaam chaam;
    public HealthSystem healthSystem { get; private set; }

    protected virtual void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.Initialize(chaam.MaxHp);
    }

    protected virtual void Update()
    {

    }
}
