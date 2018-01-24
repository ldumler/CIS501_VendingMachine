//////////////////////////////////////////////////////////////////////
//      Vending Machine (Form1.cs)                                  //
//      Written by Masaaki Mizuno, (c) 2006, 2007, 2008, 2010       //
//                      for Learning Tree Course 123P, 252J, 230Y   //
//                 also for KSU Course CIS501                       //  
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VendingMachine
{
    public partial class VendingMachine : Form
    {
        // Static Constants
        public const int NUMCANTYPES = 4;
        public const int NUMCOINTYPES = 4;
        public static readonly int[] NUMCANS = {4,4,4,4};
        public static readonly int[] CANPRICES = { 120, 170, 130, 110 };
        public static readonly string[] CANNAMES = { "Coca-Cola", "Pepsi", "Dr. Pepper", "Sprite" };

        public static readonly int[] COINVALUES = { 10, 50, 100, 500 };
        public static readonly int[] NUMCOINS = { 15, 10, 5, 2 };
            // 10Yen, 50Yen, 100Yen, 500Yen
      
        // Boundary Objects
        private AmountDisplay amountDisplay;
        private DebugDisplay displayPrice0, displayPrice1, displayPrice2, displayPrice3;
        private DebugDisplay displayNum10Yen, displayNum50Yen, displayNum100Yen, displayNum500Yen;
        private DebugDisplay displayName0, displayName1, displayName2, displayName3;
        private DebugDisplay displayNumCans0, displayNumCans1, displayNumCans2, displayNumCans3;
        private Light soldOutLight0, soldOutLight1, soldOutLight2, soldOutLight3;
        private TimerLight noChangeLight;
        private Light purchasableLight0, purchasableLight1, purchasableLight2, purchasableLight3;
        private CoinDispenser coinDispenser10Yen, coinDispenser50Yen, coinDispenser100Yen, coinDispenser500Yen;
        private CanDispenser canDispenser0, canDispenser1, canDispenser2, canDispenser3;
        private CoinInserter coinInserter10Yen, coinInserter50Yen, coinInserter100Yen, coinInserter500Yen;
        private PurchaseButton purchaseButton0, purchaseButton1, purchaseButton2, purchaseButton3;
        private CoinReturnButton coinReturnButton;

        // Declare fields for your entity and control objects
        private Can canCola, canPepsi, canDrPepper, canSprite;
        private VendingMachineController vmController;
        private Coin coin10, coin50, coin100, coin500;
        private List<Can> cans;
        private List<Coin> coins;


        public VendingMachine()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            amountDisplay = new AmountDisplay(txtAmount);

            displayNum10Yen = new DebugDisplay(txtNum10Yen);
            displayNum50Yen = new DebugDisplay(txtNum50Yen);
            displayNum100Yen = new DebugDisplay(txtNum100Yen);
            displayNum500Yen = new DebugDisplay(txtNum500Yen);
            displayPrice0 = new DebugDisplay(txtPrice0);
            displayPrice1 = new DebugDisplay(txtPrice1);
            displayPrice2 = new DebugDisplay(txtPrice2);
            displayPrice3 = new DebugDisplay(txtPrice3);
            displayName0 = new DebugDisplay(txtName0);
            displayName1 = new DebugDisplay(txtName1);
            displayName2 = new DebugDisplay(txtName2);
            displayName3 = new DebugDisplay(txtName3);
            displayNumCans0 = new DebugDisplay(txtNumCan0);
            displayNumCans1 = new DebugDisplay(txtNumCan1);
            displayNumCans2 = new DebugDisplay(txtNumCan2);
            displayNumCans3 = new DebugDisplay(txtNumCan3);

            soldOutLight0 = new Light(pbxSOLight0, Color.Orange);
            soldOutLight1 = new Light(pbxSOLight1, Color.Orange);
            soldOutLight2 = new Light(pbxSOLight2, Color.Orange);
            soldOutLight3 = new Light(pbxSOLight3, Color.Orange);

            noChangeLight = new TimerLight(pbxNoChange, Color.Red, timer1);

            purchasableLight0 = new Light(pbxPurLight0, Color.Aqua);
            purchasableLight1 = new Light(pbxPurLight1, Color.Aqua);
            purchasableLight2 = new Light(pbxPurLight2, Color.Aqua);
            purchasableLight3 = new Light(pbxPurLight3, Color.Aqua);

            coinDispenser10Yen = new CoinDispenser(txtChange10Yen);
            coinDispenser50Yen = new CoinDispenser(txtChange50Yen);
            coinDispenser100Yen = new CoinDispenser(txtChange100Yen);
            coinDispenser500Yen = new CoinDispenser(txtChange500Yen);

            // All candispensers share the same output textbox for simulation
            canDispenser0 = new CanDispenser(txtCanDispenser, CANNAMES[0]);
            canDispenser1 = new CanDispenser(txtCanDispenser, CANNAMES[1]);
            canDispenser2 = new CanDispenser(txtCanDispenser, CANNAMES[2]);
            canDispenser3 = new CanDispenser(txtCanDispenser, CANNAMES[3]);

            // Instantiate your entity and control objects
            // Connect these objects

            cans = new List<Can>();
            coins = new List<Coin>();
            vmController = new VendingMachineController(cans, coins, noChangeLight, amountDisplay);
            canCola = new Can(CANPRICES[0], NUMCANS[0], CANNAMES[0], soldOutLight0, purchaseButton0, canDispenser0, vmController, purchasableLight0);
            cans.Add(canCola);
            canPepsi = new Can(CANPRICES[1], NUMCANS[1], CANNAMES[1], soldOutLight1, purchaseButton1, canDispenser1, vmController, purchasableLight1);
            cans.Add(canPepsi);
            canDrPepper = new Can(CANPRICES[2], NUMCANS[2], CANNAMES[2], soldOutLight2, purchaseButton2, canDispenser2, vmController, purchasableLight2);
            cans.Add(canDrPepper);
            canSprite = new Can(CANPRICES[3], NUMCANS[3], CANNAMES[3], soldOutLight3, purchaseButton3, canDispenser3, vmController, purchasableLight3);
            cans.Add(canSprite);

            coin500 = new Coin(NUMCOINS[3], COINVALUES[3], coinInserter500Yen, coinDispenser500Yen, vmController);
            coins.Add(coin500);
            coin100 = new Coin(NUMCOINS[2], COINVALUES[2], coinInserter100Yen, coinDispenser100Yen, vmController);
            coins.Add(coin100);
            coin50 = new Coin(NUMCOINS[1], COINVALUES[1], coinInserter50Yen, coinDispenser50Yen, vmController);
            coins.Add(coin50);
            coin10 = new Coin(NUMCOINS[0], COINVALUES[0], coinInserter10Yen, coinDispenser10Yen, vmController);
            coins.Add(coin10);

            // You must replace the following default constructors with 
            // constructors with arguments (non-default constructors)
            // to pass (set) the first object that ButtonPressed() will
            // visit

            purchaseButton0 = new PurchaseButton(canCola);
            purchaseButton1 = new PurchaseButton(canPepsi);
            purchaseButton2 = new PurchaseButton(canDrPepper);
            purchaseButton3 = new PurchaseButton(canSprite);

            // You must replace the following default constructors with
            // constructors that take armuments to pass the first object that
            // the CoinInserted() will call
            coinInserter10Yen = new CoinInserter(coin10);
            coinInserter50Yen = new CoinInserter(coin50);
            coinInserter100Yen = new CoinInserter(coin100);
            coinInserter500Yen = new CoinInserter(coin500);

            coinReturnButton = new CoinReturnButton(vmController);

            // Display debug information
            displayCanPricesAndNames();
            updateDebugDisplays();
        }

 
        private void btnCoinInserter10Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter10Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnCoinInserter50Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter50Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnCoinInserter100Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter100Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnCoinInserter500Yen_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinInserter500Yen.CoinInserted();
            updateDebugDisplays();
        }

        private void btnPurButtn0_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton0.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnPurButton1_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton1.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnPurButton2_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton2.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnPurButton3_Click(object sender, EventArgs e)
        {
            // Do not change the body
            purchaseButton3.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnCoinReturn_Click(object sender, EventArgs e)
        {
            // Do not change the body
            coinReturnButton.ButtonPressed();
            updateDebugDisplays();
        }

        private void btnChangePickedUp_Click(object sender, EventArgs e)
        {
            // This is just for a simulation
            coinDispenser10Yen.Clear();
            coinDispenser50Yen.Clear();
            coinDispenser100Yen.Clear();
            coinDispenser500Yen.Clear();
        }

        private void btnCanPickedUp_Click(object sender, EventArgs e)
        {
            // This is just for a simulation
            canDispenser0.Clear(); // since all canDispenser objects accesses the
                                      // same textbox object
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Write the body to reset the field values of entity objects
            foreach (Can c in cans)
            {
                c.inventory = 4;
                c.checkIfCanPurchase(0); //will reset purchasable lights and sold out lights
            }

            coin500.numCoins = NUMCOINS[3];
            coin100.numCoins = NUMCOINS[2];
            coin50.numCoins = NUMCOINS[1];
            coin10.numCoins = NUMCOINS[0];

            vmController.amtInserted = 0;
            amountDisplay.DisplayAmount(vmController.amtInserted);

            updateDebugDisplays();
        }

        private void displayCanPricesAndNames()
        {
            displayPrice0.Display("\\" + CANPRICES[0]);
            displayPrice1.Display("\\" + CANPRICES[1]);
            displayPrice2.Display("\\" + CANPRICES[2]);
            displayPrice3.Display("\\" + CANPRICES[3]);
            displayName0.Display(CANNAMES[0]);
            displayName1.Display(CANNAMES[1]); 
            displayName2.Display(CANNAMES[2]);
            displayName3.Display(CANNAMES[3]);
        }

        private void updateDebugDisplays()
        {
            // You need to change XXX to appropriate "object.property"

            displayNum10Yen.Display(coin10.numCoins);
            displayNum50Yen.Display(coin50.numCoins);
            displayNum100Yen.Display(coin100.numCoins);
            displayNum500Yen.Display(coin500.numCoins);
            displayNumCans0.Display(canCola.inventory);
            displayNumCans1.Display(canPepsi.inventory);
            displayNumCans2.Display(canDrPepper.inventory);
            displayNumCans3.Display(canSprite.inventory);

        }    
    }
}