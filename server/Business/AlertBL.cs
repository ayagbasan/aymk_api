using aymk_engine.Model;
using aymk_engine.Model.Custom;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Simplify.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static aymk_engine.Util.Constants;

namespace aymk_engine.Business
{
    public class AlertBL
    {
        private static AlertBL instance;
        private static Repository<Account> repository;
        public static AlertBL GetInstance()
        {
            if (instance == null)
            {
                instance = new AlertBL();
                repository = new Repository<Account>();
            }

            return instance;
        }


        public async Task Run()
        {

            decimal deltaPercentange = 0;
            Alert currentAlert;
            Response response = GetAlerts();
            if (response.IsSuccess)
            {
                List<AlertResponse> alertList = response.Data as List<AlertResponse>;

                foreach (var item in alertList)
                {
                    currentAlert = item.Alert;
                    if (currentAlert.ValueType == AlertValueType.price.ToString())
                    {
                        if (currentAlert.BelowValue != -1)
                        {
                            if (currentAlert.BelowValue >= item.Market.Ticker.Last)
                            {
                                await SendMailAsync(item, NotificationTitleType.PRICE_BELOW);
                            }
                        }

                        if (currentAlert.AboveValue != -1)
                        {
                            if (currentAlert.AboveValue <= item.Market.Ticker.Last)
                            {
                                await SendMailAsync(item, NotificationTitleType.PRICE_ABOWE);
                            }
                        }
                    }
                    else if (currentAlert.ValueType == AlertValueType.percent.ToString())
                    {
                        deltaPercentange = 100 - (currentAlert.SavedValue / item.Market.Ticker.Last) * 100;

                        if (currentAlert.BelowValue != -1)
                        {

                            if (deltaPercentange < 0 && deltaPercentange < currentAlert.BelowValue)
                            {
                                await SendMailAsync(item, NotificationTitleType.PERCENTANGE_BELOW, deltaPercentange);
                            }
                        }

                        if (currentAlert.AboveValue != -1)
                        {
                            if (deltaPercentange > 0 && deltaPercentange > currentAlert.AboveValue)
                            {
                                await SendMailAsync(item, NotificationTitleType.PERCENTANGE_ABOWE, deltaPercentange);
                            }
                        }
                    }


                }
            }
        }

        Response GetAlerts()
        {

            try
            {
                string pBson = @"{'_id' : 1,'username' : 1,'email' : 1,'tradeSms' : 1,'tradeNotification' : 1,'tradeEmail' : 1,'alertSms' : 1,'alertNotification' : 1,'alertEmail' : 1,'phoneNumber' : 1,'surname' : 1,'name' : 1, Alert: '$alerts','Market':'$market','Exchange':'$exchange'} }";

                var m = BsonSerializer.Deserialize<BsonDocument>(pBson);

                var aggregate = repository.collection.Aggregate()
                                         .Unwind(i => i.Alerts)
                                         .Match("{'alerts.active':true}")
                                         .Lookup("catalogmarkets", "alerts.marketId", "_id", "market")
                                         .Unwind(i => i["market"])
                                         .Lookup("catalogexchanges", "market.exchangeId", "_id", "exchange")
                                         .Unwind(i => i["exchange"])
                                         .Project(m)
                                         .ToList();



                dynamic alerts = BsonSerializer.Deserialize<List<AlertResponse>>(aggregate.ToJson());
                return new Response(alerts);

            }
            catch (Exception ex)
            {
                return new Response(ex.Message);
            }
        }




        public Task SendMailAsync(AlertResponse response, NotificationTitleType type, decimal percentange = 0) => Task.Run(() => SendMail(response,type,percentange));

        public void SendMail(AlertResponse response, NotificationTitleType type, decimal percentange = 0)
        {

           

            Notification n = new Notification();
            n.Id = ObjectId.GenerateNewId();
            n.Style = NotificationStyles.INFO.ToString();
            n.AccountId = response.Id;
            n.CreatedAt = DateTime.UtcNow;

            if (type == NotificationTitleType.PRICE_ABOWE || type== NotificationTitleType.PRICE_BELOW)
            {
                n.Message = string.Format("Exchange: {4}{0}Market: {5}{0}{0}Above Price: {1}{0}Saved Price: {2}{0}Below Price: {3}{0}{0}Current Price:{7}{0}{0}Date: {6:dd.MM.yyyy HH:mm} UTC",
                    Environment.NewLine,
                    response.Alert.AboveValue,
                    response.Alert.SavedValue,
                    response.Alert.BelowValue,
                    response.Exchange.ExchangeName,
                    response.Market.Code,
                    DateTime.UtcNow,
                    response.Market.Ticker.Last);
            }
            else if (type == NotificationTitleType.PERCENTANGE_ABOWE || type== NotificationTitleType.PERCENTANGE_BELOW)
            {
                n.Message = string.Format("Exchange: {4}{0}Market: {5}{0}Above Percent: % {1:0.00}{0}Saved Price: {2}{0}Below Percent: % {3:0.00}{0}{0}Current Price:{8}{0}Percentange:% {7:0.00}{0}{0}Date: {6:dd.MM.yyyy HH:mm} UTC",
                    Environment.NewLine,
                    response.Alert.AboveValue,
                    response.Alert.SavedValue,
                    response.Alert.BelowValue,
                    response.Exchange.ExchangeName,
                    response.Market.Code,
                    DateTime.UtcNow,
                    percentange,
                    response.Market.Ticker.Last);
            }

            n.Title = response.Market.Code + " is " + type.ToString();
            n.Type = NotificationTypes.EMAIL.ToString();
            n.Repeat = 1;

            string displayName;
            if (string.IsNullOrEmpty(response.Name) && string.IsNullOrEmpty(response.Surname))
                displayName = response.Email;
            else
                displayName = string.Format("{0} {1}", response.Name, response.Surname);

            Response mailResponse = MailBL.GetInstance().SendMail(response.Email, n.Title, n.Message, displayName);

            if (mailResponse.IsSuccess)
            {
                n.CompletedAt = DateTime.UtcNow;
                n.Status = NotificationStatus.SENT.ToString();
                n.StatusMessage = mailResponse.Message;
            }
            else
            {
                n.Status = NotificationStatus.HASERROR.ToString();
                n.StatusMessage = mailResponse.Message;
            }



            AccountBL.GetInstance().AddNotification(n);


            Console.WriteLine(n.Title+"---"+ DateTime.Now);

            
        }


    }
}
