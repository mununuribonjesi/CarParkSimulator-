using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.VisualBasic;
using CarParkSimulator.CarParkSimulator;

namespace CarParkSimulator
{
    public partial class SimulatorInterface : Form
    {
        // Attributes ///        
        private TicketMachine ticketMachine;
        private ActiveTickets activeTickets;
        private TicketValidator ticketValidator;
        private Barrier entryBarrier;
        private Barrier exitBarrier;
        private FullSign fullSign;
        private CarPark carPark;
        private EntrySensor entrySensor;
        private ExitSensor exitSensor;

        /////////////////
        int num = -1;
        int num2 = 0;
        DateTime[] chip2 = new DateTime[5];
        string reg = "";
       

        // Constructor //
        public SimulatorInterface()
        {
            InitializeComponent();
        }
        /////////////////


        // Operations ///
        private void ResetSystem(object sender, EventArgs e)
        {
            // STUDENTS:
            ///// Class contructors are not defined so there will be errors
            ///// This code is correct for the basic version though
            activeTickets = new ActiveTickets();
            ticketMachine = new TicketMachine(activeTickets);
            ticketValidator = new TicketValidator(activeTickets);
            entryBarrier = new Barrier();
            exitBarrier = new Barrier();
            fullSign = new FullSign();
            carPark = new CarPark(ticketMachine, ticketValidator, fullSign, entryBarrier, exitBarrier);
            entrySensor = new EntrySensor(carPark);
            exitSensor = new ExitSensor(carPark);

            ticketMachine.AssignCarPark(carPark);
            ticketValidator.AssignCarPark(carPark);

            /////////////////////////////////////////
            //btnPay.Visible = true;
            btnCarArrivesAtEntrance.Visible = true;
            btnDriverPressesForTicket.Visible = false;
            btnCarEntersCarPark.Visible = false;
            btnCarArrivesAtExit.Visible = false;
            btnDriverEntersTicket.Visible = false;
            btnCarExitsCarPark.Visible = false;
            lblchip.Text = "NULL";

            /////////////////////////////////////////

            //Active Tickets
            lstActiveTickets.Items.Clear();

            UpdateDisplay();
        }

        private void CarArrivesAtEntrance(object sender, EventArgs e)
        {

            carPark.CarArrivedAtEntrance(entrySensor, ticketMachine);
            reg = Interaction.InputBox("Enter Reg");
            btnCarArrivesAtEntrance.Visible = false;
            btnDriverPressesForTicket.Visible = true;
            btnCarEntersCarPark.Visible = false;
            num2++;
            UpdateDisplay();
            lblchip.Text = "NULL";
        }

        private void DriverPressesForTicket(object sender, EventArgs e)
        {
            carPark.TicketDispensed(ticketMachine, entryBarrier, activeTickets);
            
            btnCarArrivesAtEntrance.Visible = false;
            btnDriverPressesForTicket.Visible = false;
            btnCarEntersCarPark.Visible = true;
            
            num++;
            
            chip2[num] = DateTime.Now;
            lblchip.Text = num + "";
            lstActiveTickets.Items.Add("reg...:" + reg + "...chip.." + chip2[num]);


            UpdateDisplay();
        }

        private void CarEntersCarPark(object sender, EventArgs e)
        {
            carPark.CarEnteredCarPark(entryBarrier, ticketMachine, entrySensor, fullSign);
            
            btnCarArrivesAtEntrance.Visible = true;
            btnDriverPressesForTicket.Visible = false;
            btnCarEntersCarPark.Visible = false;
            btnCarArrivesAtExit.Visible = true;
            lblchip.Text = "";
            if (carPark.IsFull() == true)
            {
                btnCarArrivesAtEntrance.Visible = false;
                btnDriverPressesForTicket.Visible = false;
                btnCarEntersCarPark.Visible = false;
            }

            if (carPark.IsEmpty() == false)
                btnCarArrivesAtExit.Visible = true;
            
            UpdateDisplay();
        }


        private void CarArrivesAtExit(object sender, EventArgs e)
        {
            carPark.CarArrivedAtExit(exitSensor, ticketValidator);
     
            btnCarArrivesAtExit.Visible = false;
            btnDriverEntersTicket.Visible = true;
            btnCarExitsCarPark.Visible = false;
            lblchip.Text =  "NULL";
            UpdateDisplay();
        }

