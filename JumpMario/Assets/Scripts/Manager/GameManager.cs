using UnityEngine;
using Runningboy.Utility;
using Runningboy.Module;
using Sirenix.OdinInspector;

namespace Runningboy.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        GUIModule _guiModule;
        [SerializeField]
        IOModule _ioModule;
        [SerializeField]
        SceneModule _sceneModule;

        public GUIModule GUIModule { get { return _guiModule; } }
        public IOModule IOModule { get { return _ioModule; } }
        public SceneModule SceneModule { get { return _sceneModule; } }

        private void Reset()
        {
            _guiModule = GetComponent<GUIModule>();
            _ioModule = GetComponent<IOModule>();
            _sceneModule = GetComponent<SceneModule>();
        }
    }
}
