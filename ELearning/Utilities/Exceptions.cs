namespace ELearning.Utilities;

[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message) { }

    public NotFoundException(string message, Exception inner)
        : base(message, inner) { }
}

[Serializable]
public class ServiceUnavailableException : Exception
{


    public ServiceUnavailableException(string message)
        : base(message) { }

    public ServiceUnavailableException(string message, Exception inner)
        : base(message, inner) { }
}