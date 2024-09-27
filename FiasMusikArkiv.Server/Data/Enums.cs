using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FiasMusikArkiv.Server.Data
{
    public enum GenreCode
    {
        [Description("Polska")] Polska,
        [Description("Slängpolska")] Slangpolska,
        [Description("Schottis")] Schottis,
        [Description("Hambo")] Hambo,
        [Description("Masurkka")] Masurkka,
    }
}
