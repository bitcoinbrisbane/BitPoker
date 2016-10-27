using System;

namespace BitPoker.Models
{
	public interface IRequest
	{
        /// <summary>
        /// Id
        /// </summary>
        Guid Id { get; set; }

        //Decimal Version { get; set; }

        String Method { get; set; }

        String Signature { get; set; }

        Object Params { get; set; }
    }
}