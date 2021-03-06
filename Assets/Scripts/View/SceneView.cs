using Base.Handler;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Base.View
{
    public enum VRInteraction
    {
        SurvivalMode,
        Quit,
        Stats,
        Store,
        Menu,
    }
    public class SceneView : MonoBehaviour
    {
        [SerializeField] private AudioSource _playSound;
        [SerializeField] private Animator _fadeAnimator;
        [SerializeField] private VRInteraction _interactionType;
        private SceneHandler _sceneHandler;

        [Inject]
        public void Init(SceneHandler sceneHandler)
        {
            _sceneHandler = sceneHandler;
        }

        public void OnPointerClick()
        {
            if (_playSound != null)
                _playSound.Play();

            if (_fadeAnimator != null)
                _fadeAnimator?.SetTrigger("ChangeScene");

            if (_interactionType == VRInteraction.SurvivalMode)
            {
                _sceneHandler.LoadSurvival();
            }
            else if (_interactionType == VRInteraction.Quit)
            {
                Application.Quit();
            }
            else if (_interactionType == VRInteraction.Stats)
            {
                SceneManager.LoadScene("StatsScene");
            }
            else if (_interactionType == VRInteraction.Store)
            {
                SceneManager.LoadScene("StoreScene");
            }
            else if (_interactionType == VRInteraction.Menu)
            {
                _sceneHandler.ReturnToMain();
            }
        }
    }
}