using SampleApp.Domain.Entities;
using SampleApp.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

namespace SampleApp.Infrastructure.Persistence
{
	public class SampleAppDbContext : DbContext, ISampleAppDbContext
	{
		#region Entities

		public DbSet<Hero> Heros {get;set;} 
		public DbSet<HeroSkill> HeroSkills {get;set;} 
		public DbSet<Skill> Skills {get;set;}
		#endregion


		public SampleAppDbContext(DbContextOptions<SampleAppDbContext> dbContextOpt) : base(dbContextOpt)
		{

		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}