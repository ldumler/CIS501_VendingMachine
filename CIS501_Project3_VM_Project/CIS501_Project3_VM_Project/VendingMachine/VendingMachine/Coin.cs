using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Coin
    {
        public int numCoins { get; set; }
        public int value { get; }
        private int coinsToReturn = 0;
        private CoinInserter coinInserter;
        private CoinDispenser coinDispenser;
        private VendingMachineController vmController;

        public Coin(int inventory, int coinValue, CoinInserter inserter, CoinDispenser dispenser, VendingMachineController vmController)
        {
            numCoins = inventory;
            value = coinValue;
            coinInserter = inserter;
            coinDispenser = dispenser;
            this.vmController = vmController;
        }

        public void InsertCoin()
        {
            numCoins++;
            vmController.amtInserted += value;
            vmController.coinInserted();
        }

        public int makeChange(int change)
        {
            coinsToReturn = 0;
            if (change >= value)
            {
                coinsToReturn = change / value;
                if (coinsToReturn > numCoins) coinsToReturn = numCoins;
            }
            return (coinsToReturn * value);
        }

        public void dispenseChange()
        {
            numCoins -= coinsToReturn;
            coinDispenser.Actuate(coinsToReturn);
        }
    }
}
