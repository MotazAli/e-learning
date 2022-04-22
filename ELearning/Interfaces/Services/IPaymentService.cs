using ELearning.Dto;

namespace ELearning.Interfaces.Services;
public interface IPaymentService 
{
    public Task<PaymentResponseDto> CreatePaymentAsync(PaymentRequestDto paymentRequestDto);
}

