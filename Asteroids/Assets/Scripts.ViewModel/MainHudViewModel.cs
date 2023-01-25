using Scripts.Data;
using Scripts.UI;
using Scripts.UI.Panels;
using UnityEngine;
using Zenject;

namespace Scripts.ViewModel
{
    public class MainHudViewModel : ViewModelBase<MainHudPanel>
    {
        private IDataProvider _dataProvider;

        [Inject]
        void Construct(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        

        public override void Compose(MainHudPanel panel)
        {
            base.Compose(panel);
            var playerDataRepository = _dataProvider.Get<PlayerData>();

            panel.Subscribe(playerDataRepository.Data.InstantSpeed,
                value => { panel.PlayerInstantSpeedText.text = value.ToString("F1"); });
            panel.Subscribe(playerDataRepository.Data.LaserCount,
                value => panel.LaserCountText.text = value.ToString("F0"));
            panel.Subscribe(playerDataRepository.Data.LaserDelay,
                value => panel.LaserDelayText.text = value.ToString("F0"));
        }
    }
}