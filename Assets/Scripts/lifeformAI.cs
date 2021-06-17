using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeformAI : MonoBehaviour
{
    public float speed;
    public float accuracy;
    public float radius;
    public GameObject food;
    public int foodCount;
    public Vector3 min;
    public Vector3 max;
    
    private float xAxis;
    private float yAxis;
    private Vector3 randomPosition;

    private int score = 0;


    private bool isMovingToFood;
    private bool isSeeking;
    private Coroutine randomCoroutine = null;
    private Vector3 randomDir;
    private Food targetFood;
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

    IEnumerator getRandomDirection()
    {
        while(true){
            randomDir = Random.insideUnitCircle.normalized;
            //Debug.Log(randomDir);
            yield return new WaitForSeconds(.5f);   
        }        
    }

    void spawnFood()
    {
        for(int i = 0; i < foodCount; i++)
        {
            xAxis = UnityEngine.Random.Range(min.x, max.x);
            yAxis = UnityEngine.Random.Range(min.y, max.y);
            randomPosition = new Vector3(xAxis, yAxis, 0);

            Instantiate(food, randomPosition , Quaternion.identity);
        }
    }

    //Function to seek food and determine if a food source is in distance
    void seekFood()
    {
        GameObject[] allFoods = GameObject.FindGameObjectsWithTag("Food");

        if(isMovingToFood)
        {
            if(targetFood != null)
            {
                targetFood.direction = targetFood.foodObject.transform.position - this.transform.position;
                targetFood.distance = targetFood.direction.magnitude;

                moveToFood(targetFood);
            }else{
                seekFood();
            }
        }else{
            for(int i = 0; i < allFoods.Length; i++)
            {
                Vector3 direction = allFoods[i].transform.position - this.transform.position;
                float distance = direction.magnitude;

                if(distance < radius)
                {
                    isSeeking = false;
                    if(!isMovingToFood)
                    {
                        StopCoroutine(randomCoroutine);
                    }                    
                    isMovingToFood = true;
                    targetFood = new Food(allFoods[i], direction, distance);
                    moveToFood(targetFood);               
                }else
                {
                    if(!isSeeking)
                    {
                        randomCoroutine = StartCoroutine(getRandomDirection());
                        isSeeking = true;
                    }
                    moveToRandom();
                }
            }       
        }                       
    }

    //Function to move lifeform to target food source
    void moveToFood(Food targetFood)
    {       
        //Debug.DrawRay(this.transform.position, targetFood.direction, Color.red); 
        if(targetFood.distance > accuracy)
        {            
            this.transform.Translate(targetFood.direction.normalized * speed * Time.deltaTime);
        }else
        {
            isMovingToFood = false;
            score += targetFood.nutrition;
            Destroy(targetFood.foodObject);
        }
    }

    void moveToRandom()
    {               
        this.transform.Translate(randomDir * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        memory = new List<Food>();
        isMovingToFood = false;

        spawnFood();
        randomCoroutine = StartCoroutine(getRandomDirection());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(score >= 200)
        {
            score = 0;
            Instantiate(gameObject);
            //spawnFood();
        }
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


