using System;

namespace ECS.Redesign
{
    public class ECS
    {
        private int _heaterThreshold = int.MinValue;
        private int _windowThreshold = int.MaxValue;
        private readonly ITempSensor _tempSensor;
        private readonly IHeater _heater;
        private readonly IWindow _window;

        public ECS(int heaterThr, int windowThr, ITempSensor tempSensor, IHeater heater, IWindow window)
        {
            SetHeaterThreshold(heaterThr);
            SetWindowThreshold(windowThr);
            _tempSensor = tempSensor;
            _heater = heater;
            _window = window;
        }

        public void Regulate()
        {
            var t = _tempSensor.GetTemp();
            Console.WriteLine($"Temperature measured was {t}");
            if (t < _heaterThreshold)
            {
                _heater.TurnOn();
                _window.Close();
            }
            else if (t > _windowThreshold)
            {
                _window.Open();
                _heater.TurnOff();
            }
            else
            {
                _heater.TurnOff();
                _window.Close();
            }
        }

        public void SetWindowThreshold(int wThr)
        {
            _windowThreshold = _heaterThreshold < wThr ? wThr : _heaterThreshold + 1;
        }

        public int GetWindowThreshold()
        {
            return _windowThreshold;
        }

        public void SetHeaterThreshold(int thr)
        {
            _heaterThreshold = _windowThreshold > thr ? thr : _windowThreshold - 1;
        }

        public int GetHeaterThreshold()
        {
            return _heaterThreshold;
        }

        public int GetCurTemp()
        {
            return _tempSensor.GetTemp();
        }

        public bool RunSelfTest()
        {
            return _tempSensor.RunSelfTest() && _heater.RunSelfTest() && _window.RunSelfTest();
        }
    }
}
