using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "Game/GameConfigs")]
    public class GameConfigs : ScriptableObject
    {
        public GameObject playerPrefab;
        public GameObject CM_FollowCamPrefab;
    }
}
