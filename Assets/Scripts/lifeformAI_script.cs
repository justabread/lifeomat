using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeformAI_script : MonoBehaviour
{
    public float speed;
    public float accuracy;
    public GameObject food;
    public int hunger;

    //private variables
    private CircleCollider2D playerSight;

    private Vector3 randomDir;

    private int healthAtMitosis;
    private int health = 100;

    private bool isMovingToFood = false;

    private Coroutine updateHealthCoroutine = null;

    private Food targetFood;
    private List<Food> memory = new List<Food>();

    //Memory utilities 
    /*void RememberFood(Food food)
    {   
        memory.Add(food);
    }

    void ForgetFood(Food food)
    {
        memory.Remove(food);
    }

    Food GetMostRecentFood()
    {
        return memory[0];
    }*/

    void GetRandomDirection()
    {
        var x = Random.Range(-1f, 1f);
        var y = Random.Range(-1f, 1f);
        randomDir = new Vector3(x, y, 0f);
    }  

    IEnumerator UpdateHealth() 
    {
        while(true)
        {
            if(health > 0)
            {
                health -= hunger;
            }else{
                StopCoroutine(updateHealthCoroutine);
                Destroy(gameObject);
                yield break;
            }

            yield return new WaitForSeconds(2f);
        }        
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Food")
        {
            if(!isMovingToFood)
            {
                targetFood = new Food(other.gameObject);
                isMovingToFood = true;
            }            
        }
    }

    //Function to move lifeform to target food source
    void MoveToFood(Food targetFood)
    {
        if(targetFood.foodObject == null)
        {
            isMovingToFood = false;
            return;
        }

        Vector3 direction = targetFood.foodObject.transform.position - this.transform.position;
        float distance = direction.magnitude;

        if(distance > accuracy)
        {            
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);
        }else
        {
            isMovingToFood = false;
            health += targetFood.nutrition;
            Destroy(targetFood.foodObject);
        }
        //Debug.DrawRay(this.transform.position, targetFood.direction, Color.red); 
        
    }

    void MoveToRandom()
    {             
        this.transform.Translate(randomDir.normalized * speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        healthAtMitosis = health;
        playerSight = gameObject.GetComponent<CircleCollider2D>();
        InvokeRepeating("GetRandomDirection", 0.0f, 1.0f);
        updateHealthCoroutine = StartCoroutine(UpdateHealth());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(health >= healthAtMitosis + 100)
        {
            healthAtMitosis = health;
            Instantiate(gameObject);
            //spawnFood();
        }
        //SeekFood();
        if(isMovingToFood)
        {
            MoveToFood(targetFood);
        }else{
            MoveToRandom();
        }
    }

    //Food subclass
    public class Food {
        public GameObject foodObject;
        public int nutrition = 40;

        public Food(GameObject _foodObject)
        {
            foodObject = _foodObject;
        }       
    }
}


