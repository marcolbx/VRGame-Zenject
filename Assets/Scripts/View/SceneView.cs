using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.View
{
    public enum VRInteraction
    {
        SurvivalMode,
        Quit,
    }
    public class SceneView : MonoBehaviour
    {
        [SerializeField] private VRInteraction _interactionType;

        public void OnPointerClick()
        {
            if (_interactionType == VRInteraction.SurvivalMode)
            {
                SceneManager.LoadScene(1);
            }
            else if (_interactionType == VRInteraction.Quit)
            {
                Application.Quit();
            }
        }
    }
}