namespace StrengthJournal.Server.Models
{
    public class AppViewModel
    {
        protected const string appDistBase = "dist";

        protected readonly string runtimeHash;
        protected readonly string polyfillsHash;
        protected readonly string mainHash;
        protected readonly string stylesHash;

        public string runtimeUrl { get => $"/{appDistBase}/runtime.{runtimeHash}.js"; }
        public string polyfillsUrl { get => $"/{appDistBase}/polyfills.{polyfillsHash}.js"; }
        public string mainUrl { get => $"/{appDistBase}/main.{mainHash}.js"; }
        public string stylesUrl { get => $"/{appDistBase}/styles.{stylesHash}.css"; }

        public AppViewModel(string runtimehash, string polyfillsHash, string mainHash, string stylesHash)
        {
            this.runtimeHash = runtimehash;
            this.polyfillsHash = polyfillsHash;
            this.mainHash = mainHash;
            this.stylesHash = stylesHash;
        }
    }
}