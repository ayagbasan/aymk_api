using System;
using System.Linq;
using System.Linq.Expressions;
using aymk_database.Database;
using aymk_database.Repository.Base;
using aymk_database.Repository.Interface;
using System.Collections.Generic;
using aymk_library.Library.Models;
using aymk_library.Library.Util;
using System.Text;

namespace aymk_database.Repository
{
    public class AccountBL : RepositoryBase<Account>, IAccountBL
    {
        private IAccountBL accountBL;
        
        public aymkResponse GetAllData(int id)
        {
            try
            {
                accountBL = new AccountBL();
                List<string> inc = new List<string>();
                inc.Add("Exchange.CatalogExchange");
                inc.Add("Alert.CatalogMarket.CatalogExchange");              
                

                return new aymkResponse(accountBL.Get(p => p.id == id, inc));
            }
            catch (System.Exception ex)
            {
                return new aymkResponse(aymkError.GetError, "aymk_api.database.account", ex);
            }
        }

        public aymkResponse Login(string username, string password)
        {
            try
            {
                accountBL = new AccountBL();
                aymkResponse response = accountBL.Get(p => p.username == username && p.password == password);
                if(response.IsSuccess)
                {
                    if(response.Data!=null)
                    {
                       
                        return response;
                    }
                    else
                    {
                        return  new aymkResponse(aymkError.UsernamePasswordWrong, "aymk_api.database.account");
                    }
                }
                else
                {
                    return  new aymkResponse(aymkError.UserNotFound, "aymk_api.database.account");
                }
            }
            catch (System.Exception ex)
            {
                return new aymkResponse(aymkError.GeneralError, "aymk_api.database.account", ex);
            }
        }

        public aymkResponse Register(Account item)
        {
            try
            {
                // general validation
                aymkResponse validateAccount = isValidAccount(item);
                if (!validateAccount.IsSuccess)
                    return validateAccount;

                accountBL = new AccountBL();

                aymkResponse response = accountBL.Add(item);
                if (response.IsSuccess)
                {
                    return response;                    
                }
                else
                {
                    if(response.Detail.Contains("UQ_Account_Email"))
                        return new aymkResponse(aymkError.Register_Email_Exist);
                    else if(response.Detail.Contains("UQ_Account_Username"))
                        return new aymkResponse(aymkError.Register_Username_Exist);
                    else
                    {
                        response.Message = aymkError.RegisterError.GetDescription();
                        return response;
                    }
                    
                }
            }
            catch (System.Exception ex)
            {
                return new aymkResponse(aymkError.GeneralError, "aymk_api.database.account", ex);
            }
        }
        
        aymkResponse isValidAccount(Account item)
        {
             
            StringBuilder errors = new StringBuilder();


            if (item.email==null || string.IsNullOrEmpty(item.email.Trim()))
                errors.AppendLine("E-mail is empty");
            else
                item.email = item.email.Trim().ToLower();

            if (item.username==null || string.IsNullOrEmpty(item.username.Trim()))
                errors.AppendLine("Username is empty");
            else
                item.username = item.username.Trim().ToLower();

            if (item.password == null || string.IsNullOrEmpty(item.password.Trim()))
                errors.AppendLine("Password is empty");
            else
                item.password = item.password.Trim();


            item.active = item.active ?? true;
            item.alertEmail = item.active ?? true;
            item.alertNotification = item.active ?? false;
            item.alertSms = item.active ?? false;
            item.tradeEmail = item.active ?? true;
            item.tradeNotification = item.active ?? false;
            item.tradeSms = item.active ?? false; 
            item.createdAt = DateTime.Now;

            if (string.IsNullOrEmpty(errors.ToString()))
                return new aymkResponse(item);
            else
                return new aymkResponse(aymkError.Register_Not_Valid, errors.ToString()); 
        }


    }
}
