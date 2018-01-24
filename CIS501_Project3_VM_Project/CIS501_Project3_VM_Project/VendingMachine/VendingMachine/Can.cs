using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Can
    {
        public string name { get; }
        private int price;
        public int inventory { get; set; }
        private Light soldOutLight;
        private PurchaseButton pb;
        private CanDispenser canDispenser;
        private VendingMachineController vmController;
        private Light purchasableLight;

        public Can(int p, int i, string n, Light so, PurchaseButton button, CanDispenser dispenser, VendingMachineController c, Light purchaseLight)
        {
            name = n;
            price = p;
            inventory = i;
            soldOutLight = so;
            pb = button;
            canDispenser = dispenser;
            vmController = c;
            purchasableLight = purchaseLight;
        }

        public void checkIfCanPurchase(int amountInserted)
        {
            if (amountInserted >= price && inventory > 0) purchasableLight.TurnOn();
            else purchasableLight.TurnOff();

            if (inventory == 0)
            {
                soldOutLight.TurnOn();
                purchasableLight.TurnOff();
            }
            else soldOutLight.TurnOff();
        }

        public void purchaseCan()
        {
            int change = vmController.amtInserted - price;
                if(purchasableLight.IsOn() && vmController.CheckIfCanMakeChange(change))
                {
                    canDispenser.Actuate();
                    inventory--;
                    vmController.amtInserted -= price;
                    vmController.makeChange();
                    if (inventory == 0) soldOutLight.TurnOn();
                }
        }
    }
}
