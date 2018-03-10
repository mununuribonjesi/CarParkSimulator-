using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarParkSimulator
{
    class EntrySensor:Sensor
    {
        private CarPark carPark;

        public EntrySensor(CarPark carPark)
        {
            
            IsCarOnSensor();
            this.carPark = carPark;
        }   
    }
}