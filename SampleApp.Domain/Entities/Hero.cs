using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Domain.Entities
{
    public class Hero
    {
        public string Name { get; set; }

        public int Health { get; set; }
        public int Mana { get; set; }


        public ICollection<HeroSkill> Skills { get; private set; } = new HashSet<HeroSkill>();
    }
}
