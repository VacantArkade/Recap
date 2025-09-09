using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class HealthSO : ScriptableObject
{
    public float currentHealth;
    public float maxHealth;

    public void takeDamage()
    {
        currentHealth -= 1;
    }

    public void restoreHealth()
    {
        currentHealth = maxHealth;
    }
}
