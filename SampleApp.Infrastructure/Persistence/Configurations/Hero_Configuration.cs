using SampleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Infrastructure.Persistence.Configurations
{
	public class Hero_Configuration : BaseConfiguration<Hero>
	{
        public override void ConfigureProperty(BasePropertyBuilder<Hero> builder)
		{
			builder.Property(a => a.Name)
				.HasMaxLength(ConfigConstants.NameMaxLength)
				.IsRequired();
		}
    }
}