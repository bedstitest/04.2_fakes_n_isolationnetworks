namespace ECS.Redesign
{
    public interface IHeater
    {
        void TurnOn();

        void TurnOff();

        bool RunSelfTest();
    }
}