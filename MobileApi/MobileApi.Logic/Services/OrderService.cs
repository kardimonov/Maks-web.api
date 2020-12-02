using MobileApi.Data.Interfaces;
using MobileApi.Data.Models;
using MobileApi.Logic.Interfaces;
using MobileApi.Logic.Models;
using System;

namespace MobileApi.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repo;

        public OrderService(IOrderRepository repository)
        {
            repo = repository;
        }

        public TResult AddInetMobileOrder(TOrder order)
        {
            var result = new TResult() { type = "error", value = "" };
            try
            {
                repo.AddInetMobileOrder(order);

                result.type = "success";
            }
            catch (Exception ex)
            {
                result.value = ex.Message;
            }
            return result;
        }

        public TResult AddInetMobileOrder2(string smsText, string comment)
        {
            var result = new TResult() { type = "error", value = "" };

            repo.AddInetMobileOrder2(smsText, comment);

            result.type = "success";
            result.value = smsText;

            return result;
        }

        public TResult AddInetMobileOrder3()
        {
            var result = new TResult() { type = "error", value = "" };
            return result;
        }
    }
}