using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reactional.Demo
{
    public class SetControlTrigger : MonoBehaviour
    {
        public string ControlName;
        public float value;

        private void OnTriggerEnter(Collider other)
        {
            Reactional.Playback.Theme.SetControl(ControlName, value);
        }
    }
}
