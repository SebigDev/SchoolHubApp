using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SchoolHubProfiles.Core.Context;
using SchoolHubProfiles.Core.DTOs.Payments;
using SchoolHubProfiles.Core.Models.Payments;
using SchoolHub.Core.Enums;
using SchoolHub.Core.Extensions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolHubProfiles.Application.Services.Payments
{
    public class PaymentAppServices : IPaymentAppServices
    {
        private readonly SchoolHubDbContext _schoolhubDbContext;
        public PaymentAppServices(SchoolHubDbContext schoolHubDbContext)
        {
            _schoolhubDbContext = schoolHubDbContext;
        }
       

        public async Task<PaymentResponse> MakePayment(PaymentRequest request)
        {
            Payment payment;
            PaymentResponse response;
            if (request == null)
                throw new ArgumentNullException(nameof(request));

          using(var _transaction = _schoolhubDbContext.Database.BeginTransaction())
          {
                var nFeeType = (int)request.FeeType;
                payment = new Payment
                {
                    StudentId = request.StudentId,
                    ClassId = request.ClassId,
                    FeeType = request.FeeType,
                    PaymentDate = DateTime.UtcNow,
                    PaymentRefernceId = $"SchoolHub-{Guid.NewGuid().ToString().Substring(5)}",
                    PaymentStatus = PaymentStatus.Paid,
                    Amount = RetrieveAmountByClassIdAndFeeType(request.ClassId,nFeeType).Result
                };

                   await _schoolhubDbContext.Payment.AddAsync(payment);
                   await _schoolhubDbContext.SaveChangesAsync();
                response = new PaymentResponse
                {
                    PayChannel = new PayChannel
                    {
                        ClassId = payment.ClassId,
                        StudentId = payment.StudentId
                    },
                    PaymentReport = new PaymentReport
                    {
                        PaymentRefrenceId = payment.PaymentRefernceId,
                        FeeType = payment.FeeType.GetDescription(),
                        PaymentStatus = payment.PaymentStatus.GetDescription(),
                        Amount = payment.Amount,
                    }
                };
                _transaction.Commit();

                return response;
          }
        }

        public Task<bool> CancelPayment(long paymentId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PaymentResponse>> RetrieveAllPayments()
        {
            var studentFees = new List<PaymentResponse>();

            var allFees = await _schoolhubDbContext.Payment.ToListAsync();
            studentFees.AddRange(allFees.OrderBy(o => o.PaymentDate).Select(s => new PaymentResponse()
            {
                PayChannel = new PayChannel
                {
                    ClassId = s.ClassId,
                    StudentId = s.StudentId
                },
                PaymentReport = new PaymentReport
                {
                    PaymentRefrenceId = s.PaymentRefernceId,
                    FeeType = s.FeeType.GetDescription(),
                    PaymentStatus = s.PaymentStatus.GetDescription(),
                    Amount = s.Amount
                }
            }));
            return studentFees;
        }

        public async Task<IEnumerable<PaymentResponse>> RetrieveAllPaymentsByStudent(long studentId)
        {
            var studentFees = new List<PaymentResponse>();

            if (studentId < 1)
                throw new ArgumentNullException(nameof(studentId));

            var allFees = await _schoolhubDbContext.Payment.Where(s => s.StudentId == studentId).ToListAsync();
            studentFees.AddRange(allFees.OrderBy(o => o.PaymentDate).Select(s => new PaymentResponse()
            {
                PayChannel = new PayChannel
                {
                    ClassId = s.ClassId,
                    StudentId = s.StudentId
                },
                PaymentReport = new PaymentReport
                {
                    PaymentRefrenceId = s.PaymentRefernceId,
                    FeeType = s.FeeType.GetDescription(),
                    PaymentStatus = s.PaymentStatus.GetDescription(),
                    Amount = s.Amount,
                }
            }));
            return studentFees;
        }

        public async Task<AmountDto> RetrieveAmountByClassAndFeeType(long classId, int feeType)
        {
            var nfeeType = (FeeType)feeType;
            var fee = new AmountDto();

            if (classId < 1)
                throw new ArgumentNullException(nameof(classId));

            if (feeType < 1)
                throw new ArgumentNullException(nameof(feeType));

            var myFee = await _schoolhubDbContext.Amount.Where(s => s.ClassId== classId && s.FeeType == nfeeType).FirstOrDefaultAsync();
            if (myFee != null)
            {
                fee.Id = myFee.Id;
                fee.ClassId = myFee.ClassId;
                fee.FeeAmount = myFee.FeeAmount;
                fee.FeeType = myFee.FeeType.GetDescription();
                fee.Id = myFee.Id;
                fee.DateCreated = myFee.DateCreated;
                fee.UpdatedDate = myFee.UpdatedDate;

                return fee;
            }
            return null;
  
        }


        public async Task<PaymentResponse> RetrievePaymentById(long id)
        {
            var fee = new PaymentResponse();

            if (id < 1)
                throw new ArgumentNullException(nameof(id));

            var myFee = await _schoolhubDbContext.Payment.Where(s => s.Id == id).FirstOrDefaultAsync();
            fee = new PaymentResponse
            {
                PayChannel = new PayChannel
                {
                    ClassId = myFee.ClassId,
                    StudentId = myFee.StudentId
                },
                PaymentReport = new PaymentReport
                {
                    PaymentRefrenceId = myFee.PaymentRefernceId,
                    FeeType = myFee.FeeType.GetDescription(),
                    PaymentStatus = myFee.PaymentStatus.GetDescription(),
                    Amount = myFee.Amount
                }
            };
            return fee;
        }

        public async Task<PaymentResponse> RetrievePaymentByType(int paymentType)
        {
            PaymentResponse fee = new PaymentResponse();
            var feeType = (FeeType)paymentType;

            if (paymentType < 1)
                throw new ArgumentNullException(nameof(paymentType));

            var myFee = await _schoolhubDbContext.Payment.Where(s => s.FeeType == feeType).FirstOrDefaultAsync();
            fee = new PaymentResponse
            {
                PayChannel = new PayChannel
                {
                    ClassId = myFee.ClassId,
                    StudentId = myFee.StudentId
                },
                PaymentReport = new PaymentReport
                {
                    PaymentRefrenceId = myFee.PaymentRefernceId,
                    FeeType = myFee.FeeType.GetDescription(),
                    PaymentStatus = myFee.PaymentStatus.GetDescription(),
                    Amount = myFee.Amount
                }
            };
            return fee;
        }

        #region Amount

        public async Task<int> CreateAmount(CreateAmountDto createAmountDto)
        {
            Amount amount;
            if (createAmountDto == null)
                throw new ArgumentNullException(nameof(createAmountDto));

            amount = new Amount
            {
                ClassId = createAmountDto.ClassId,
                FeeAmount = createAmountDto.FeeAmount,
                FeeType = createAmountDto.FeeType,
                DateCreated = DateTime.UtcNow,
            };
            await _schoolhubDbContext.Amount.AddAsync(amount);
            await _schoolhubDbContext.SaveChangesAsync();
            return amount.Id;
        }

        public async Task<decimal> RetrieveAmountByClassIdAndFeeType(long classId, int feeType)
        {
            if (classId < 1)
                throw new ArgumentNullException(nameof(classId));
            if (feeType < 1)
                throw new ArgumentNullException(nameof(feeType));

            var nFeeType = (FeeType)feeType;
            var amount = await _schoolhubDbContext.Amount.Where(a => a.ClassId == classId && a.FeeType == nFeeType).FirstOrDefaultAsync();
            if (amount == null)
                return 0;
            return amount.FeeAmount;
        }

        public async Task<IEnumerable<AmountDto>> RetrieveAllAmount()
        {
            var allAmountDto = new List<AmountDto>();
            var allAmount = await _schoolhubDbContext.Amount.ToListAsync();
            allAmountDto.AddRange(allAmount.OrderBy(o => o.DateCreated).Select(a => new AmountDto()
            {
                Id = a.Id,
                ClassId = a.ClassId,
                FeeAmount = a.FeeAmount,
                FeeType = a.FeeType.GetDescription(),
                DateCreated = DateTime.UtcNow,
            }));
            return allAmountDto;
        }

        public async Task<bool> UpdateAmount(AmountDto amountDto)
        {
            if (amountDto == null)
                throw new ArgumentNullException(nameof(amountDto));

            var amountUpdate = await _schoolhubDbContext.Amount.Where(s => s.Id == amountDto.Id).FirstOrDefaultAsync();
            if (amountUpdate != null)
            {
                amountUpdate.Id = amountDto.Id;
                amountUpdate.FeeAmount = amountDto.FeeAmount;
                amountUpdate.UpdatedDate = DateTime.UtcNow;

                _schoolhubDbContext.Entry(amountUpdate).State = EntityState.Modified;
                await _schoolhubDbContext.SaveChangesAsync();
                return true;
            }
            return false;
               
        }
        #endregion

        #region PaymentSummary

        public async Task<PaymentSummaryResponse> RetrievePaymentSummary(long studentId)
        {
            var allPaySum = new List<PaymentType>();

            var allPayment = await RetrieveAllPaymentsByStudent(studentId);
            foreach (var payment in allPayment)
            {
                var payDetail = new PaymentType
                {
                    PayAmount = payment.PaymentReport.Amount,
                    PayName = payment.PaymentReport.FeeType
                };
                allPaySum.Add(payDetail);
            }
            var student = await _schoolhubDbContext.Student.FirstOrDefaultAsync(s => s.Id == studentId);
            var studentClassMap = await _schoolhubDbContext.StudentClassMap.FirstOrDefaultAsync(s => s.StudentId == studentId);
            var studentClass = await _schoolhubDbContext.ClassName.Where(x => x.Id == studentClassMap.ClassId).FirstOrDefaultAsync();
            var summaryResponse = new PaymentSummaryResponse
            {
                StudentFullname = $"{student.Firstname} {student.Lastname}",
                Classname = studentClass.Name,
                PaymentType = allPaySum,
                SumTotal = allPaySum.Sum(s =>s.PayAmount)
            };

            return summaryResponse;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PaymentAppServices()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

      
        #endregion
    }
}
