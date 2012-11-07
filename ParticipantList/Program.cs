using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParticipantList.ETOResults.QaaWS.Samples.Participants;

namespace ParticipantList
{
	/// <summary>
	/// Prints a list of Participants in the specified site retrieved via ETO Results QaaWS
	/// </summary>
	class Program
	{
		/// <summary>
		/// Prints the command line params/usage to the console
		/// </summary>
		private static void showUsage()
		{
			Console.WriteLine("ERROR: invalid arguments\n\n");
			Console.WriteLine("Usage:\n\tParticipantList <username> <password> [site]\n\n");
			Console.WriteLine("Press [ENTER] to exit.");
			Console.ReadLine();
		}
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
			string site = string.Empty;

			// Verify that username & password are supplied
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
			{
				showUsage();
				return;
			}

			if (args.Length > 2)
				site = args[2];

			// Return Values
			string msg;
			int fetchedrows;
			int queryruntime = 0;
			string universe, creatorname;
			string description;
			string creationdateformatted;
			DateTime createdate;
			bool delegated, partialResult;


			QaaWSHeader header = new ETOResults.QaaWS.Samples.Participants.QaaWSHeader();
			QueryAsAServiceSoapClient client = new ETOResults.QaaWS.Samples.Participants.QueryAsAServiceSoapClient();

			// Site not specified, present the user with options
			if (string.IsNullOrEmpty(site))
			{
				Console.WriteLine("Obtaining a list of Sites for the prompt on the query...");
				var lovRequest = new ETOResults.QaaWS.Samples.Participants.valuesOf_Site_Name();
				lovRequest.login = username;
				lovRequest.password = password;

				var lovValues = client.valuesOf_Site_Name(header, lovRequest, out msg, out delegated, out partialResult);
				if (!string.IsNullOrEmpty(msg)) Console.WriteLine(@"Message: {0}", msg);
				foreach (var lov in lovValues)
				{
					Console.WriteLine("\t{0}\t{1}", lov.index, lov.value);
				}
				Console.Write("\nEnter value for [Site]: ");
				site = Console.ReadLine().Trim();
			}

			Console.WriteLine("Obtaining a list of Participants in site \"{0}\" from ETO Results via QaaWS", site);
			msg = string.Empty;

			// Build Request/Query/Soap Client
			runQueryAsAService requestParticipants = new ETOResults.QaaWS.Samples.Participants.runQueryAsAService();
			requestParticipants.login = username;
			requestParticipants.password = password;
			requestParticipants.Site = site;


			// Execute the query
			Row[] rows = client.runQueryAsAService(header, requestParticipants
				, out msg, out creatorname, out createdate, out creationdateformatted
				, out description, out universe, out queryruntime, out fetchedrows);

			Console.WriteLine(@"Retrieved {0} rows in {1} seconds.", fetchedrows, queryruntime);

			// If msg contains "Enterprise authentication could not log you on.." you have specified an incorrect username/password.
			if (!string.IsNullOrEmpty(msg)) Console.WriteLine(@"Message: {0}", msg);


			// Print the results to the console
			foreach (Row row in rows)
			{
				Console.WriteLine("\t{0}\t{1}\t{2}", row.Participant_Site_Identifier, row.Case_Number, row.Name);

			}

			Console.WriteLine("Press [ENTER] to exit.");
			Console.ReadLine();
		}
	}
}
