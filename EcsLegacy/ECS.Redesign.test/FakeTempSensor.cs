using System;

namespace ECS.Redesign
{
    internal class FakeTempSensor : ITempSensor
    {
        private int temp;
        private int thr;
        //public int Temp { set { temp = value; } get { return Temp; } }
        //public int Thr { set { thr = value; } get { return Thr; } }

        public int GetTemp()
        {
            return temp;
        }


        /*
        public bool Regulate()
        {
            temp = Temp;
            if (temp < Thr)
            {
                return true;
            }
            else return false;

        }
        */
        public bool RunSelfTest()
        {
            return true;
        }
        
    }
}