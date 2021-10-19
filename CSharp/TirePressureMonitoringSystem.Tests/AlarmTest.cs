using System;
using Xunit;

namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class TestSensor : Sensor
    {
        private readonly double _pressure;

        public TestSensor(double pressure)
        {
            _pressure = pressure;
        }
        
        public override double PopNextPressurePsiValue() => _pressure;
    }
    
    public class AlarmTest
    {
        [Theory]
        [InlineData(17)]
        [InlineData(19.1)]
        [InlineData(21)]
        public void WhenThePressureIsWithinLimits_ThenTheAlarmIsNotOn(double pressure)
        {
            var alarm = new Alarm(new TestSensor(pressure));
            alarm.Check();
            Assert.False(alarm.AlarmOn);
        }
        
        [Theory]
        [InlineData(double.MinValue)]
        [InlineData(16.999999999999)]
        [InlineData(21.000000000001)]
        [InlineData(double.MaxValue)]
        public void WhenThePressureIsOutsideLimits_ThenTheAlarmIsOn(double pressure)
        {
            var alarm = new Alarm(new TestSensor(pressure));
            alarm.Check();
            Assert.True(alarm.AlarmOn);
        }
        
    }
}