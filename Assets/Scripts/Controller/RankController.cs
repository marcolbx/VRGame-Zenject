using Base.Model;
using Base.Signal;
using UnityEngine;
using Zenject;

namespace Base.Controller
{
    public class RankController
    {
        private Player _player;
        private SignalBus _bus;
        public uint PlayerExperience => _player.Experience;
        private Ranks _rankList;

        public RankController(Player player, SignalBus bus, Ranks ranks)
        {
            _player = player;
            _bus = bus;
            _rankList = ranks;
        }

        public uint GetPlayerRank()
        {
            if (_player.Experience < 20)
                return 0;
            else if (_player.Experience < 40)
                return 1;
            else if (_player.Experience < 70)
                return 2;
            else if (_player.Experience < 100)
                return 3;
            else if (_player.Experience < 150)
                return 4;
            else if (_player.Experience < 250)
                return 5;
            else if (_player.Experience < 500)
                return 6;
            else if (_player.Experience < 1000)
                return 7;
            else
                return 8;
        }

        public Sprite ObtainCurrentRankIcon()
        {
            return _rankList.RankIcon[GetPlayerRank()];
        }

        public string ObtainCurrentRankTitle()
        {
            return _rankList.Rank[GetPlayerRank()];
        }
    }
}