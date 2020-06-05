using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Domain.Entities
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsUltimate { get; set; }
        public int ManaCost { get; set; }
        public SkillType SkillType { get; set; }
    }
}
