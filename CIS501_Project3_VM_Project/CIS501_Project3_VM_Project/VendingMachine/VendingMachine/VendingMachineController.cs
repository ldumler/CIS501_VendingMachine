using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class VendingMachineController
    {
        private List<Can> cans;
        private List<Coin> coins;
        private TimerLight noChangeLight;
        private AmountDisplay amtDisplay;
        public int amtInserted { get; set; }
        private int change;

        public VendingMachineController(List<Can> cans, List<Coin> coins, TimerLight noChange, AmountDisplay ad) {
            this.cans = cans;
            this.coins = coins;
            noChangeLight = noChange;
            amtDisplay = ad;
        }

        public void coinInserted()
        {
            foreach (Coin c in coins)
            {
             amtDisplay.DisplayAmount(amtInserted);
                 foreach (Can can in cans)
                  {
                   can.checkIfCanPurchase(amtInserted);
                  }
             }
         }

        public void makeChange()
        {
            foreach(Coin c in coins)
            {
                change = c.makeChange(amtInserted);
                amtInserted -= change;
                c.dispenseChange();
            }

            amtDisplay.DisplayAmount(amtInserted);

            foreach (Can c in cans)
            {
                c.checkIfCanPurchase(amtInserted);
            }
        }

        public bool CheckIfCanMakeChange(int changeAmount)
        {
            foreach (Coin c in coins) changeAmount -= c.makeChange(changeAmount);
            if (changeAmount == 0) return true;
            else
            {
                noChangeLight.TurnOn3Sec();
                return false;
            }
        }
    }
}
