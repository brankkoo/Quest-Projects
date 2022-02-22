using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace QuestProject
{
    class SaveInfo
    {
        //making a method that serializes the data to xml and saves it to the directory of the project
        public static void saveinfo (object obj, string filename)
        {
            //making a new xml serializer object and passing the obj as object parametar also calling the method GetType to return the data from runtime
            XmlSerializer xml = new XmlSerializer(obj.GetType());
            //making a new file writer object
            TextWriter writer = new StreamWriter(filename);
            //calling xml serializer method
            xml.Serialize(writer, obj);
            //closes the writer
            writer.Close();
        }
    }
}
