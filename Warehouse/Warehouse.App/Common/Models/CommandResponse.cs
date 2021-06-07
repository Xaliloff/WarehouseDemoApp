using System.Collections.Generic;
using System.Linq;

namespace Warehouse.App
{
    public class CommandResponse<T> : CommandResponse
    {
        public CommandResponse()
        {
        }

        public CommandResponse(T body)
        {
            Body = body;
        }

        public CommandResponse(IEnumerable<CommandError> errors) : base(errors)
        {
        }

        public T Body { get; set; }
    }

    public class CommandResponse
    {
        public CommandResponse()
        {
        }

        public CommandResponse(IEnumerable<CommandError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<CommandError> Errors { get; set; } = new List<CommandError>();

        public bool HasErrors
        {
            get
            {
                return Errors.Count() > 0;
            }
        }
    }
}