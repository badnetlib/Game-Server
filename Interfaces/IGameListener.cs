using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Interfaces
{
	public interface IGameListener
	{
		//Called after events has been polled, used to update game state
		void Update(int delta);
	}
}
