using Base.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerRankView : MonoBehaviour
{
    [SerializeField] private Image _currentRankIcon;
    [SerializeField] private TextMeshProUGUI _currentExperience;
    [SerializeField] private TextMeshProUGUI _currentRankTitle;
    private RankController _rankController;

    [Inject]
    public void Init(RankController rankController)
    {
        _rankController = rankController;
    }

    private void Start() 
    {
        ShowStats();
    }

    private void ShowStats()
    {
        _currentExperience.text = _rankController.PlayerExperience.ToString();
        _currentRankIcon.sprite = _rankController.ObtainCurrentRankIcon();

        if (_currentRankTitle != null)
            _currentRankTitle.text = _rankController.ObtainCurrentRankTitle();
    }
}
