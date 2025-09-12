using Asce.Managers.UIs;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Asce.Game
{
    public class UIPlaytime : UIObject
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private float _delay = 1f;
        private Coroutine _updateCoroutine;

        public float Delay
        {
            get => _delay;
            set => _delay = value;
        }

        private void Start()
        {
            this.SetTime();
            if (_updateCoroutine != null) StopCoroutine(_updateCoroutine);
            _updateCoroutine = StartCoroutine(this.UpdateText());
        }

        private IEnumerator UpdateText()
        {
            while (true) 
            {
                yield return new WaitForSeconds(_delay);
                this.SetTime();
            }
        }

        public void SetTime ()
        {
            if (_timeText == null) return;
            if (PlaytimeManager.Instance == null) return;

            _timeText.text = PlaytimeManager.Instance.GetPlaytimeAsText();
        }
    }
}
