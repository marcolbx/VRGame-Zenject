using Base.Controller;
using Base.View;
using UnityEngine;
using Zenject;

public class GazeInteraction : MonoBehaviour
{
    [SerializeField] private SceneView _sceneView;
    [SerializeField] private float _gazeTime = 3f;
    private float _timer;
    private bool _gazedAt;
    private ControlsController _controlsController;

    [Inject]
    public void Init(ControlsController controlsController)
    {
        _controlsController = controlsController;
    }

    void Update()
    {
        if (_controlsController.CurrentControl == 1)
            return;

        if (_gazedAt)
        {
            if (_gazeTime > _timer)
                GazeSelectionHandler.Instance.UpdateCountdown(_gazeTime - _timer);

            _timer += Time.deltaTime;

            if (_timer >= _gazeTime)
            {
                _sceneView.HandleInteractions();
                GazeSelectionHandler.Instance.DisableSelectionHandler();
                _timer = 0;
            }
        }
    }

    public void OnPointerEnter()
    {
        if (_controlsController.CurrentControl == 1)
            return;

        _gazedAt = true;
        GazeSelectionHandler.Instance.EnableSelectionHandler();
    }

    public void OnPointerExit()
    {
        if (_controlsController.CurrentControl == 1)
            return;

        _timer = 0;
        _gazedAt = false;
        GazeSelectionHandler.Instance.DisableSelectionHandler();
    }
}
