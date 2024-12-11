using System.ComponentModel;

namespace iLearn.Common.Enums
{
    public enum Roles
    {
        [Description("Unknown")]
        Unknown = 0,

        [Description("User")]
        User = 1,

        [Description("Instructor")]
        Instructor = 2
    }
}
