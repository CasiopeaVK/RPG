﻿using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        // IEnumerator Start()
        // {   
        //     Fader fader = FindObjectOfType<Fader>();
        //     fader.FadeOutImmediate();
        //     yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
        //     yield return fader.FadeIn(0.5f);
        // }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}
