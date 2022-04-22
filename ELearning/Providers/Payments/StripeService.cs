using Stripe;

namespace ELearning.Providers.Payments;


public class StripeService 
{
    
    public StripeService(string apiKey)
    {
        StripeConfiguration.ApiKey = apiKey;
    }


    public async Task<StripeResult> CreatePayment(
        string CardNumber,
        long ExpireMonth,
        long ExpireYear,
        string Cvc, 
        long Amount,
        string Currency,
        string Description)
    {


        try
        {
            var cardOptions = new PaymentMethodCardOptions
            {
                Number = CardNumber,
                ExpMonth = ExpireMonth,
                ExpYear = ExpireYear,
                Cvc = Cvc
            };


            var paymentMethodCreateOptions = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = cardOptions,
            };
            var paymentMethodService = new PaymentMethodService();
            var resultPaymentMethod = await paymentMethodService.CreateAsync(paymentMethodCreateOptions);


            var tokenCreateOptions = new TokenCreateOptions
            {
                Card = resultPaymentMethod.Id,
            };
            var tokenService = new TokenService();
            var resultToken = await tokenService.CreateAsync(tokenCreateOptions);

            var chargeCreateOptions = new ChargeCreateOptions
            {
                Amount = Amount,
                Currency = Currency,
                Source = resultToken.Id,
                Description = Description,
            };
            var chargeService = new ChargeService();
            var chargeCreateResult = await chargeService.CreateAsync(chargeCreateOptions);

            if (chargeCreateResult.Status == "succeeded") 
                return new StripeResult(chargeCreateResult.Id, chargeCreateResult.Status);

            return new StripeResult("", chargeCreateResult.Status);
        }
        catch (Exception ex)
        {
            return new StripeResult("", ex.Message);
        }

        
    }

}


public record struct StripeResult(string Reference, string Message);

