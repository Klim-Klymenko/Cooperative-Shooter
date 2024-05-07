using TMPro;
using UnityEngine;

namespace UI.View
{
    public sealed class RewardView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _rewardText;
        
        public void ChangeRewardText(string reward)
        {
            _rewardText.text = reward;
        }
    }
}