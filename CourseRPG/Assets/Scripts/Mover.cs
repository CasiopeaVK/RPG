﻿using System.Collections;
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
        if(Input.GetMouseButton(0))
        {
            MoveToCursor();    
        }
        UpdateAnimator();
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

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
