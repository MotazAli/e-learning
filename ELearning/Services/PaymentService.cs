using ELearning.Dto;
using ELearning.Interfaces.Services;
using ELearning.Providers.Payments;

namespace ELearning.Services;
public class PaymentService : IPaymentService
{
    private readonly StripeService _stripeService;

    public PaymentService(StripeService stripeService) 
    {
        _stripeService = stripeService;
    }


    public async Task<PaymentResponseDto> CreatePaymentAsync(PaymentRequestDto paymentRequestDto)
    {
        var result = await _stripeService.CreatePayment(
            paymentRequestDto.CardNumber,
            paymentRequestDto.ExpireMonth,
            paymentRequestDto.ExpireYear,
            paymentRequestDto.Cvc,
            paymentRequestDto.Amount,
            paymentRequestDto.Currency,
            paymentRequestDto.Description
            );


        if (result.Reference != string.Empty)
            return new PaymentResponseDto(200,result.Message,result.Reference);

        return new PaymentResponseDto(504, result.Message, result.Reference);


    }
}

