using UnityEngine;
using Reactional;

namespace Reactional.Demo
{
    public class ReactionalSizePulse : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.Linear(0.0f, 2.0f, 1.0f, 1.0f);
        [SerializeField] private float _prelayAnimationBy = 0.01f;
        [SerializeField] private bool pulseParent = false;
        [SerializeField] private float beatMultiplier = 1f;
        [SerializeField] private float CullDistance = 60f;

        private float dist;
        private Vector3 _originalScale;
        private float lastBeatPos;
        private GameObject player;

        private void OnEnable()
        {
            _originalScale = pulseParent ? transform.parent.localScale : transform.localScale;
            player = Camera.main.gameObject;
        }

        private void Update()
        {
            dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist > CullDistance)
                return;

            var currBeat = Reactional.Playback.MusicSystem.GetCurrentBeat() * beatMultiplier;
            if (currBeat < lastBeatPos)
            {
                if (currBeat <= 0)
                {
                    lastBeatPos = 0; // if song changes and we're at the start, reset
                    return;
                }
                return;
            }

            float animationValue = _animationCurve.Evaluate((currBeat + _prelayAnimationBy) % 1f);
            float scaleFactor = 1 + ((animationValue - 1f) * Mathf.InverseLerp(CullDistance, 0, dist));

            if (pulseParent)
                transform.parent.localScale = _originalScale * scaleFactor;
            else
                transform.localScale = _originalScale * scaleFactor;

            lastBeatPos = currBeat % 1;
        }
    }
}