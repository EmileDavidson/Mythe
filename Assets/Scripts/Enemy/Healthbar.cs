using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Health))]
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("No Health script was found!");
            return;
        }
        SetMaxHealth(health.maxHealth);
        // _health.RemoveHealthEvent.AddListener(SetMaxHealth(_health.health));
        health.OnRemoveHealth.AddListener((int value)=>
        {
            //we dont want to get value what equals damage value. but we want the new health.
            SetHealth(health.health);
        });
    }

    private void SetMaxHealth(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public  void SetHealth(int value)
    {
        slider.value = value;
    }
}
