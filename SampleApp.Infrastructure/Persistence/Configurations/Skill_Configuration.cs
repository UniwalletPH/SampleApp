using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Infrastructure.Persistence.Configurations
{
	public class Skill_Configuration : BaseConfiguration<Skill>
	{
		public override void ConfigureProperty(BasePropertyBuilder<Skill> builder)
		{
			builder.Property(a => a.Name)
				.HasMaxLength(ConfigConstants.NameMaxLength)
				.IsRequired();

			builder.Property(a => a.Description)
				.HasMaxLength(ConfigConstants.DescriptionMaxLength);

			builder.Property(a => a.SkillType)
				.HasConversion<string>()
				.HasMaxLength(ConfigConstants.EnumMaxLength);
		}
	}
}