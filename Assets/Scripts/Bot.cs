using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    void Start()
    {
        GetComponent<NavMeshAgent>().destination=GameObject.Find("Player").transform.position;
    }


 
  
        
 
}
