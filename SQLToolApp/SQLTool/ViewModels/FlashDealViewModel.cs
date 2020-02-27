using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Http;
using SQLTool.Views;
using System.Windows.Forms;

namespace SQLTool.ViewModels
{
    [POCOViewModel]
    public class FlashDealViewModel : BasePopupViewModel
    {
        private FlashDealView view;
        private Timer runTimer;
        private string _textContent;
        public string TextContent
        {
            get => _textContent;
            set => SetProperty(ref _textContent, value);
        }
        private string _textUrl;
        public string TextUrl
        {
            get => _textUrl;
            set => SetProperty(ref _textUrl, value);
        }
        private string _textTime;
        public string TextTime
        {
            get => _textTime;
            set => SetProperty(ref _textTime, value);
        }
        private DateTime _dteContent;
        public DateTime dteContent
        {
            get => _dteContent;
            set => SetProperty(ref _dteContent, value);
        }
        private List<CardProductInfo> _lstProducts;
        public List<CardProductInfo> lstProducts
        {
            get => _lstProducts;
            set => SetProperty(ref _lstProducts, value);
        }
        private List<RequestInfos> _lstRequestInfos;
        public List<RequestInfos> lstRequestInfos
        {
            get => _lstRequestInfos;
            set => SetProperty(ref _lstRequestInfos, value);
        }
        public ICommand searchCommand { get; set; }
        public ICommand runCommand { get; set; }
        public ICommand stopCommand { get; set; }
        int iTimeTick = 500;
        public FlashDealViewModel()
        {
            dteContent = DateTime.Today;
            runTimer = new Timer();
            runTimer.Enabled = false;
            runTimer.Interval = iTimeTick;
            lstProducts = new List<CardProductInfo>();
            lstRequestInfos = new List<RequestInfos>();
            searchCommand = new RelayCommand<object>((x) => { return true; }, (x) => SearchFlashDeal(x));
            runCommand = new RelayCommand<object>((x) => { return true; }, (x) => RunFlashDeal(x));
            stopCommand = new RelayCommand<object>((x) => { return true; }, (x) => StopFlashDeal(x));
        }

        public FlashDealViewModel(FlashDealView view) : this()
        {
            this.view = view;
        }

        private async void SearchFlashDeal(object x)
        {
            DateTime dtTime = DateTime.Parse(TextContent);
            DateTime dtNow = new DateTime(dteContent.Year, dteContent.Month, dteContent.Day, dtTime.Hour, dtTime.Minute, dtTime.Second);
            Services.RestApiHelper apiHelper = new Services.RestApiHelper("https://api.sendo.vn", "");
            lstProducts = new List<CardProductInfo>();
            int page = 1;
            this.view.dgcResults.ShowLoadingPanel = true;
            while (true)
            {
                string json = string.Concat(@"{", string.Format(@"""page"":{0},""limit"":100,""special_status"":0,""category_group_id"":0,""buy_limit"":0,""shoptype"":0,""is_new_app"":0,""tag"":0,""slot"":""{1}""", page, dtNow.ToString("yyyy-MM-dd HH:mm:ss")), "}");
                var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                Services.ApiResult result = await apiHelper.Run("flash-deal/ajax-product/", Services.HttpMethodType.Post, obj);
                if (result != null && result.statusCode == System.Net.HttpStatusCode.OK)
                {
                    JObject objData = result.data as JObject;
                    JArray objItems = objData.GetValue("products") as JArray;
                    if (objItems.Count == 100)
                    {
                        foreach (JToken item in objItems.Children())
                        {
                            JObject objItem = (item as JObject);
                            if (Convert.ToDouble(objItem.GetValue("final_price")).Equals(1000))
                            {
                                lstProducts.Add(JsonConvert.DeserializeObject<CardProductInfo>(objItem.ToString()));
                            }
                        }
                    }
                    else if (objItems.Count < 100)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
                page++;
            }
            this.view.dgcResults.ShowLoadingPanel = false;
            this.view.dgcResults.RefreshData();
            //Services.ApiResult result = await apiHelper.AddDefaultClientHeader().RunClient("flash-deal/category-group?special_status=0", Services.HttpMethodType.Get);
            //if (result.statusCode == System.Net.HttpStatusCode.OK)
            //{
            //    JObject objData = result.data;
            //}

        }
        Services.RestApiHelper apiHelperCheckPrice;
        Services.RestApiHelper apiHelperSaleOrder;
        DateTime dtCurrent;
        private async void RunFlashDeal(object x)
        {
            apiHelperCheckPrice = new Services.RestApiHelper("https://www.sendo.vn/m/wap_v2/full/san-pham", "");
            apiHelperSaleOrder = new Services.RestApiHelper("https://www.sendo.vn/m/wap_v2/full/san-pham", "");
            apiHelperCheckPrice.AddRequestHeader("cookie", "tracking_id=38a2e70cd8bd428695e60f042163a45a; browserid=c3ee01162d7b98ce500e0a4aa46c4d2a; SSID=v5f2cjb55u1r0baqbq53kjsmu7");
            runTimer.Tick += RunTimer_Tick;
            this.view.processBar.Minimum = 0;
            this.view.processBar.ContentDisplayMode = DevExpress.Xpf.Editors.ContentDisplayMode.Value;
            this.view.processBar.DisplayFormatString = "{0:p}";
            this.view.processBar.IsPercent = true;
            DateTime dtNow = DateTime.Now;
            dtCurrent = DateTime.Parse(TextTime);
            TimeSpan ts = dtCurrent.AddSeconds(-3).Subtract(dtNow);
            TimeSpan ts1 = dtCurrent.AddMinutes(1).Subtract(dtNow);
            this.view.processBar.Maximum = Math.Round(ts1.TotalSeconds);
            if (ts.Ticks >= 0)
            {
                runTimer.Enabled = true;
            }
            lstRequestInfos.Clear();
        }

        private async void RunTimer_Tick(object sender, EventArgs e)
        {
            this.view.processBar.Value += 0.5;
            TimeSpan ts = dtCurrent.AddSeconds(-3).Subtract(DateTime.Now);
            if (ts.TotalSeconds < 0)
            {
                SendoProductResult result = await apiHelperCheckPrice.Run<SendoProductResult>(this.TextUrl, Services.HttpMethodType.Get);
                if (result != null)
                {
                    ((JObject)result.result).SelectToken("data.products_checkout");
                    RequestInfos info = new RequestInfos() { Status = result.statusCode.ToString(), Content = ((JObject)result.status).GetValue("message").ToString() };
                    lstRequestInfos.Add(info);
                }
                this.view.dgvResultRequest.RefreshData();
            }
        }

        private void StopFlashDeal(object x)
        {
            runTimer.Enabled = false;
        }
    }

    public class CardProductInfo
    {
        public long id { get; set; }
        public long product_id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public double final_price { get; set; }
        public string url_key { get; set; }
        public string button_text { get; set; }
    }
    public class RequestInfos : ElementObject
    {
        private string _status;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
        private string _content;
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }
    }
    public class SendoProductResult
    {
        public bool success { get; set; }
        public string message { get; set; }
        public JObject validation { get; set; }
        public JObject status { get; set; }
        public JObject result { get; set; }
        public HttpStatusCode statusCode { get; set; }
    }
}