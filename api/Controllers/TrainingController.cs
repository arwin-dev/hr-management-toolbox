﻿using hr_app.api.DataContext;
using hr_app.api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hr_app.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlManager _sqlManager;

        public TrainingController(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlManager = new MySqlManager(_configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public async Task<IActionResult> GetTrainingDetails(string empID)
        {
            string query = "SELECT t.Training_ID, t.Name FROM training t WHERE t.Training_ID NOT IN (SELECT et.Training_ID FROM employee_training et WHERE et.Employee_ID = @empID);";

            var parameters = new Dictionary<string, object>
            {
                { "@empID", empID }
            };

            var trainings = await _sqlManager.GetDataTableAsync(query,parameters);

            if (trainings.Rows.Count == 0)
            {
                return NotFound();
            }

            var serializedData = JsonConvert.SerializeObject(trainings);

            return Content(serializedData, "application/json");
        }

        [HttpGet("getTraining")]
        public async Task<IActionResult> GetAllTraining(string empID)
        {
            string query = "SELECT t.Name, t.Mode, et.Start_date, et.Completion_date, et.Score FROM training t, employee_training et WHERE t.Training_ID = et.Training_ID AND et.Employee_ID = @empID";
            var parameters = new Dictionary<string, object>
            {
                { "@empID", empID }
            };
            var trainings = await _sqlManager.GetDataTableAsync(query, parameters);

            if (trainings.Rows.Count == 0)
            {
                return NotFound();
            }

            var serializedData = JsonConvert.SerializeObject(trainings);

            return Content(serializedData, "application/json");
        }

        [HttpPost("createTraining")]
        public async Task<IActionResult> createTraining(Dictionary<string, object> trainingDict)
        {
            string query = "INSERT INTO training ( Name, Mode)  VALUES (@name, @mode)";

            var parameters = new Dictionary<string, object>()
            {
                {"@name", trainingDict["Name"]},
                {"@mode", trainingDict["Mode"]},
            };

            var result = await _sqlManager.ExecuteQueryAsync(query, parameters);

            if (result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTraining(Dictionary<string, object> trainingDict)
        {
            string query = "INSERT INTO employee_training (Start_date, Score, Employee_ID, Training_ID) " +
                           "VALUES (@Start_date, @Score, @Employee_ID, @Training_ID)";

            var parameters = new Dictionary<string, object>()
            {
                {"@Start_date", trainingDict["Start_date"]},
                {"@Score", -1 },
                {"@Employee_ID", trainingDict["Employee_ID"]},
                {"@Training_ID", trainingDict["Training_ID"]},
            };

            if (trainingDict["Completion_date"].ToString() != "")
            {
                query = "INSERT INTO employee_training (Start_date, Completion_date, Score, Employee_ID, Training_ID) " +
                           "VALUES (@Start_date, @Completion_date, @Score, @Employee_ID, @Training_ID)";
                parameters.Add("@Completion_date", trainingDict["Completion_date"]);
            }

            var result = await _sqlManager.ExecuteQueryAsync(query, parameters);

            if (result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
