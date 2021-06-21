using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController_script : MonoBehaviour
{
    public GameObject food;
    public Vector3 min;
    public Vector3 max;

    private float xAxis;
    private float yAxis;
    private Vector3 randomPosition;

    void SpawnFood()
    {
        for(int i = 0; i < 100; i++)
        {
            xAxis = UnityEngine.Random.Range(min.x, max.x);
            yAxis = UnityEngine.Random.Range(min.y, max.y);
            randomPosition = new Vector3(xAxis, yAxis, 0);

            Instantiate(food, randomPosition , Quaternion.identity);
        }       
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnFood", 0.0f, 60.0f);
    }
}
