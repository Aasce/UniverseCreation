using Asce.Managers.UIs;
using TMPro;
using UnityEngine;

namespace Asce.Shared.UIs
{
    public class UICopyright : UIObject
    {
        [SerializeField] private TextMeshProUGUI _version;
        [SerializeField] private TextMeshProUGUI _company;

        public TextMeshProUGUI Version => _version;
        public TextMeshProUGUI Company => _company;


        private void Start()
        {
            this.UpdateVersion();
            this.UpdateCompany();
        }


        public void UpdateVersion()
        {
            if (Version == null) return;
            Version.text = $"version {Application.version}";
        }

        public void UpdateCompany()
        {
            if (Company == null) return;
            Company.text = $"By {Application.companyName}";
        }
    }
}
