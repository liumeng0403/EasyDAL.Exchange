﻿using MyDAL.Core;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace MyDAL.Tools
{
    public sealed partial class XHttp
    {

        // 下一步改进  自动url encode ,  cookie 携带支持

        private byte[] Buffer { get; set; }
        private Stream RequestStream { get; set; }
        private HttpWebRequest Request { get; set; }
        private bool ResponseFlag { get; set; }
        private string Result { get; set; }
        private bool TimeoutFlag { get; set; }
        private int TimeoutTime { get; set; }
        private int RetryCount { get; set; }
        private int WaitSleep { get; set; }
        private int TrySleep { get; set; }

        private void RemoteNew(Action<XHttp, string> action)
        {
            var reNum = 0;
            for (var i = 0; i < this.RetryCount; i++)
            {
                try
                {
                    //
                    var uri = URL;
                    this.Request = WebRequest.Create(uri) as HttpWebRequest;
                    this.Request.KeepAlive = false;
                    this.Request.Method = this.RequestMethod;
                    this.Request.Credentials = CredentialCache.DefaultCredentials;
                    if (Token.IsNotBlank())
                    {
                        Request.Headers.Add(HttpRequestHeader.Authorization, $" Bearer {Token}");
                    }
                    if (this.RequestMethod.Equals("POST", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Buffer = Encoding.UTF8.GetBytes(this.JsonContent);
                        this.Request.ContentLength = this.Buffer.Length;
                        this.Request.ContentType = "application/json";
                        this.RequestStream = this.Request.GetRequestStream();
                        this.RequestStream.Write(this.Buffer, 0, this.Buffer.Length);
                        this.RequestStream.Close();
                    }

                    //
                    this.Request.BeginGetResponse((arr) =>
                    {
                        var state = arr.AsyncState as XHttp;
                        var response = state.Request.EndGetResponse(arr) as HttpWebResponse;
                        var respStream = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                        action(state, respStream.ReadToEnd());
                        respStream.Close();
                        response.Close();
                    }, this);
                    break;
                }
                catch (AggregateException ae)
                {
                    string errMsg = string.Empty;
                    ae.Flatten().Handle(e =>
                    {
                        errMsg += e.Message;
                        return true;
                    });
                    reNum++;
                    if (reNum == this.RetryCount)
                    {
                        throw XConfig.EC.Exception(XConfig.EC._116, $"请求失败!请求次数:{this.RetryCount}次,失败原因:{errMsg};Url:{this.URL}");
                    }
                    Thread.Sleep(this.TrySleep);
                    continue;
                }
                catch (Exception ex)
                {
                    reNum++;
                    if (reNum == this.RetryCount)
                    {
                        throw XConfig.EC.Exception(XConfig.EC._117, $"请求失败!请求次数:{this.RetryCount}次,失败原因:{ex.Message};Url:{this.URL}");
                    }
                    Thread.Sleep(this.TrySleep);
                    continue;
                }
            }
        }
        private void SetResult(XHttp state, string jsonData)
        {
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                state.Result = jsonData;
                state.ResponseFlag = true;
            }
        }
        private void GetRemoteDataX()
        {
            //
            if (string.IsNullOrWhiteSpace(this.URL))
            {
                throw XConfig.EC.Exception(XConfig.EC._118, "requestURL, 未赋值 !");
            }

            // 
            RemoteNew(SetResult);

            //
            var timeNum = 0;
            while (true)
            {
                if (ResponseFlag)
                {
                    break;
                }
                if (TimeoutFlag)
                {
                    if (!XConfig.RI.IsDebug) 
                    {
                        throw XConfig.EC.Exception(XConfig.EC._097, $"请求超时!超时时间:{TimeoutTime / 1000}S;Url:{this.URL}");
                    }
                    else // 调试模式下 5分钟超时
                    {
                        Thread.Sleep(TimeSpan.FromMinutes(5));
                        throw XConfig.EC.Exception(XConfig.EC._097, $"调试超时!Url:{this.URL}");
                    }
                }
                timeNum += WaitSleep;
                if (timeNum >= TimeoutTime)
                {
                    TimeoutFlag = true;
                }
                Thread.Sleep(WaitSleep);
            }
        }

        private string RequestMethod { get; set; }
        private string URL { get; set; }
        private string JsonContent { get; set; }
        private string Token { get; set; }

        /***********************************************************************************************************************************************/

        public string GET(string requestURL)
        {
            this.RequestMethod = "GET";
            this.URL = requestURL;
            this.GetRemoteDataX();
            return this.Result;
        }
        public string GET(string requestURL, string oAuth2Token)
        {
            this.RequestMethod = "GET";
            this.URL = requestURL;
            this.Token = oAuth2Token;
            this.GetRemoteDataX();
            return this.Result;
        }
        public string POST(string requestURL)
        {
            this.RequestMethod = "POST";
            this.URL = requestURL;
            this.GetRemoteDataX();
            return this.Result;
        }
        public string POST(string requestURL, string jsonRequestData)
        {
            this.RequestMethod = "POST";
            this.URL = requestURL;
            this.JsonContent = jsonRequestData;
            this.GetRemoteDataX();
            return this.Result;
        }
        public string POST(string requestURL, string jsonRequestData, string oAuth2Token)
        {
            this.RequestMethod = "POST";
            this.URL = requestURL;
            this.JsonContent = jsonRequestData;
            this.Token = oAuth2Token;
            this.GetRemoteDataX();
            return this.Result;
        }
    }
}