        private void DriverEntersTicket(object sender, EventArgs e)
        {
            carPark.TicketValidated(ticketValidator, exitBarrier, activeTickets);


          
            if ((DateTime.Now.Minute - chip2[num].Minute) >= 1)
            {

                System.Windows.Forms.MessageBox.Show("please go back and pay ticket no:" + num);

            }
                

            else 
            {
                btnCarArrivesAtExit.Visible = false;
                btnDriverEntersTicket.Visible = false;
                btnCarExitsCarPark.Visible = true;
                UpdateDisplay();
                lblchip.Text = num + "";
                num--;
        
         
            
              
            }

           
        }

        private void CarExitsCarPark(object sender, EventArgs e)
        {
            carPark.CarExitedCarPark(exitBarrier, ticketValidator, exitSensor, fullSign);
         
            btnCarArrivesAtExit.Visible = true;
            btnDriverEntersTicket.Visible = false;
            btnCarExitsCarPark.Visible = false;
         

            if (carPark.IsEmpty() == true)
            {
                btnCarArrivesAtExit.Visible = false;
                btnDriverEntersTicket.Visible = false;
                btnCarExitsCarPark.Visible = false;
            }

            if (carPark.HasSpace() == true)
                btnCarArrivesAtEntrance.Visible = true;
            lblchip.Text =  "NULL";
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            lblEntrySensor.Text = "" + entrySensor.IsCarOnSensor();
            lblExitSensor.Text = "" + exitSensor.IsCarOnSensor();
            lblEntryBarrier.Text = "" + entryBarrier.IsLifted();
            lblExitBarrier.Text = "" + exitBarrier.IsLifted();
            lblSpaces.Text = "" + carPark.GetCurrentSpaces();
            lblFullSign.Text = "" + fullSign.IsLit();
            lblTicketMachine.Text = "" + ticketMachine.GetMessage();
            lblTicketValidator.Text = "" + ticketValidator.GetMessage();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }




        private void lstActiveTickets_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnPay_Click(object sender, EventArgs e)
        {
        }

        private void SimulatorInterface_Load(object sender, EventArgs e)
        {

        }

        private void lblTck_Click(object sender, EventArgs e)
        {

        }

   


        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            //btnPay.Visible = true;

            int tckNo = int.Parse(textBox1.Text);

            DateTime current = DateTime.Now;

            //int pricePay = Convert.ToInt32(current.TimeOfDay.Subtract(chip2[tckNo].TimeOfDay));

            int currentMinutes = current.Minute;
            int minutesStayed = chip2[num].Minute;
            double price = 0;

            if (tckNo == num)
            {
                System.Windows.Forms.MessageBox.Show("\nDuration:" + (currentMinutes - minutesStayed));

                if (currentMinutes - minutesStayed == 1)
                {
                    price = 5;

                    string muni = Interaction.InputBox("\nDuration:" + (currentMinutes - minutesStayed) + "min" + "\nPrice inc Vat:" + price);

                    int muni1 = int.Parse(muni);

                    if (price - muni1 == 0)
                    {

                        chip2[num] = DateTime.Now;


                    }

                }

                else if (currentMinutes - minutesStayed > 1)
                {
                    price = 7;

                    string muni = Interaction.InputBox("\nDuration:" + (currentMinutes - minutesStayed) + "min" + "\nPrice inc Vat:" + price);

                    int muni1 = int.Parse(muni);

                    if (price - muni1 == 0)
                    {

                        chip2[num] = DateTime.Now;


                    }
                }

                else if (currentMinutes - minutesStayed > 1)
                {

                    price = 10;

                    string muni = Interaction.InputBox("\nDuration:" + current.Minute.CompareTo(chip2[tckNo].Minute) + "min" + "\nPrice inc Vat:" + price);

                    int muni1 = int.Parse(muni);

                    if (price - muni1 == 0)
                    {

                        chip2[num] = DateTime.Now;


                    }

                }


            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //carPark.TicketDispensed(ticketMachine, entryBarrier, activeTickets);
            //btnPay.Visible = false;
            btnCarArrivesAtEntrance.Visible = false;
            btnDriverPressesForTicket.Visible = false;
            btnCarEntersCarPark.Visible = false;
            btnCarArrivesAtExit.Visible = false;
            btnDriverEntersTicket.Visible = false;
            btnCarExitsCarPark.Visible = false;
            fullSign.emergencyLit();
            entryBarrier.Raise();
            exitBarrier.Raise();
            entrySensor.CarDetected();
            exitSensor.CarDetected();
            lblSpaces.Text = "EMERGENCY !!!";
            lblchip.Text = "NULL";
            button1.Visible = false;
            UpdateDisplay();
           
        }
    }
    }
    
