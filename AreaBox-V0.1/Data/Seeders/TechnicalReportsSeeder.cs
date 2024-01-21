using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AreaBox_V0._1.Data.Seeders
{
	public class TechnicalReportsSeeder : ISeeder
	{
		public async Task SeedAsync(AreaBoxDbContext dbContext, ILogger logger)
		{
			if (dbContext.TechnicalReports.Any())
			{
				return; 
			}

			var users = dbContext.Users.ToList();

			var initialReports = new List<TechnicalReports>
			{
				new TechnicalReports
				{
					Type = "Issue",
					Details = "Something is not working",
					UserId = users.FirstOrDefault()?.Id,
					UserEmail = users.FirstOrDefault()?.Email
				},
				new TechnicalReports
				{
					Type = "Bug",
					Details = "Application crashes on startup",
					UserId = users.LastOrDefault()?.Id,
					UserEmail = users.LastOrDefault()?.Email
				},
            };

			dbContext.TechnicalReports.AddRange(initialReports);
			await dbContext.SaveChangesAsync();
		}
	}
}
