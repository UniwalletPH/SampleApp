using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Domain.Entities
{
    public class HeroSkill
    {
        public Hero Hero { get; set; }
        public Skill Skill { get; set; }
    }
}
