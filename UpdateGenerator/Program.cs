using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace UpdateGenerator
{
    class Program
    {

        private static Dictionary<string, byte[]> objects = new Dictionary<string, byte[]>();
        private static List<string> ignoredFilenames = new List<string>();
        private static string currentDir = "";

        static void loadIgnoredFilenamesFromFile(string filename)
        {
            using (StreamReader reader = File.OpenText("ignored.json"))
            {
                string text = reader.ReadToEnd();
                ignoredFilenames = JsonConvert.DeserializeObject<List<string>>(text);
            }

        }

        static void Main(string[] args)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        byte[] checksum = generateCheckSum(d + @"\" + Path.GetFileName(f));
                        string filename = new DirectoryInfo(d).Name + @"\" + Path.GetFileName(f);

                        if (ignoredFilenames.Contains(filename))
                            continue;

                        objects.Add(filename, checksum);
                        searchDir(d);
                    }
                }

                foreach (string f in Directory.GetFiles(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)))
                {
                    byte[] checksum = generateCheckSum(Path.GetFileName(f));
                    string filename = Path.GetFileName(f);

                    if (ignoredFilenames.Contains(filename))
                        continue;

                    objects.Add(filename, checksum);
                }

                //Write the json file
                JArray arr = new JArray();

                foreach (string filename in objects.Keys)
                {
                    string checksum = generateHex(objects[filename]);

                    Console.WriteLine(filename + " => " + checksum);
                    arr.Add(new JObject(new JProperty("filename", filename), new JProperty("checksum", checksum)));
                }

                using(StreamWriter file = File.CreateText("updates.json"))
                using(JsonTextWriter writer = new JsonTextWriter(file))
                {
                    arr.WriteTo(writer);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt);
            }

            Console.WriteLine("Application Completed!");
            Console.ReadLine();
        }

        private static void searchDir(string directory)
        {
            currentDir += @"\" + new DirectoryInfo(directory).Name;

            Console.WriteLine("Currently in: " + currentDir);

            foreach (string d in Directory.GetDirectories(directory))
            {
                foreach (string f in Directory.GetFiles(d))
                {
                    
                    byte[] checksum = generateCheckSum(d + @"\" + Path.GetFileName(f));
                    string filename = currentDir + @"\" + new DirectoryInfo(d).Name + @"\" + Path.GetFileName(f);

                    objects.Add(filename, checksum);
                    searchDir(d);

                    currentDir = currentDir.Substring(0, currentDir.Length - (new DirectoryInfo(d).Name.Length));
                }
            }
        }

        private static string generateHex(byte[] hash)
        {
            StringBuilder result = new StringBuilder(hash.Length * 2);
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString());
            }

            return result.ToString();
        }

        private static byte[] generateCheckSum(string file)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }
    }
}
