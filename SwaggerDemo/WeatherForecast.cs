using System;
using System.ComponentModel.DataAnnotations;

namespace SwaggerDemo
{
    /* SWAGGER-HOWTO: add XML comments in models for detailed descriptions in swagger UI */
    /// <summary>
    /// Weather forecast info
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Date of the forecast
        /// </summary>
        /// <example>2020-08-30</example>
         /* SWAGGER-HOWTO: add DataAnnotations to restrict property access and format */
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperature in Celsius for the day
        /// </summary>
        /// <example>28</example>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Temperature in Fahrenheit for the day
        /// </summary>
        /// <example>82,4</example>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Description of the day's forecast
        /// </summary>
        /// <example>Warm</example>
        public string Summary { get; set; }
    }
}