using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reactional.Demo
{
    public class TrackLoadTrigger : MonoBehaviour
    {
        [SerializeField] string trackName;
        [SerializeField] string exitTrackName;
        [SerializeField] string colliderTag = "Player";
        [SerializeField] bool stopTrackOnExit = false;
        [SerializeField] float exitThreshold = 10f;
        [SerializeField] float fadeoutInBeats = 4f;
        [SerializeField] float _delayTrigger = 0;
        [SerializeField] bool playOnce = true;

        private Collider coll;


        private void Start()
        {
            coll = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(colliderTag))
            {
                StartCoroutine(StartSong());
                AlterColliderRadius(exitThreshold);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(colliderTag))
            {
                if (stopTrackOnExit)
                {
                    var selectedtrack = Reactional.Core.ReactionalManager.Instance.selectedTrack;
                    if (Reactional.Core.ReactionalManager.Instance._loadedTracks[selectedtrack].trackName == trackName)
                    {
                        StopSong(fadeoutInBeats);
                    }

                    if (exitTrackName != "")
                    {
                        StartCoroutine(StartExitSong());
                    }

                    AlterColliderRadius(-exitThreshold);
                }

                if (playOnce && exitTrackName == "")
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public IEnumerator StartSong()
        {
            yield return new WaitForSeconds(_delayTrigger);
            Reactional.Playback.Playlist.PlayTrack(trackName);
        }

        public void StopSong(float div)
        {
            Reactional.Playback.Playlist.Stop(div / 16f);
        }

        private IEnumerator StartExitSong()
        {
            yield return new WaitForSeconds(_delayTrigger);
            Reactional.Playback.Playlist.PlayTrack(exitTrackName);
            gameObject.SetActive(false);
        }

        private void AlterColliderRadius(float adjustment)
        {
            if (coll.GetType() == typeof(CapsuleCollider))
            {
                var colli = GetComponent<CapsuleCollider>();
                colli.radius += adjustment;
            }

            if (coll.GetType() == typeof(SphereCollider))
            {
                var colli = GetComponent<SphereCollider>();
                colli.radius += adjustment;
            }
        }
    }
}
