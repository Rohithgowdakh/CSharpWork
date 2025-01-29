using StockExchange.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.BusinessLayer
{
    public class DataBridge
    {
        private  ExchangeRepository _exchangeRepository;
        public DataBridge(ExchangeRepository exchangeRepository)
        {
            _exchangeRepository = exchangeRepository;
        }
        public void AddFunds(decimal amount)
        {
            _exchangeRepository.AddUsersFunds(amount);
        }
        public void GetHoldingsBasedOnEmail()
        {
            _exchangeRepository.GetHoldingsBasedOnEmail();
        }
        public void ProcessSellShareTransaction(string company,Dictionary<string,double> companies)
        {
            _exchangeRepository.ProcessSellShareTransaction(company, companies);
        }
        public void GetFundsDetailBasedOnEmail(string company, Dictionary<string, double> companies)
        {
            _exchangeRepository.GetFundsDetailBasedOnEmail(company, companies);
        }
        public void GetUserHoldingData(Dictionary<string, double> companies)
        {
            _exchangeRepository.GetUserHoldingData(companies);
        }
        public void GetUserTransactions()
        {
            _exchangeRepository.GetUserTransactions();
        }
        public void CreateUserHoldingsTable()
        {
            _exchangeRepository.CreateUserHoldingsTable();
        }
        public void CreateTransactionTable()
        {
            _exchangeRepository.CreateTransactionTable();
        }
        public void CreateLoginTable()
        {
            _exchangeRepository.CreateLoginTable();
        }
        public string ProcessLoginBasedOnPassword(string email, string password)
        {
            return _exchangeRepository.ProcessLoginBasedOnPassword(email, password);
        }
    }
}
