using UnityEngine.SceneManagement;

namespace Base.Controller
{
    public class SceneTransitionHandler
    {
        public void LoadMenuScene()
        {
            LoadScene(0);
        }

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}