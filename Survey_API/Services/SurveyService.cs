using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Survey_API.Database;
using Survey_API.Models;
using MongoDB.Bson;

namespace Survey_API.Services
{
    public class SurveyService
    {
        private readonly IMongoCollection<Survey> surveys;
        public SurveyService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("Survey_DB"));

            var database = client.GetDatabase("Survey_DB");

            surveys = database.GetCollection<Survey>("Surveys");
        }

        public List<Survey> GetSurveys()
        {
            return surveys.Find(survey => true).ToList();
        }

        public Survey CreateSurvey(Survey survey)
        {
            surveys.InsertOne(survey);

            return survey;
        }
    }
}
