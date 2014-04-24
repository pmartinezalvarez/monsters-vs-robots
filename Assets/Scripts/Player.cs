using System.Collections;
using System.Collections.Generic;

public class Player
{
		private long model;
		private int strength;
		private int health;
		private List<string> abilityKeys = new List<string> ();
		private Dictionary<string,Ability> abilities = new Dictionary<string, Ability> ();

		public long Model {
				get{ return model; }
				set { this.model = value; }
		}

		public int Strength {
				get{ return strength; }
				set{ this.strength = value; }
		}

		public int Health {
				get{ return health; }
				set { this.health = value; }
		}

		public List<string> AbilityKeys {
				get{ return abilityKeys; }
				set { this.abilityKeys = value; }
		}

		public Dictionary<string,Ability> Abilities {
				get{ return abilities; }
				set{ this.abilities = value; }
		}
}
