using Game1.Code.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Code.Components.Interactions
{
	abstract class Interaction : Component
	{
		public override ComponentType ComponentType
		{
			get { return ComponentType.Interaction; }
		}

		public abstract void action();
		public virtual void Reload(Entities entities) { }

	}
}
