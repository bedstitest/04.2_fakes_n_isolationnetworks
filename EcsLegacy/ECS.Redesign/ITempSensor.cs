namespace ECS.Redesign {
    public interface ITempSensor
    {
        int GetTemp();
        bool RunSelfTest();
    }
}