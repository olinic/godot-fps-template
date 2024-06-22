using static GdUnit4.Assertions;
using System;
using Godot;
using GdUnit4;
using System.Threading.Tasks;

namespace TestUtils
{
    

    public class SignalTester
    {
        private readonly string _signalName;
        private readonly Action _action;

        public SignalTester(string signalName, Action action)
        {
            _signalName = signalName;
            _action = action;
        }

        public static SignalTester AssertSignalEmitted(String signalName, Action action)
        {
            return new SignalTester(signalName, action);
        }

        public async Task On(GodotObject emitter)
        {
            await AssertSignal(emitter).IsNotEmitted(_signalName).WithTimeout(10);
            _action();
            await AssertSignal(emitter).IsEmitted(_signalName).WithTimeout(100);
        }
    }
}