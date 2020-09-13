using System;
using Elk.Core;
using Kavenegar;
using GolPooch.CrossCutting;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GolPooch.SmsGateway
{
    public class SmsGatway : ISmsGatway
    {
        private readonly KavenegarApi _smsGateWay;

        public SmsGatway()
        {
            _smsGateWay = new KavenegarApi(GlobalVariables.SmsGatewaySettings.ApiKey);
        }

        public async Task<IResponse<bool>> SendAsync(string receiver, string text)
        {
            var response = new Response<bool>();
            try
            {
                var sendResult = await _smsGateWay.Send(GlobalVariables.SmsGatewaySettings.Sender, receiver, text);
                if (sendResult.Messageid > 0)
                {
                    response.Result = true;
                    response.IsSuccessful = true;
                    response.Message = SmsGatewayMessage.Success;
                }
                else
                {
                    response.Result = false;
                    response.Message = sendResult.StatusText;
                }

                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.Message = SmsGatewayMessage.Exception;
                return response;
            }
        }

        public async Task<IResponse<List<bool>>> SendMultipleAsync(List<string> receiver, string text)
        {
            var response = new Response<List<bool>> { IsSuccessful = true, Message = SmsGatewayMessage.Success };
            try
            {
                var sendResult = await _smsGateWay.Send(GlobalVariables.SmsGatewaySettings.Sender, receiver, text);

                var result = new List<bool>();
                foreach (var item in sendResult)
                {
                    if (item.Messageid > 0) result.Add(true);
                    else result.Add(false);
                }

                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                FileLoger.Error(e);
                response.IsSuccessful = false;
                response.Message = SmsGatewayMessage.Exception;
                return response;
            }
        }
    }
}