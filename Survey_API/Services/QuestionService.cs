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
    public class QuestionService
    {
        private readonly IMongoCollection<Question> questions;
        public QuestionService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("Survey_DB"));

            var database = client.GetDatabase("Survey_DB");

            questions = database.GetCollection<Question>("Questions");
        }

        public List<Question> GetQuestions()
        {
            return questions.Find(question => true).ToList();
        }

        public Question CreateQuestion(Question question)
        {
            questions.InsertOne(question);

            return question;
        }
    }
}
