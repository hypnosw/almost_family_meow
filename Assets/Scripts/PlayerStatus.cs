using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Slider hungerSlider;
    public Slider energySlider;
    public int startingHunger = 100;
    public float hungerDecreaseRate = 1f;
    public int startingEnergy = 30;
    public float energyDecreaseRate = 5;
    public float energyIncreaseRate = 1f;
    public static bool isAlive{get; set;}
    private float currentHunger;

    [HideInInspector]
    public float currentEnergy;
    private FPSPlayerController playerController;
    LevelManager levelManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAlive = true;
        if (PlayerPrefs.HasKey("SavedHunger") && PlayerPrefs.HasKey("SavedEnergy"))
        {
            LoadStatus(); // restore saved state
        }
        else
        {
            currentHunger = startingHunger;
            currentEnergy = startingEnergy;
        }

        UpdateHungerSlider();
        UpdateEnergySlider();
        playerController = GetComponent<FPSPlayerController>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive){
            DecreaseHunger();

            if (playerController != null && FPSPlayerController.isRunning)
            {
                DecreaseEnergy();
            }
            else
            {
                IncreaseEnergy();
            }
        }
        if (currentHunger <= 0 && isAlive) 
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        transform.Rotate(0, 0, 90, Space.Self);
        levelManager.LevelLost();
        Debug.Log("You died!");
    }

    void DecreaseHunger()
    {
        float hungerToReduce = hungerDecreaseRate * Time.deltaTime;
        currentHunger -= hungerToReduce;
        currentHunger = Mathf.Clamp(currentHunger, 0, startingHunger);

        UpdateHungerSlider();
    }

    public void IncreaseHunger(int amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, startingHunger);

        UpdateHungerSlider();
    }

    void IncreaseEnergy()
    {
        float energyToIncrease = energyIncreaseRate * Time.deltaTime;
        currentEnergy += energyToIncrease;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, startingEnergy);

        UpdateEnergySlider();
    }

    void DecreaseEnergy()
    {
        float energyToDecrease = energyDecreaseRate * Time.deltaTime;
        currentEnergy -= energyToDecrease;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, startingEnergy);

        UpdateEnergySlider();
    }

    void UpdateHungerSlider()
    {
        if(hungerSlider != null){
            hungerSlider.value = currentHunger;
        }
    }

    void UpdateEnergySlider()
    {
        if(energySlider != null){
            energySlider.value = currentEnergy;
        }
    }

    public void SaveStatus()
    {
        PlayerPrefs.SetFloat("SavedHunger", currentHunger);
        PlayerPrefs.SetFloat("SavedEnergy", currentEnergy);
        PlayerPrefs.Save();
        Debug.Log("Player status saved!");
    }

    public void LoadStatus()
    {
        if (PlayerPrefs.HasKey("SavedHunger"))
        {
            currentHunger = PlayerPrefs.GetFloat("SavedHunger");
            UpdateHungerSlider();
        }

        if (PlayerPrefs.HasKey("SavedEnergy"))
        {
            currentEnergy = PlayerPrefs.GetFloat("SavedEnergy");
            UpdateEnergySlider();
        }

        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            Vector3 savedPos = new Vector3(
                PlayerPrefs.GetFloat("PlayerX"),
                PlayerPrefs.GetFloat("PlayerY"),
                PlayerPrefs.GetFloat("PlayerZ")
            );

            transform.position = savedPos;
        }
    }

}
