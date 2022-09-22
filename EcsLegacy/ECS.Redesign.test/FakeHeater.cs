namespace ECS.Redesign
{
    public class FakeHeater : IHeater
    {
        public bool heaterWasTurnOn { set; get; }
        public bool heaterWasTurnOff { set; get; }
        public void TurnOn()
        {
            heaterWasTurnOn = true;
        }

        public void TurnOff()
        {
            heaterWasTurnOff = true;
        }

        public bool RunSelfTest()
        {
            return true;
        }
    }
}