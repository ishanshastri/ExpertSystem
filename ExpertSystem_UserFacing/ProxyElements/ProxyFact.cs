﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExpertSystem_2
    {
    [Serializable]
    [XmlType("fact")]
    public class ProxyFact : ProxyElement, IProxy// : ProxyFactObservation
        {
        #region Common Properties

        /// <summary>
        /// The identifier of the fact
        /// </summary>
       // [XmlAttribute]
        //public string ID;

        /// <summary>
        /// The type of the fact
        /// </summary>
        [XmlAttribute]
        public string type;//FactObsType type;

        /// <summary>
        /// A brief, general description of the fact
        /// </summary>
        [XmlElement]
        public string description;
        #endregion
        #region Type-Specific Properties
        /// <summary>
        /// The units of the fact; applies to numeric fact types
        /// </summary>
        [XmlAttribute]
        public string units;

        /// <summary>
        /// The description given if the fact value is true; applies to boolean facts
        /// </summary>
        [XmlAttribute]
        public string trueDescription;

        /// <summary>
        /// The description given if the fact value is false; applies to boolean facts
        /// </summary>
        [XmlAttribute]
        public string falseDescription;

        #endregion

        #region Fields

        /// <summary>
        /// The signature
        /// </summary>
        private const string Signature = "pif";

        /// <summary>
        /// The version   
        /// </summary>
        private const int Version = 1;

        #endregion

        /// <summary>
        /// Syntactically validates the proxy object.
        /// </summary>
        /// <returns></returns>
        public ValidationResult Validate()
            {
            ValidationResult result = new ValidationResult();
            return result;
            }
        }
    }
