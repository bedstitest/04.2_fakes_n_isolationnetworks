using System;
using ECS.Redesign;
using NSubstitute;

namespace ECS.Redesign.test
{
    public class Tests
    {
        ECS uut;
        ITempSensor FS;
        IHeater FH;
        IWindow FW;

        [SetUp]
        public void Setup()
        {
            FH = Substitute.For<IHeater>();
            FS = Substitute.For<ITempSensor>();
            FW = Substitute.For <IWindow>();
            
            uut = new ECS(15, 25, FS, FH, FW);
        }

        [Test]
        public void SetHeaterThreshold()
        {
            uut.SetHeaterThreshold(20);
            Assert.That(uut.GetHeaterThreshold(), Is.EqualTo(20));
        }

        #region RegulateTests
        [Test]
        public void Regulate_HighTemp_HeaterTurnedOff()
        {
            FS.GetTemp().Returns(45);
            uut.Regulate();
            FH.Received(1).TurnOff();
        }
        [Test]
        public void Regulate_LowTemp_HeaterTurnedOn()
        {

            FS.GetTemp().Returns(5);
            uut.Regulate();
            FH.Received().TurnOn();
        }
        [Test]
        public void Regulate_LowTemp_WindowClosed()
        {
            FS.GetTemp().Returns(5);
            uut.Regulate();
            FW.Received(1).Close();
        }
        [Test]
        public void Regulate_HighTemp_WindowOpened()
        {
            FS.GetTemp().Returns(45);
            uut.Regulate();
            FW.Received(1).Open();
        }
        [Test]
        public void Regulate_MiddleTemp_WindowClosed()
        {
            FS.GetTemp().Returns(20);
            uut.Regulate();
            FW.Received(1).Close();
            //FH.Received(1).TurnOff();
        }
        #endregion

        #region GetTests
        [Test]
        public void GetWindowThreshold()
        {
            uut.GetHeaterThreshold();
            
        }
        #endregion

        #region RunSelfTests
        [Test]
        public void RunSelfTest_FakeWindowFails_ECSFails()
        {
            FH.RunSelfTest().Returns(true);
            FS.RunSelfTest().Returns(true);
            FW.RunSelfTest().Returns(false);
            Assert.That(uut.RunSelfTest(), Is.False);

        }

        [Test]
        public void RunSelfTest_FakeTempSensorFails_ECSFails()
        {
            FH.RunSelfTest().Returns(true);
            FS.RunSelfTest().Returns(false);
            FW.RunSelfTest().Returns(true);
            Assert.That(uut.RunSelfTest(), Is.False);
        }
        [Test]
        public void RunSelfTest_FakeHeaterFails_ECSFails()
        {
            FH.RunSelfTest().Returns(false);
            FS.RunSelfTest().Returns(true);
            FW.RunSelfTest().Returns(true);
            Assert.That(uut.RunSelfTest(), Is.False);
        }

        [Test]
        public void RunSelfTest_ECSSucceeds()
        {
            FH.RunSelfTest().Returns(true);
            FS.RunSelfTest().Returns(true);
            FW.RunSelfTest().Returns(true);
            Assert.That(uut.RunSelfTest(), Is.True);
        }
        #endregion

    }
}