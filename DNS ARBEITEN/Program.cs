using DNS.Client;
using DNS.Protocol;
using DNS.Protocol.ResourceRecords;
using DNS.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace DNS_ARBEITEN
{
	class Program
	{
		static async System.Threading.Tasks.Task Main(string[] args)
		{
			await ServerAsync();
		}

		static async System.Threading.Tasks.Task ServerAsync()
		{
			// Proxy to google's DNS
			MasterFile masterFile = new MasterFile();
			DnsServer server = new DnsServer(masterFile, "8.8.8.8");

			// Resolve these domain to localhost
			masterFile.AddIPAddressResourceRecord("github.com", "127.0.0.1");

			// Log every request
			server.Requested += (sender, e) => Console.WriteLine(e.Request);
			// On every successful request log the request and the response
			server.Responded += (sender, e) => Console.WriteLine("{0} => {1}", e.Request, e.Response);
			// Log errors
			server.Errored += (sender, e) => Console.WriteLine(e.Exception.Message);
			
			// Start the server (by default it listens on port 53)
			await server.Listen();
			//ЧЫРЫК-ЧЫРЫК
		}
	}
}
