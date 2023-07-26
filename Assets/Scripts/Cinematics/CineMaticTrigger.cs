using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CineMaticTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false;
        void OnTriggerEnter(Collider other)
        {
            if (!alreadyTriggered && other.tag == "Player")
            {
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}


