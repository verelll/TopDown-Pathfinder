using Cysharp.Threading.Tasks;
using UnityEngine;
using verell.Architecture;

namespace Pathfinder.ScreenLoading
{
    public sealed class LoadingScreenService : SharedObject
    {
        internal LoadingScreen Screen
        {
            get
            {
                if (_screen == null)
                    _screen = Object.FindObjectOfType<LoadingScreen>();

                return _screen;
            }
        }

        private LoadingScreen _screen;

        public async UniTask ShowScreenAsync()
        {
            await Screen.ShowScreenAsync();
        }

        public async UniTask HideScreenAsync() 
        {
            await Screen.HideScreenAsync();
        }
    }
}