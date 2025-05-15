using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Utiles.Pool;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour, IPoolable
    {
        private AudioSource _audioSource;

        private AudioClip _currentAudioClip;
        
        private Coroutine _waitCoroutine;
        
        public event Action<AudioPlayer> OnReleased;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void OnDisable()
        {
            transform.position = Vector3.zero;
        }

        public void PlayEffect(AudioClip clip, Transform parent, float volume, float radius, bool isLooped)
        {
            _audioSource.transform.SetParent(parent);
            _audioSource.transform.localPosition = Vector3.zero;
            
            _audioSource.spatialize = true;
            _audioSource.spatialBlend = 1;
            
            _audioSource.clip = clip;
            _audioSource.volume = volume;
            _audioSource.loop = isLooped;
            
            _audioSource.minDistance = radius / 2;
            _audioSource.maxDistance = radius;
            
            _audioSource?.Play();
            
            if (!isLooped && _audioSource)
            {
                _waitCoroutine = StartCoroutine(WaitingCoroutine(clip.length));
            }
        }
        
        public void PlayEffect(AudioClip clip, float volume, bool isLooped)
        {
            _audioSource.spatialize = false;
            _audioSource.spatialBlend = 0;
            
            _audioSource.clip = clip;
            _audioSource.volume = volume;
            _audioSource.loop = isLooped;
            
            _audioSource?.Play();
            
            if (!isLooped && _audioSource)
            {
               _waitCoroutine = StartCoroutine(WaitingCoroutine(clip.length));
            }
        }

        public void StopSound()
        {
            if (_waitCoroutine != null)
            {
                StopCoroutine(_waitCoroutine);
            }
            
            OnReleased?.Invoke(this);
        }
        
        public void Release()
        {
            _audioSource.Stop();
            
            StopCoroutine(_waitCoroutine);
            _waitCoroutine = null;
            
            _audioSource.transform.localPosition = Vector3.zero;
            
            _audioSource.clip = null;
            _audioSource.loop = false;
            _audioSource.spatialize = false;
            _audioSource.spatialBlend = 0f;
        }

        private IEnumerator WaitingCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            
            OnReleased?.Invoke(this);
        }
    }
}