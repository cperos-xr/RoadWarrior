using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointContainer : MonoBehaviour
{
    public List <Transform> wayPoints = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform child in transform)
        {
            wayPoints.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
