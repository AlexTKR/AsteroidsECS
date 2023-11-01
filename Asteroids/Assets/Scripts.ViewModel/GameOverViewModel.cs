using Scripts.Data;
using Scripts.GlobalEvents;
using Scripts.UI;
using Scripts.UI.Panels;
using Zenject;

namespace Scripts.ViewModel
{
    public class GameOverViewModel : ViewModelBase<GameOverPanel>
    {
        private IDataProvider _dataProvider;

        [Inject]
        void Construct(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public override void Compose(GameOverPanel panel)
        {
            base.Compose(panel);
            var playerData = _dataProvider.Get<PlayerData>();
            panel.Subscribe(playerData.Data.Score, value => { panel.ScoreText.text = $"Score: {value}"; });
            panel.RestartButton.onClick.AddListener(() =>
            {
                EventProcessor.Get<RestartGameEvent>().Publish();
                panel.SetActive(false);
            });
        }
    }
}