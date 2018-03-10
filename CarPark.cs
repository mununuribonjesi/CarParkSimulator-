using CarParkSimulator.CarParkSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarParkSimulator
{
    class CarPark
    {
        private int currentSpaces =5;
        private int maxSpaces = 5;
        private TicketMachine  ticketMachine;
        private TicketValidator ticketValidator;
        private FullSign fullSign;
        private Barrier entryBarrier;
        private Barrier exitBarrier;

        public CarPark(int currentSpaces, int maxSpaces)
        {
            this.currentSpaces = currentSpaces;
            this.maxSpaces = maxSpaces;
        }

        public CarPark(TicketMachine ticketMachine, TicketValidator ticketValidator, FullSign fullSign, Barrier entryBarrier, Barrier exitBarrier)
        {
            this.ticketMachine = ticketMachine;
            this.ticketValidator = ticketValidator;
            this.fullSign = fullSign;
            this.entryBarrier = entryBarrier;
            this.exitBarrier = exitBarrier;
        }

        public void CarArrivedAtEntrance(EntrySensor entrySensor, TicketMachine ticketMachine)
        {
            entrySensor.CarDetected();
            ticketMachine.CarArrived();
        }

        public void TicketDispensed(TicketMachine ticketMachine, Barrier entryBarrier, ActiveTickets activeTickets )
        {
            entryBarrier.Raise();
            ticketMachine.PrintTicket(activeTickets);
        }

        public void CarEnteredCarPark(Barrier entryBarrier, TicketMachine ticketMachine, EntrySensor entrySensor, FullSign fullSign)
        {
            entrySensor.CarLeftSensor();
            entryBarrier.Lower();
            ticketMachine.ClearMessage();
            currentSpaces--;
            if (IsFull() == true)
                fullSign.SetLit();
        }

        public void CarArrivedAtExit(ExitSensor exitSensor, TicketValidator ticketValidator)
        {
            exitSensor.CarDetected();
            ticketValidator.CarArrived();
        }

        public void TicketValidated(TicketValidator ticketValidator, Barrier exitBarrier, ActiveTickets activeTickets)
        {
            exitBarrier.Raise();
            ticketValidator.TicketEntered(activeTickets);
        }

        public void CarExitedCarPark(Barrier exitBarrier, TicketValidator ticketValidator, ExitSensor exitSensor, FullSign fullSign)
        {
            exitSensor.CarLeftSensor();
            exitBarrier.Lower();
            ticketValidator.ClearMessage();
            if (IsFull() == true)
                fullSign.SetLit();
            currentSpaces++;
        }

        public bool IsFull()
        {
            bool full;
            if (currentSpaces == 0)
                full = true;
            else
                full = false;
            return full;
        }

        public bool IsEmpty()
        {
            bool empty;
            if (currentSpaces == maxSpaces)
                empty = true;
            else
                empty = false;
            return empty;
        }

        public bool HasSpace()
        {
            bool space;
            if (currentSpaces > 0)
                space = true;
            else
                space = false;
            return space;
        }

        public int GetCurrentSpaces()
        {
            return currentSpaces;
        }
    }
}
