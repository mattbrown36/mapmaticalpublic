using mlShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using System.Runtime.InteropServices;
using Google.Cloud.Storage.V1;
using System.Text;
using System.IO;

namespace mlShared
{
    public static class Util
    {

        private static SemaphoreSlim lockSettings = new SemaphoreSlim(1, 1);
        private static ConfigFile settings;

        public static async Task<FileInfo> DownloadFileFromBucketAsync(string bucketName, string objectName, string localFileName)
        {
            var client = await StorageClient.CreateAsync();
            using (var fs = File.OpenWrite(localFileName))
            {
                await client.DownloadObjectAsync(bucketName, objectName, fs);
            }
            return new FileInfo(localFileName);
        }

        public static string GetSetting(string key) {
            return GetSettingAsync(key).Result;
        }

        public static async Task<string> GetSettingAsync(string key)
        {
            Func<Task> initProduction = async () =>
            {
                var localFileName = "./mlsettings.txt";
                await Util.DownloadFileFromBucketAsync("mapmatical-secrets", "mlsettings.txt", localFileName);
                settings = new ConfigFile(localFileName, throwIfNotExists: true);
                try
                {
                    //Delete the secrets off the server just in case it's compromised at some point.
                    //We're still in trouble if they do a memory dump, but at least the settings aren't
                    //just sitting there.
                    if (File.Exists("./mlsettings.txt"))
                        File.Delete(localFileName);
                }
                catch { }
            };

            Action initDebug = () =>
            {
                var isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                if (isWindows)
                {
                    //Wanted to put this in the user folder, but have to wait for .net standard 1.7 for that.
                    settings = new ConfigFile(@"\mapmatical\mlsettings.txt", throwIfNotExists: false);
                }
                else
                {
                    //Is docker:
                    //If we're running from docker in debug mode (which isn't done all that often),
                    //then we assume that mlsettings.txt was copied into the container. This can probably
                    //be accomplished by just putting the file into the debug output folder. Haven't tested yet.
                    settings = new ConfigFile("./mlsettings.txt", throwIfNotExists: true);
                }
            };

            await lockSettings.WaitAsync();
            try
            {
                if (settings == null)
                {
#if DEBUG
                    initDebug();
#else
                    await initProduction();
#endif 
                }
            }
            finally
            {
                lockSettings.Release();
            }

            return settings.GetValue(key);
        }

        public static void LogException(string title, Exception ex)
        {
            //TODO
        }

    }
}
