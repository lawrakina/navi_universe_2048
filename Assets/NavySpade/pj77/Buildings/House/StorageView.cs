using System;
using TMPro;
using UnityEngine;


namespace NavySpade.pj77.Buildings.House{
    public class StorageView : MonoBehaviour{
        [SerializeField]
        private TMP_Text _moneyCount;

        public int StorageMoney{
            get => Convert.ToInt32(_moneyCount.text);
            set => _moneyCount.text = value.ToString();
        }
    }
}