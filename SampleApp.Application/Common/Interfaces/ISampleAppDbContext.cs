using SampleApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Application
{
	public interface ISampleAppDbContext
	{
		#region Entities
        
		DbSet<Hero> Heros { get; set; }         
		DbSet<HeroSkill> HeroSkills { get; set; }         
		DbSet<Skill> Skills { get; set; }
		#endregion

		
		EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;
		
		//EntityEntry Entry([NotNull] object entity);
		//DatabaseFacade Database { get; }        
	}
}