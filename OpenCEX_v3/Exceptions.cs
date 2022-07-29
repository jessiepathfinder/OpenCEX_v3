﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenCEX
{
	public sealed class UserError : Exception{
		public readonly int code;
		private UserError(string reason, int code) : base(reason)
		{
			this.code = code;
		}
		//maxcode: 11
		public static void Throw(string reason, int code){
			throw new UserError(reason, code);
		}
	}

	public sealed class OptimisticRepeatException : Exception{
		private readonly Task cleanUp;

		private OptimisticRepeatException(Task cleanUp)
		{
			this.cleanUp = cleanUp;
		}

		public async Task WaitCleanUp(){
			if(cleanUp is { }){
				await cleanUp;
			}
		}

		public static void Throw(Task cleanUp){
			throw new OptimisticRepeatException(cleanUp);
		}
	}

	public sealed class CacheMissException : Exception
	{
		private CacheMissException(){
			
		}

		public static void Throw(){
			throw new CacheMissException();
		}
	}
}