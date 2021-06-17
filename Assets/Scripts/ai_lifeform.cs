using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_lifeform : MonoBehaviour
{
    public float speed;
    public float accuracy;
    public float radius;
    public GameObject goal;

    public GameObject[] meals;

    GameObject rememberFood()
    {
        for(int i = 0; i < meals.Length; i++)
        {
            
        }
    }

    void moveToFood(Vector3 direction, float distance)
    {
        if(distance > accuracy)
        {
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);
        }else if(distance == accuracy)
        {
            Destroy(goal);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = goal.transform.position - this.transform.position;
        float distance = direction.magnitude;
        
        if(distance < radius)
        {
            moveToFood(direction, distance);
        }
    }
}
