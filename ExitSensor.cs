using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarParkSimulator
{
    class ExitSensor:Sensor
    {
        private CarPark carPark;

        public ExitSensor(CarPark carPark)
        {
            IsCarOnSensor();
            this.carPark = carPark;
        }
    }
}
