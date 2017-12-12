//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace Black.Beard.Oracle.Helpers
//{

//    public static class SerializerExtension
//    {

//        public static void Serialize<T>(this T self, string filename)
//        {
//            FileInfo file = new FileInfo(filename);
//            using (FileStream stream = file.OpenWrite())
//            {
//                string sz = JsonConvert.SerializeObject(self, Formatting.Indented);
//                byte[] ar = System.Text.Encoding.UTF8.GetBytes(sz);
//                stream.Write(ar, 0, ar.Length);
//            }
//        }

//        public static T Deserialize<T>( string filename)
//        {
//            FileInfo file = new FileInfo(filename);
//            using (StreamReader stream = file.OpenText())
//            {
//                T result = JsonConvert.DeserializeObject<T>(stream.ReadToEnd());
//                return result;
//            }
//        }

//    }
//}
