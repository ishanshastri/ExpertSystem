﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertSystem
    {
    interface ITest
        {
        State TestState();
        IGenericFactAndObservation GetFact();
        }
    }
