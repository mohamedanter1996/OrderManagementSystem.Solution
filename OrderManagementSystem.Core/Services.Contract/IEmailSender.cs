﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Services.Contract
{
	public interface IEmailSender
	{
		public Task SendEmailAsync(string toEmail, string subject, string body);
	}
}
