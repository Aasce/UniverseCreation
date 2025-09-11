using Asce.Managers.UIs;
using Asce.Managers.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Asce.Shared.UIs
{
    [RequireComponent(typeof(Button))]
    public abstract class AnimationButton : UIObject
    {
        [SerializeField] protected Button _button;

        protected override void RefReset()
        {
            base.RefReset();
            this.LoadComponent(out _button);
        }
    }
}
