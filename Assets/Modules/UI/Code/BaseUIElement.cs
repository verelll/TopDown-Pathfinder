using Sirenix.OdinInspector;

namespace verell.UI
{
    public abstract class BaseUIElement : SerializedMonoBehaviour, IUIElement
    {
        protected virtual void Init(){ }
        protected virtual void Dispose(){ }
        
#region IUIElement

        void IUIElement.Init() => Init();

        void IUIElement.Dispose() => Dispose();

#endregion
    }
}