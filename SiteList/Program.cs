using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiteList.ETOResults.QaaWS.Samples.Sites;

namespace SiteList
{
	/// <summary>
	/// Console Application to print a list of sites retrieved by ETO Results QaaWS
	/// </summary>
	class Program
	{
		/// <summary>
		/// Prints the command line params/usage to the console
		/// </summary>
		private static void showUsage()
		{
			Console.WriteLine("ERROR: invalid arguments\n\n");
			Console.WriteLine("Usage:\n\tSiteList <username> <password>\n\n");
			Console.WriteLine("Press [ENTER] to exit.");
			Console.ReadLine();
		}

		/// <summary>
		/// Main command line method
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				showUsage();
				return;
			}

			// Read the username and password from the command line arguments
			string username = args[0];
			string password = args[1];

			// Verify that username & password are supplied
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
			{
				showUsage();
				return;
			}

			Console.WriteLine(@"Obtaining a list of Sites from ETO Results via QaaWS");

			// Return Values
            string msg;
            int fetchedrows;
            int queryruntime = 0;
            string universe, creatorname;
            string description;
            string creationdateformatted;
            DateTime createdate;

			// Build Request/Query/Soap Client
			QaaWSHeader header = new ETOResults.QaaWS.Samples.Sites.QaaWSHeader();

			runQueryAsAService query = new ETOResults.QaaWS.Samples.Sites.runQueryAsAService();
			query.login = username;
			query.password = password;

			QueryAsAServiceSoapClient client = new ETOResults.QaaWS.Samples.Sites.QueryAsAServiceSoapClient();

			// Execute the query
			Row[] rows = client.runQueryAsAService(header, query
				, out msg, out creatorname, out createdate, out creationdateformatted
				, out description, out universe, out queryruntime, out fetchedrows);

			Console.WriteLine(@"Retrieved {0} rows in {1} seconds.", fetchedrows, queryruntime);
			
			// If msg contains "Enterprise authentication could not log you on.." you have specified an incorrect username/password.
			if (!string.IsNullOrEmpty(msg)) Console.WriteLine(@"Message: {0}", msg);


			// Print the results to the console
			foreach (Row row in rows)
			{
				Console.WriteLine("\t{0}", row.Site_Name);
			}

			Console.WriteLine("Press [ENTER] to exit.");
			Console.ReadLine();
		}
	}
}
