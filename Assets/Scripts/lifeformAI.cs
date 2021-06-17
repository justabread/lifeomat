using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeformAI : MonoBehaviour
{
    public float speed;
    public float accuracy;
    public float radius;

    private bool isMovingToFood;
    private bool isSeekingFood;
    private List<Food> memory;

    //Memory utilities
    void rememberFood(Food food)
    {
        memory.Add(food);
    }

    void forgetFood(Food food)
    {
        memory.Remove(food);
    }

    Food getMostRecentFood()
    {
        return memory[0];
    }

    //Function to seek food and determine if a food source is in distance
    void seekFood()
    {
        GameObject[] allFoods = GameObject.FindGameObjectsWithTag("Food");

        for(int i = 0; i < allFoods.Length; i++)
        {
            Vector3 direction = allFoods[i].transform.position - this.transform.position;
            float distance = direction.magnitude;

            if(distance < radius)
            {
                Food targetFood = new Food(allFoods[i], direction, distance);
                moveToFood(targetFood);
            }
        }
    }

    //Function to move lifeform to target food source
    void moveToFood(Food targetFood)
    {        
        if(targetFood.distance > accuracy)
        {            
            this.transform.Translate(targetFood.direction.normalized * speed * Time.deltaTime);
        }else if(targetFood.distance == accuracy)
        {
            Destroy(targetFood.foodObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        seekFood();
    }

    //Food subclass
    public class Food {
        public GameObject foodObject;
        public Vector3 direction;
        public float distance;
        public int nutrition = 100;

        public Food(GameObject _foodObject, Vector3 _direction, float _distance)
        {
            foodObject = _foodObject;
            direction = _direction;
            distance = _distance;
        }
    }
}


