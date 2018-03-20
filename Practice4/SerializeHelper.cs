using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Practice4
{
    static class SerializeHelper
    {
        internal static void Serialize<TObject>(TObject obj, string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fs, obj);
            }
        }

        internal static TObject Deserialize<TObject>(string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return (TObject) formatter.Deserialize(fs);
            }
        }
    }
}
