using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Updater
{
    class updater
    {

        private frmLauncher launcher;
        
        private string FileName;
        private bool started;

        private List<string> toUpdate = new List<string>();

        public updater(frmLauncher launcher)
        {
            this.launcher = launcher;
            started = false;
            CheckForUpdates();
        }

        public void StartUpdater()
        {
            
            launcher.UpdateStatus("Checking patches and updates...");
        }

        public void downloader_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            launcher.UpdateStatusBar((float)((e.ProgressPercentage) / 100f));
            launcher.UpdateStatus("Downloading: " + FileName + " (" + e.BytesReceived + "/" + e.TotalBytesToReceive + ") " + e.ProgressPercentage + "%");
        }

        public void downloader_completed(object sender, AsyncCompletedEventArgs e)
        {
            downloadItem();
        }

        public byte[] generateCheckSum(string file)
        {
            using (MD5 md5 = MD5.Create())
            {
                if (!Directory.Exists(Application.StartupPath + @"\" + file)) Directory.CreateDirectory(Path.GetDirectoryName((Application.StartupPath + @"\" + file)));
                using (var stream = File.OpenRead(Application.StartupPath + @"\" + file))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

        private string generateHex(byte[] hash)
        {
            StringBuilder result = new StringBuilder(hash.Length * 2);
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString());
            }

            return result.ToString();
        }

        public void CheckForUpdates()
        {
            WebClient downloader = new WebClient();
            config configuration = new config();
            launcher.UpdateStatus("Reading patch information...");
            downloader.DownloadFile(new Uri(configuration.GetVariable("UpdateURL") + "/updates.json"), @"launcher\updates.json");
           
            //Scan the downloaded Updates.json
            using (StreamReader file = File.OpenText(@"launcher\updates.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                
                JArray token = (JArray)JToken.ReadFrom(reader);
                foreach(JObject obj in token.Children<JObject>())
                {
                    string filename = (string)obj["filename"];
                    string checkSum = (string)obj["checksum"];
                    

                    if (!Directory.Exists(Application.StartupPath + @"\" + file)) Directory.CreateDirectory(Path.GetDirectoryName((Application.StartupPath + @"\" + file)));
                    if (!File.Exists(Application.StartupPath + @"\" + filename)) {
                        toUpdate.Add(filename);
                    } else if (checkSum != generateHex(generateCheckSum(filename))){
                        toUpdate.Add(filename);
                    }
                }
            }

            downloadItem();
        }

        private void downloadItem()
        {
            config configuration = new config();

            if (toUpdate.Count > 0)
            {
                foreach (string filename in toUpdate)
                {
                    WebClient downloader = new WebClient();
                    downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(downloader_completed);
                    downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloader_ProgressChanged);
                    toUpdate.Remove(filename);
                    string download = filename.Replace(@"\", @"/");
                    downloader.DownloadFileAsync(new Uri(configuration.GetVariable("UpdateURL") + "/" + download), Application.StartupPath + @"\" + filename);
                    return;
                }
            }

            launcher.UpdateStatus("Patch completed, starting game...");
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = configuration.GetVariable("Application");

            Process.Start(start);
            Application.Exit();
        }
    }
}
