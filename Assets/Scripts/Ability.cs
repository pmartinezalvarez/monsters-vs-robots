using System;

public class Ability
{
		private int count;
		private string name;
		private int value;

		public Ability ()
		{
		}

		public Ability (string name, int value)
		{
				this.name = name;
				this.value = value;
				this.count = 0;
		}

		public Ability (string name, int value, int count)
		{
				this.name = name;
				this.value = value;
				this.count = count;
		}

		public int Count {
				get{ return count; }
				set{ count = value; }
		}

		public string Name {
				get{ return name; }
				set{ name = value; }
		}

		public int Value {
				get{ return value; }
				set{ this.value = value; }
		}
}
