using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace ExpertSystem
    {
     class ModifiableKnowledgeBase : KnowledgeBase, IKnowledgeBase
        {
        private KnowledgeBase _knowledgeBase;

        private string FileLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifiableKnowledgeBase"/> public class.
        /// </summary>
        public ModifiableKnowledgeBase(KnowledgeBase knowledgeBase, string fileLocation) : base()
            {
            this._knowledgeBase = knowledgeBase;
            this.FileLocation = fileLocation;//Maybe make protected property of KnowledgeBase?
            }



        /// <summary>
        /// Loads knowledge base from XML file (deserializes knowledge base).
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>the completed knowledge base</returns>
        /// <exception cref="NotImplementedException"></exception>
        public void LoadFromXmlFile()//Maybe make protected method of KnowledgeBase, then publicised here
            {
            XmlSerializer serializer = new XmlSerializer(this._knowledgeBase.GetType());
            try
                {
                using (FileStream fStream = File.Open(this.FileLocation, FileMode.Open))
                    {
                    this._knowledgeBase = (KnowledgeBase)serializer.Deserialize(fStream);
                    }
                }
            catch (Exception ex)
                {
                throw new Exception("Error loading from Xml File: " + ex.Message); 
                }
            }

        /// <summary>
        /// Saves to XML file (serializes knowledge base).
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void SaveToXmlFile(string filename)
            {
            throw new NotImplementedException();
            }
        }
    }
