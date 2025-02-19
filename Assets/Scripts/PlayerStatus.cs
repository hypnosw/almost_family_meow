using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Slider hungerSlider;
    public Slider energySlider;
    public int startingHunger = 100;
    public float hungerDecreaseRate = 0.5f;
    public int startingEnergy = 30;
    public float energyDecreaseRate = 1;
    public float energyIncreaseRate = 0.5f;
    public static bool isAlive{get; private set;}
    private float currentHunger;
    
    [HideInInspector]
    public float currentEnergy;
    private FPSPlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAlive = true;
        currentHunger = startingHunger;
        UpdateHungerSlider();
        currentEnergy = startingEnergy;
        UpdateEnergySlider();
        playerController = GetComponent<FPSPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive){
            DecreaseHunger();

            if (playerController != null && playerController.isRunning)
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

    void Die()
    {
        isAlive = false;
        transform.Rotate(0, 0, 90, Space.Self);
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
}
