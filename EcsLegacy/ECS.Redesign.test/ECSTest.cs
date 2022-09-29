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

        #region SetGetTests
        [Test]
        public void SetGetWindowThreshold()
        {
            uut.SetWindowThreshold(40);
            Assert.That(uut.GetWindowThreshold(), Is.EqualTo(40));
        }
        [Test]
        public void SetGetHeaterThreshold()
        {
            uut.SetHeaterThreshold(20);
            Assert.That(uut.GetHeaterThreshold(), Is.EqualTo(20));
        }

        [Test]
        public void GetCurTemp()
        {
            FS.GetTemp().Returns(20);
            uut.Regulate();
            FS.Received(1).GetTemp();
        }
        #endregion

        #region RegulateTests
        //1
        [Test]
        public void Regulate_HighTemp_HeaterTurnedOff()
        {
            FS.GetTemp().Returns(26);
            uut.Regulate();
            FH.Received(1).TurnOff();
        }
        //2
        [Test]
        public void Regulate_LowTemp_HeaterTurnedOn()
        {

            FS.GetTemp().Returns(14);
            uut.Regulate();
            FH.Received().TurnOn();
        }
        //3
        [Test]
        public void Regulate_LowTemp_WindowClosed()
        {
            FS.GetTemp().Returns(14);
            uut.Regulate();
            FW.Received(1).Close();
        }
        //4
        [Test]
        public void Regulate_HighTemp_WindowOpened()
        {
            FS.GetTemp().Returns(26);
            uut.Regulate();
            FW.Received(1).Open();
        }
        //5
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(24)]
        [TestCase(25)]
        public void Regulate_MiddleTemp_WindowClosed(int input)
        {
            FS.GetTemp().Returns(input);
            uut.Regulate();
            FW.Received(1).Close();
        }
        //6
        [TestCase(15)] //On boundary value
        [TestCase(16)] //1 above boundary value
        [TestCase(24)] //1 below boundary valye
        [TestCase(25)] //on boundary value
        public void Regulate_MiddleTemp_HeaterTurnedOff(int input)
        {
            FS.GetTemp().Returns(input);
            uut.Regulate();
            FH.Received(1).TurnOff();
        }

        /*
         Tests 5 and 6 COULD be combined (when code implementation is known),
         Tests 2 and 3 COULD be combined (when code implementation is known),
         Tests 1 and 4 COULD be combined (when code implementation is known)
        */
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