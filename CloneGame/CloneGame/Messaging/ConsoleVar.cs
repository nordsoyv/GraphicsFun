﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloneGame.Messaging
{
	public class ConsoleVar
	{
		public string Name { get; set; }

		public string Value {get; set; }

		public const string FOV = "g_fov";
		public const string LANDSCAPE_SEED = "l_seed";

	}


}
