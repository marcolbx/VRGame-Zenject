using UnityEngine.SceneManagement;

namespace Base.Controller
{
    public enum Scenes
    {
        Menu,
        Tutorial,
        StoryMode,
        Survival
    }
    public class SceneTransitionHandler
    {
        public void LoadMenuScene()
        {
            LoadScene(Scenes.Menu, LoadSceneMode.Single);
        }

        void LoadScene(Scenes scenes, LoadSceneMode mode)
        {
            SceneManager.LoadScene(scenes.ToString(), mode);
        }
    }
}