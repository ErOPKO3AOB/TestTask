using Assets.Scripts.Health;
using Assets.Scripts.Player.Services;

namespace Assets.Scripts.Player
{
    public class PlayerModel
    {
        public PlayerModel(PlayerView playerView, AutoAimService autoAimService, LookService lookService, MoveService moveService, HealthService healthService)
        {
            playerView.Construct(this);

            this.autoAimService = autoAimService;
            this.lookService = lookService;
            this.moveService = moveService;
            this.healthService = healthService;
        }

        public readonly AutoAimService autoAimService;
        public readonly LookService lookService;
        public readonly MoveService moveService;

        public readonly HealthService healthService;
    }
}