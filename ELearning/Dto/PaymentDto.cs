namespace ELearning.Dto;


public record struct PaymentRequestDto(string CardNumber,
        long ExpireMonth,
        long ExpireYear,
        string Cvc,
        long Amount,
        string Currency,
        string Description);
public record struct PaymentResponseDto(int StatusCode,string message, string ReferenceNumber);



