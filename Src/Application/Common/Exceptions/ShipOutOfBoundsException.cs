using System;

namespace Ofx.Battleship.Application.Common.Exceptions
{
    public class ShipOutOfBoundsException : Exception
    {
        public ShipOutOfBoundsException(string name, int value, int max) 
            : base($"Ships {name} dimension ({value}) is outside the bounds ({max}) of the board.")
        {
        }
    }
}
