namespace StrengthJournal.Server
{
    public sealed class StrengthJournalConfiguration
    {
        private static readonly StrengthJournalConfiguration instance = new StrengthJournalConfiguration();

        public string SqlService_ConnectionString { get; private set; }

        private StrengthJournalConfiguration()
        {
            SqlService_ConnectionString = @"Server=localhost;Database=StrengthJournal;Trusted_Connection=True";
        }

        public static StrengthJournalConfiguration Instance
        {
            get => instance;
        }
    }
}
