using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Core.Utilities.EmailStampModel
{
	public class EmailStamp
	{
		public string Host { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string FromEmail { get; set; }
		public int Port { get; set; }
		public bool EnableSsl { get; set; }
	}
}
