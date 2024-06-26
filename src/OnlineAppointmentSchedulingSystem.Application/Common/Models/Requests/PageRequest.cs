﻿using System.ComponentModel.DataAnnotations;

namespace OnlineAppointmentSchedulingSystem.Application.Common.Models.Requests
{
	public class PageRequest
	{
		[Range(0, int.MaxValue)]
		public int PageIndex { get; set; }

		[Range(1, int.MaxValue)]
		public int PageSize { get; set; } = 10;
	}
}
