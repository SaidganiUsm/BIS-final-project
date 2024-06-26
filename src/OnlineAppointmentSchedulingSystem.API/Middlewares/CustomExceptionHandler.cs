﻿using FluentValidation;
using Newtonsoft.Json;
using System.Net;

namespace OnlineAppointmentSchedulingSystem.API.Middlewares
{
	public class CustomExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

		public CustomExceptionHandlerMiddleware(
			RequestDelegate next,
			ILogger<CustomExceptionHandlerMiddleware> logger
		)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"An unhandled exception has occurred while executing the request"
				);
				await HandleExceptionAsync(context, ex);
			}
		}

		/// <summary>
		/// This method is used to handle exceptions that occur during the execution of the request.
		/// It returns a JSON response with the error message and the HTTP status code.
		/// </summary>
		/// <param name="context">
		/// The HttpContext object that encapsulates all HTTP-specific information about an individual HTTP request.
		/// </param>
		/// <param name="exception"
		/// The exception that occurred during the execution of the request.
		/// </param>
		/// <returns>
		/// A task that represents the asynchronous exception handling.
		/// </returns>
		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var code = HttpStatusCode.InternalServerError; // 500 if unexpected

			object? errorObject = null;

			if (exception is ArgumentException)
			{
				code = HttpStatusCode.BadRequest; // 400 if bad request
			}
			else if (exception is ValidationException validationException)
			{
				code = HttpStatusCode.BadRequest;

				errorObject = new { errors = validationException.Errors };
			}
			else if (exception is InvalidOperationException)
			{
				code = HttpStatusCode.BadRequest;
			}

			var result = JsonConvert.SerializeObject(
				errorObject ?? new { errors = exception.Message }
			);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;
			return context.Response.WriteAsync(result);
		}
	}

	public static class ExceptionMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomExceptionHandler(
			this IApplicationBuilder builder
		)
		{
			return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
		}
	}
}
