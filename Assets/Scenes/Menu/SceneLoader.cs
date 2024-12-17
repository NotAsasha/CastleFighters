using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scenes.Menu
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoad;

        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneToLoad);
        }

    }
}
