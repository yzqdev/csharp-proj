namespace NsisoCmd {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            GetJson();
        }
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
       
        public static void GetJson() {
            Console.WriteLine("test http");
            var url = "https://bmclapi2.bangbang93.com/mc/game/version_manifest.json";
            var result = HttpGetStringAsync(url).GetAwaiter().GetResult();
            Console.WriteLine(result);
            Console.ReadKey();
        }

    }
}