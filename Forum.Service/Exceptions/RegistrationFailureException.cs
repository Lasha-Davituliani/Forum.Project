﻿namespace Forum.Service.Exceptions
{
    public class RegistrationFailureException : Exception
    {
        public RegistrationFailureException(string message) : base(message)
        {
        }
    }
}
