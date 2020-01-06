using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematic
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if(!isTriggered && other.transform.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                isTriggered = true;
            }
            
        }
    }
}
