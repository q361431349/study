using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    abstract class Motorcycle
    {

        public void StartEngine() { }

        public void AddGas(int gallons) { }


        public virtual int Drive(int miles, int speed) { return 1; }


        public virtual int Drive(TimeSpan time, int speed) { return 0; }


        public abstract double GetTopSpeed();
        public virtual void A() { }
    }
}
