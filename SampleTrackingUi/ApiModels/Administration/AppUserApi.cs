﻿namespace SampleTrackingUi.ApiModels.Administration
{
    public class AppUserApi
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
