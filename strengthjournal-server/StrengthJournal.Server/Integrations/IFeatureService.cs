﻿namespace StrengthJournal.Server.Integrations
{
    public interface IFeatureService
    {
        public Task<ICollection<string>> GetFeatures();
        public Task<bool> HasFeature(string featureName);
    }
}