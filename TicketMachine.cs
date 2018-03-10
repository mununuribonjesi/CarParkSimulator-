using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSimulator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace CarParkSimulator
    {
        class TicketMachine
        {
            private string message;
            private CarPark carPark;
            private ActiveTickets activeTickets;

            public TicketMachine(ActiveTickets activeTickets)
            {
                this.activeTickets = activeTickets;
            }

            public void AssignCarPark(CarPark carPark)
            {
                this.carPark = carPark;
            }

            public void CarArrived()
            {
                message = "Please press to get a chip.";
            }

            public void PrintTicket(ActiveTickets activeTickets)
            {
                message = "Thank you! Enjoy your stay.";
                this.activeTickets = activeTickets;
         
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

}
