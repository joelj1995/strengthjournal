namespace StrengthJournal.Server.Models
{
    public class AppViewModel
    {
        protected const string appDistBase = "dist";

        protected readonly string runtimeHash;
        protected readonly string polyfillsHash;
        protected readonly string mainHash;

        public string runtimeUrl { get => $"/{appDistBase}/runtime.{runtimeHash}.js"; }
        public string polyfillsUrl { get => $"/{appDistBase}/polyfills.{polyfillsHash}.js"; }
        public string mainUrl { get => $"/{appDistBase}/main.{mainHash}.js"; }

        public AppViewModel(string runtimehash, string polyfillsHash, string mainHash)
        {
            this.runtimeHash = runtimehash;
            this.polyfillsHash = polyfillsHash;
            this.mainHash = mainHash;
        }
    }
}