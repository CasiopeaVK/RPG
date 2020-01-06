using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Controll;
using UnityEngine;

namespace RPG.Cinematic
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            player = GameObject.FindGameObjectWithTag("Player");
        }
        public void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<Scheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        public void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

    }
}
