using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarParkSimulator
{
    class TicketValidator
    {
        private string message;
        private CarPark carPark;
        private ActiveTickets activeTickets;

        public TicketValidator(ActiveTickets activeTickets)
        {
            this.activeTickets = activeTickets;
        }

        public void AssignCarPark(CarPark carPark)
        {
            this.carPark = carPark;
        }

        public void CarArrived()
        {
            message = "Please insert your chip.";
        }

        public void TicketEntered(ActiveTickets activeTickets)
        {
            message = "Thank you! Please come again.";
            this.activeTickets = activeTickets;
            //activeTickets.RemoveTicket();
        }

        public void ClearMessage()
        {
            message = " ";
        }

        public string GetMessage()
        {
            return message;
        }
    }
}
