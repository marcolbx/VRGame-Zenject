using Base.Handler;
using Base.Controller;
using UnityEngine;
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
        GunMode,
    }

    public class SceneView : MonoBehaviour
    {
        [SerializeField] private AudioSource _playSound;
        [SerializeField] private Animator _fadeAnimator;
        [SerializeField] private VRInteraction _interactionType;
        private SceneHandler _sceneHandler;
        private ControlsController _controlsController;

        [Inject]
        public void Init(SceneHandler sceneHandler, ControlsController controlsController)
        {
            _sceneHandler = sceneHandler;
            _controlsController = controlsController;
        }

        public void OnPointerClick()
        {
            if (_controlsController.CurrentControl == 0)
                return;
            HandleInteractions();
        }

        // public void OnPointerEnter()
        // {
        //     if (_controlsController.CurrentControl == 1)
        //         return;
        //    // HandleInteractions();
        // }

        public void HandleInteractions()
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
                _sceneHandler.LoadStats();
            }
            else if (_interactionType == VRInteraction.Store)
            {
                _sceneHandler.LoadStore();
            }
            else if (_interactionType == VRInteraction.Menu)
            {
                _sceneHandler.ReturnToMain();
            }
            else if (_interactionType == VRInteraction.GunMode)
            {
                _sceneHandler.LoadGunMode();
            }
        }
    }
}