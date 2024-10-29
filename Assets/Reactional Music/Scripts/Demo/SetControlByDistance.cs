using UnityEngine;

namespace Reactional.Demo
{
    public class SetControlByDistance : MonoBehaviour
    {
        public Transform object1;
        public Transform object2;
        public string ControlName;
        public bool active = true;

        [SerializeField] [Range(0, 200)] float minVal;
        [SerializeField] [Range(0, 200)] float maxVal;

        [Range(0, 1)] public float normalizedValue;

        void OnEnable()
        {
            active = true;
            InvokeRepeating("UpdateDistance", 1, 0.05f);
        }

        public void SetActive(bool value)
        {
            active = value;
        }

        void UpdateDistance()
        {
            if (active)
            {
                var distance = Vector3.Distance(object1.position, object2.position);
                normalizedValue = Mathf.InverseLerp(minVal, maxVal, distance);

                Reactional.Playback.Theme.SetControl(ControlName, normalizedValue);
            }

        }

        private void OnDisable()
        {
            active = false;
            CancelInvoke("UpdateDistance");
        }
    }
}
