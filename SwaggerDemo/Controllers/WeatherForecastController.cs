using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SwaggerDemo.Controllers
{
    /* SWAGGER-HOWTO: add XML comments to describe endpoints and classes */
    /// <summary>
    /// Returns weather forecasts
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    /* SWAGGER-HOWTO: add Produces and Consumes attributes to restrict formats in swagger UI */
    [Produces("application/json")]
    [Consumes("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new List<string>
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// Get the weather forecast for the next 5 days.
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="500">Server error occured</response>
        [HttpGet]
        /* SWAGGER-HOWTO: add response attributes for all possible response types to display them in swagger UI, 
         * additionally describe the responses in XML comments for more details */
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                var rng = new Random();
                return new JsonResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Count)]
                })
                .ToArray());
            }
            catch (Exception)
            {
                return new StatusCodeResult(statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get the weather forecast for a specific day.
        /// </summary>
        /// <param name="day" example="2020-08-30">The day for the forecast.</param>
        /// <response code="200">Success</response>
        /// <response code="500">Server error occured</response>
        [HttpGet("{day}")]
        [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDay(DateTime day)
        {
            try
            {
                var rng = new Random();
                return new JsonResult(new WeatherForecast
                {
                    Date = day.Date,
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Count)]
                });
            }
            catch (Exception)
            {
                return new StatusCodeResult(statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Add a new the weather forecast summary.
        /// </summary>
        /// <param name="summary" example="Cold">The forecast summary to add</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request, possibly the parameters are invalid</response>
        /// <response code="500">Server error occured</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /WeatherForecast
        ///     {
        ///        "summary": "Cold"
        ///     }
        ///
        /// </remarks>
        [HttpPost("{summary}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddSummary(string summary)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(summary))
                    throw new ArgumentNullException(nameof(summary));

                if (!Summaries.Contains(summary))
                    Summaries.Add(summary);

                return Ok();
            }
            catch (ArgumentNullException)
            {
                return new StatusCodeResult(statusCode: (int)HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return new StatusCodeResult(statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}