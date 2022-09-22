using System;
using ECS.Redesign;

namespace ECS.Redesign.test
{
    public class Tests
    {
        ECS uut;
        FakeTempSensor FS;
        FakeHeater FH;

        [SetUp]
        public void Setup()
        {
            FS = new FakeTempSensor();
            FH = new FakeHeater();
            uut = new ECS(45,FS,FH);
        }

        [Test]
        public void SetThreshold()
        {
            uut.SetHeaterThreshold(25);
            Assert.That(uut.GetHeaterThreshold(), Is.EqualTo(25));
        }
        [Test]
        public void IsHeater_on()
        {
            FH.heaterWasTurnOn = false;
            FH.TurnOn();
            Assert.That(FH.heaterWasTurnOn, Is.True);
        }


        [Test]
        public void IsHeater_off()
        {
            FH.heaterWasTurnOff = false;
            FH.TurnOff();
            Assert.That(FH.heaterWasTurnOff, Is.True);
        }

        [Test]
        public void Regulate_HighTemp_HeaterTurnedOff()
        {
            FS.Temp = 45;
            uut.Regulate();

            Assert.That(FH.heaterWasTurnOff, Is.True);
            Assert.That(FH.heaterWasTurnOn, Is.False);
        }
        [Test]
        public void Regulate_LowTemp_HeaterTurnedOn()
        {
            FS.Temp = -5;
            uut.Regulate();

            Assert.That(FH.heaterWasTurnOn, Is.True);
            Assert.That(FH.heaterWasTurnOff, Is.False);
        }

        [Test]
        public void Regulate_LowTemp_WindowOpened()
        {
            
        }
        [Test]
        public void Regulate_LowTemp_WindowClosed()
        {

        }
        [Test]
        public void Regulate_HighTemp_WindowOpened()
        {

        }
        [Test]
        public void Regulate_HighTemp_WindowClosed()
        {

        }



        //[Test]
        //public void Regulate_fake_low()
        //{
        //    FS.Temp = -5;
        //    FS.Thr = 10;
        //    bool reg_low_or_high = FS.Regulate();

        //    if(reg_low_or_high == true)
        //    {
        //        FH.TurnOn();
        //    }
        //    else
        //    {
        //        throw new Exception("Heater was not turned on");
        //    }

        //    Assert.That(FH.heaterWasTurnOn, Is.True);
        //}
        //[Test]
        //public void Regulate_fake_high()
        //{
        //    FS.Temp = 15;
        //    FS.Thr = 10;
        //    bool reg_low_or_high = FS.Regulate();

        //    if (reg_low_or_high == true)
        //    {
        //        FH.TurnOff();
        //    }
        //    else
        //    {
        //        throw new Exception("Heater was not turned on");
        //    }

        //    Assert.That(FH.heaterWasTurnOff, Is.True);
        //}
        //[Test]
        //public void regulate()
        //{
        //    FS.Temp = 100;


        //}
        [Test]
        public void RunSelfTest_ResultIsCorrect()
        {
            Assert.That(uut.RunSelfTest(), Is.True);
        }
    }
}