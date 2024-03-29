﻿namespace Dictionary.Common.Models.QueriesModels
{
    public class UserDetailViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
