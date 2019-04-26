using SchoolHubProfiles.Core.DTOs.Payments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHubProfiles.Application.Services.Payments
{
    public interface IPaymentAppServices : IDisposable
    {
        Task<PaymentResponse> MakePayment(PaymentRequest request);
        Task<PaymentResponse> RetrievePaymentByType(int paymentType);
        Task<PaymentResponse> RetrievePaymentById(long id);
        Task<IEnumerable<PaymentResponse>> RetrieveAllPayments();
        Task<IEnumerable<PaymentResponse>> RetrieveAllPaymentsByStudent(long studentId);
        Task<PaymentResponse> RetrieveFeeByTypeAndStudentId(int feeType, long studentId);
        Task<bool> CancelPayment(long paymentId);

        //Amount
        Task<int> CreateAmount(CreateAmountDto createAmountDto);
        Task<decimal> RetrieveAmountByClassIdAndFeeType(long classId, int feeType);
        Task<IEnumerable<AmountDto>> RetrieveAllAmount();
        Task<AmountDto> RetrieveAmountByClassAndFeeType(long classid, int feeType);
        Task<bool> UpdateAmount(AmountDto amountDto);

        //Summation Of All Payment
        Task<PaymentSummaryResponse> RetrievePaymentSummary(long studentId);
    }
}
