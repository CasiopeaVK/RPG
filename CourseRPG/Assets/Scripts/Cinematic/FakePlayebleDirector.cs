using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace RPG.Cinematic
{
    public class FakePlayebleDirector : MonoBehaviour
    {
        public event Action<float> onFinish;
        void Start()
        {
            Invoke("OnFinish", 3f);
        }

        void OnFinish()
        {
            onFinish(4f);
        }
    }
}
