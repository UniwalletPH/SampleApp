using SampleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Infrastructure.Persistence.Configurations
{
	public class HeroSkill_Configuration : BaseConfiguration<HeroSkill>
	{
		public override void KeyBuilder(BaseKeyBuilder<HeroSkill> builder)
		{
			builder.HasKey("HeroID", "SkillID");
		}

		public override void ConfigureProperty(BasePropertyBuilder<HeroSkill> builder)
		{
			builder.Property<int>("HeroID");
			builder.Property<int>("SkillID");
		}

        public override void ConfigureRelationship(BaseRelationshipBuilder<HeroSkill> builder)
        {
			builder.HasOne(a => a.Hero)
				.WithMany(a => a.Skills)
				.HasForeignKey("HeroID");

			builder.HasOne(a => a.Skill)
				.WithMany()
				.HasForeignKey("SkillID");
        }
    }
}