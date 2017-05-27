using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class Player: MonoBehaviour
    {
        [SerializeField]
        private int gold;
        [SerializeField]
        private Text goldLabel;

        public int Gold
        {
            get
            {
                return gold;
            }
        }

        public static Player Instance
        {
            get; private set;
        }

        private void Awake ()
        {
            Instance = this;
        }

        private void Start ()
        {
            goldLabel.text = gold.ToString ();
            ServicesFacade.Instance.FetchGold (SetGold);
        }

        private void SetGold (int gold)
        {
            this.gold = gold;
            goldLabel.text = gold.ToString ();
        }

        public void UseGold (int amount)
        {
            gold -= amount;
            goldLabel.text = gold.ToString ();
        }
    }
}