using Asce.Managers;
using Asce.Managers.Utils;
using System;
using UnityEngine;

namespace Asce.Game
{
    public class PlaytimeManager : MonoBehaviourSingleton<PlaytimeManager>
    {
        [SerializeField] private float _playtime = 0f;
        [SerializeField] private bool _isRunning = false;

        public float Playtime
        {
            get => _playtime;
            set => _playtime = value;
        }

        public bool IsRunning => _isRunning;


        private void Update()
        {
            if (_isRunning)
            {
                _playtime += Time.deltaTime;
            }
        }

        public void StartTimer()
        {
            _isRunning = true;
            _playtime = 0f;
        }

        public void ResumeTimer()
        {
            _isRunning = true;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        public void ResetTimer()
        {
            _playtime = 0f;
        }


        public string GetPlaytimeAsText() => NumberUtils.FloatToTime(Playtime);
    }
}