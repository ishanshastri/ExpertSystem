using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem_2
    {
    interface IObservation
        {
        bool GetBoolValue();
        int GetIntValue();
        double GetDoubleValue();
        string GetStringValue();

        void SetValue(bool value);
        void SetValue(int value);
        void SetValue(double value);
        void SetValue(string value);
        }
    }
