using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [Range(0f, 10f)]
    public float maxStealth;
    [Range(0f, 5f)]
    public float stealthDecay;
    [Range(0f, 5f)]
    public float stealthSpeed;



    public static float maxStealthS;
    public static float stealthDecayS;
    public static float stealthSpeedS;
    private static float currentStealth = 0f;

    public Slider stealthBar;



    public float maxHealth;
    public float currentHealth;




    public static float maxHealthS;
    public static float currentHealthS;
    private static float currentHealthClampedS;

    public Slider healthBar;

    void Start()
    {
        maxStealthS = maxStealth;
        stealthDecayS = stealthDecay;
        stealthSpeedS = stealthSpeed;
        stealthBar.maxValue = maxStealth;

        currentHealthS = maxHealth;
        healthBar.maxValue = maxHealth;
    }

    void Update()
    {
        UpdateHealthSlider();
        currentHealth = currentHealthS;
        UpdateStealthSlider();
        currentStealth = Mathf.Clamp(currentStealth, 0f, maxStealth);
        if (!CheckIfAnyEnemySeesPlayer())
        {
            RemoveStealth();
        }
    }

    public static float GetStealth()
    {
        Debug.Log(currentStealth);
        return currentStealth;

    }

    public static void AddStealth()
    {
        currentStealth += stealthSpeedS * Time.deltaTime;
        Debug.Log("Add Stealth");
    }

    public static void RemoveStealth()
    {
        currentStealth -= stealthDecayS * Time.deltaTime;
    }

    public static float MaxOutStealth()
    {

        return maxStealthS;
    }

    private void UpdateStealthSlider()
    {
        stealthBar.value = currentStealth;
    }

    private bool CheckIfAnyEnemySeesPlayer()
    {
        // Check all guards
        GuardAI[] allGuards = FindObjectsByType<GuardAI>(FindObjectsSortMode.None);
        foreach (GuardAI guard in allGuards)
        {
            if (guard.PlayerBeingSeen()) return true;
        }

        // Check all drones (if you have them)
        DroneAI[] allDrones = FindObjectsByType<DroneAI>(FindObjectsSortMode.None);
        foreach (DroneAI drone in allDrones)
        {
            if (drone.PlayerBeingSeen()) return true;
        }

        return false;
    }


    public static void ClearStealth()
    {
        currentStealth = 0;
    }


    public static void PlayerTakeDamage(float Amount)
    {
        currentHealthS -= Amount;

    }

    private void UpdateHealthSlider()
    {
        currentHealthClampedS = Mathf.Clamp(currentHealth, 0f, maxHealth);
        healthBar.value = currentHealthClampedS;

    }
}