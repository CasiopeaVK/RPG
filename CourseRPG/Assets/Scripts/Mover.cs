using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;
    float maxDistance = 1000f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveToCursor();    
        }
    }

    private void MoveToCursor()
    {   
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        bool hasHit = Physics.Raycast(ray, out raycastHit);
        if(hasHit)
        {
            GetComponent<NavMeshAgent>().destination = raycastHit.point;
        }
    }
}
