using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] HealthSO health;
    Image healthBarImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBarImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = health.currentHealth / health.maxHealth;
    }
}
