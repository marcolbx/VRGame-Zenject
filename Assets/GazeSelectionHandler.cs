using TMPro;
using UnityEngine;

public class GazeSelectionHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentSecondsToSelect;
    private static GazeSelectionHandler _instance;
    public static GazeSelectionHandler Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else 
        {
            _instance = this;
        }
    }

    public void DisableSelectionHandler()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void EnableSelectionHandler()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void UpdateCountdown(float number)
    {
        var integer = Mathf.RoundToInt(number);
        _currentSecondsToSelect.text = integer.ToString();
    }
}
