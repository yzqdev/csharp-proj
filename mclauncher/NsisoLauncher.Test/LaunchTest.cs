using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NsisoLauncherCore;
using NsisoLauncherCore.Util;
using NsisoLauncherCore.Net;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace NsisoLauncher.Test
{
    [TestClass]
    public class LaunchTest
    {
        //LaunchHandler launchHandler;

        //public LaunchTest()
        //{
        //    CreateLaunchHandler();
        //}

        //public void CreateLaunchHandler()
        //{
        //    launchHandler = new LaunchHandler(".minecraft/", Java.GetSuitableJava(), true);
        //}

        //[TestMethod]
        //public async void TestGetVersions()
        //{
        //    //NsisoLauncherCore.Net.FunctionAPI.FunctionAPIHandler api = new NsisoLauncherCore.Net.FunctionAPI.FunctionAPIHandler(DownloadSource.Mojang);
        //    //var verList = (api.GetVersionList()).Result;
        //    //VersionReader versionReader = new VersionReader(launchHandler);
        //    //var vers = versionReader.GetVersions();
        //    //Assert.IsFalse(vers.Count == 0);
        //}

        public async static Task<string> HttpGetStringAsync(string uri) {

            try {
                var httpClientHandler = new HttpClientHandler {
                    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
                };
                using HttpClient client = new(httpClientHandler);
                return await client.GetStringAsync(uri);
            } catch (TaskCanceledException) {
                return null;
            }
        }
        [Test]
        public void GetJson() {
            Console.WriteLine("test http");
            var url = "https://bmclapi2.bangbang93.com/mc/game/version_manifest.json";
            var result = HttpGetStringAsync(url).GetAwaiter().GetResult();
            Console.WriteLine(result);
            Console.ReadKey();
        }


    }
}
