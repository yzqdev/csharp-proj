using System.Collections.Generic;

namespace ChromeUpdater.Model {
    public class ChromeUpdate
    {
        public AppUpdateWithArch Stable { get; set; }
        public AppUpdateWithArch Beta { get; set; }
        public AppUpdateWithArch Dev { get; set; }
        public AppUpdateWithArch Canary { get; set; }

        public ChromeUpdate(AppUpdateWithArch stable, AppUpdateWithArch beta, AppUpdateWithArch dev, AppUpdateWithArch canary) {
            Stable = stable;
            Beta = beta;
            Dev = dev;
            Canary = canary;
        }

        public ChromeUpdate() {
        }

        public Dictionary<string, AppUpdateWithArch> ToDictionary()
        {
            var dc = new Dictionary<string, AppUpdateWithArch>();
            if (Stable != null)
                dc.Add("Stable", Stable);
            if (Beta != null)
                dc.Add("Beta", Beta);
            if (Dev != null)
                dc.Add("Dev", Dev);
            if (Canary != null)
                dc.Add("Canary", Canary);
            return dc;
        }
        public AppUpdate GetUpdate(string branch, bool isX64)
        {
            switch (branch)
            {
                case "Stable":
                    return isX64 ? Stable.x64 : Stable.x86;
                case "Beta":
                    return isX64 ? Beta.x64 : Beta.x86;
                case "Dev":
                    return isX64 ? Dev.x64 : Dev.x86;
                case "Canary":
                    return isX64 ? Canary.x64 : Canary.x86;
            }
            return null;
        }
        public AppUpdate GetUpdate(Branch branch, bool isX64)
        {
            switch (branch)
            {
                case Branch.Stable:
                    return isX64 ? Stable.x64 : Stable.x86;
                case Branch.Beta:
                    return isX64 ? Beta.x64 : Beta.x86;
                case Branch.Dev:
                    return isX64 ? Dev.x64 : Dev.x86;
                case Branch.Canary:
                    return isX64 ? Canary.x64 : Canary.x86;
            }
            return null;
        }
    }


    
    public class AppUpdateWithArch
    {
        public AppUpdateWithArch() {
        }

        public AppUpdateWithArch(AppUpdate x64, AppUpdate x86) {
            this.x64 = x64;
            this.x86 = x86;
        }

        public AppUpdate x64 { get; set; }
        public AppUpdate x86 { get; set; }
    }

    public class AppUpdate
    {
        public AppUpdate( ) {
          
        }

        public AppUpdate(string[] url, long size, decimal time, string version, string name, string cdn, string sha1, string sha256) {
            this.url = url;
            this.size = size;
            this.time = time;
            this.version = version;
            this.name = name;
            this.cdn = cdn;
            this.sha1 = sha1;
            this.sha256 = sha256;
        }

        public string[] url { get; set; }
        public long size { get; set; }
        public decimal time { get; set; }
        public string version { get; set; }
        public string name { get; set; }
        public string cdn { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }

        public override string ToString()
        {
            return $"版本号{version}，大小{size}字节(≈{size / 1024 / 1024}MB);";
        }
    }
}
