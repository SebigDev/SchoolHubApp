using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolHubProfiles.Application.Services.Payments;
using SchoolHubProfiles.Core.DTOs.Payments;

namespace SchoolHubProfiles.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentAppServices _paymentAppServices;
        public PaymentController(IPaymentAppServices paymentAppServices)
        {
            _paymentAppServices = paymentAppServices;
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveAllPayment()
        {
            try
            {
                var allPayment = await _paymentAppServices.RetrieveAllPayments();
                return Ok(allPayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveAllPaymentsByStudent(long studentId)
        {
            try
            {
                var allPayment = await _paymentAppServices.RetrieveAllPaymentsByStudent(studentId);
                return Ok(allPayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveAmountByClassIdAndFeeType(long classId, int feeType)
        {
            try
            {
                var amount = await _paymentAppServices.RetrieveAmountByClassIdAndFeeType(classId, feeType);
                return Ok(amount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(PaymentResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrievePaymentById(long Id)
        {
            try
            {
                var allPayment = await _paymentAppServices.RetrievePaymentById(Id);
                return Ok(allPayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(AmountDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CheckAmountByClassIdAndFeeType(long classId, int feeType)
        {
            try
            {
                var allPayment = await _paymentAppServices.RetrieveAmountByClassAndFeeType(classId, feeType);
                return Ok(allPayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(PaymentResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrievePaymentByType(int paymentType)
        {
            try
            {
                var allPayment = await _paymentAppServices.RetrievePaymentByType(paymentType);
                return Ok(allPayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AmountDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RetrieveAllAmount()
        {
            try
            {
                var allPayment = await _paymentAppServices.RetrieveAllAmount();
                return Ok(allPayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAmount([FromBody] CreateAmountDto createAmountDto)
        {
            try
            {
                var amount = await _paymentAppServices.CreateAmount(createAmountDto);
                if (amount < 1)
                    return BadRequest("Amount Creation Failed");
                return Ok(amount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateAmount([FromBody] AmountDto amountDto)
        {
            try
            {
                var amount = await _paymentAppServices.UpdateAmount(amountDto);
                if (amount == false)
                    return BadRequest("Amount Update Failed");
                return Ok(amount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(PaymentResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> MakePayment([FromBody] PaymentRequest request)
        {
            try
            {
                var nPayment = await _paymentAppServices.MakePayment(request);
                if (nPayment == null)
                    return BadRequest("Payment Transaction Failed");
                return Ok(nPayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
