using System;
using System.Device.Gpio;
using System.Diagnostics.CodeAnalysis;
using Iot.Device.DHTxx;

namespace SensorsManager
{
    public static class Dht
    {
        private const int MaxRetryCount = 10;
        
        public static double GetTemperature(int pin, PinNumberingScheme? pinNumberingScheme)
        {
            var tryCounter = 0;
            double temperature;
            using var dht = new Dht11(pin, pinNumberingScheme ?? PinNumberingScheme.Logical);
            do
            {
                temperature = dht.Temperature.Celsius;
                tryCounter++;
            } while (!dht.IsLastReadSuccessful || tryCounter > MaxRetryCount);

            if (!dht.IsLastReadSuccessful)
            {
                throw new Exception($"Чтение данных с датчика не удалось. Кол-во попыток {tryCounter}.");
            }

            return temperature;
        }
        
        public static double GetHumidity(int pin, PinNumberingScheme? pinNumberingScheme)
        {
            var tryCounter = 0;
            double humidity;
            using var dht = new Dht11(pin, pinNumberingScheme  ?? PinNumberingScheme.Logical);
            do
            {
                humidity = dht.Humidity;
                tryCounter++;
            } while (!dht.IsLastReadSuccessful || tryCounter > MaxRetryCount);

            if (!dht.IsLastReadSuccessful)
            {
                throw new Exception($"Чтение данных с датчика не удалось. Кол-во попыток {tryCounter}.");
            }

            return humidity;
        }
        
        public static (double temperature, double humidity) GetData(int pin, PinNumberingScheme? pinNumberingScheme)
        {
            var tryCounter = 0;
            double temperature;
            double humidity;
            using var dht = new Dht11(pin, pinNumberingScheme ?? PinNumberingScheme.Logical);
            do
            {
                temperature = dht.Temperature.Celsius;
                humidity = dht.Humidity;
                tryCounter++;
            } while (!dht.IsLastReadSuccessful || tryCounter > MaxRetryCount);

            if (!dht.IsLastReadSuccessful || double.IsNaN(temperature) && double.IsNaN(humidity))
            {
                throw new Exception($"Чтение данных с датчика не удалось. Кол-во попыток {tryCounter}.");
            }

            return (temperature, humidity);
        }
    }
}