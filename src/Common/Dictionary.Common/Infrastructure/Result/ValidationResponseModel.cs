﻿using System.Text.Json.Serialization;

namespace Dictionary.Common.Infrastructure.Result
{
    public class ValidationResponseModel
    {  
        public IEnumerable<string> Errors { get; set; }

        public ValidationResponseModel(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public ValidationResponseModel(string message) : this (new List<string> { message })
        {

        }

        [JsonIgnore]
        public string FlattenErrors => Errors != null ? string.Join(Environment.NewLine, Errors) : string.Empty;
    }
}
