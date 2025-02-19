using UnityEngine;

public class FoodMechanics : MonoBehaviour
{
    public int foodValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.IncreaseHunger(foodValue);
                Debug.Log("Hunger Restored: " + foodValue);
            }
            Destroy(gameObject, 0.1f);
        }
    }
    
}
