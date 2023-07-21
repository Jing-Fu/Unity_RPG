using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CineMaticTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            bool alreadyTriggered = false;
            if (!alreadyTriggered && other.tag == "Player")
            {
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}


