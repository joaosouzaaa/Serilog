using System.ComponentModel;

namespace SerilogAPI.Enums;

public enum ELogs 
{
    [Description("{0} is Invalid.")]
    Invalid,

    [Description("{0} was not found.")]
    NotFound
}
