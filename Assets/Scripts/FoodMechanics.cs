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

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Debug.Log("Hunger Restored: " + foodValue);
            Destroy(gameObject, 1);
        }
    }
}
