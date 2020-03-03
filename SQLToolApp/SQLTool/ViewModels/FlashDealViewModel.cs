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
            apiHelperSaleOrder = new Services.RestApiHelper("https://checkout.sendo.vn/api/checkout", "");
            string cookie = @"_ga=GA1.2.215617723.1542848381; _omappvp=kbDxYwUsfQzQl1QsdzOKuVDmMVEOiTxYCqSVfZTrErv377dNeTmHUzOMKABPQjuT7tcqolQURecH9eZdUpI6ClVpmQqCkYf2; __utmv=147100981.|1=Thang%20Tran=Old%20Buyer=1; _fbp=fb.1.1549853646954.532844823; mp_7ee9ac5438d5ed68c57319eb6bf3821f_mixpanel=%7B%22distinct_id%22%3A%20%221673f7e4f37275-0ad571820ce56f-4313362-100200-1673f7e4f381dc%22%2C%22%24device_id%22%3A%20%221673f7e4f37275-0ad571820ce56f-4313362-100200-1673f7e4f381dc%22%2C%22%24initial_referrer%22%3A%20%22https%3A%2F%2Fwww.sendo.vn%2Fthong-tin-tai-khoan%2Fdia-chi-nhan-hang%2F%22%2C%22%24initial_referring_domain%22%3A%20%22www.sendo.vn%22%7D; _fbc=fb.1.1555486842076.IwAR3cFSCmHHF-5_Nr_APBAUVTuFF29hRGQB2ZntnwNYl075yVS8yPwRb6jPU; tracking_id=6c863eca9d7b46d8b8de17fd62f77076; search_algo=algo5; _gcl_au=1.1.959991754.1565760962; WZRK_G=b1b31e4735c64531ae5041cb64af10dc; fbm_387062634759025=base_domain=.sendo.vn; browserid=a861addf4979a177b5a5d447886e74bb; access_token=Rsd8KVvDRXWs7GKdtPN%2F5%2F9KIbLNPHb6J5fOliSLfV3eXxurN1BZSriadaOGDsM8ryBUQnRl%2Bc6vpPCM5QADOtkUCykqibXoDL3BFC4GKkLH7OdlNmIwYdg0OoIz2laxjgwUn9jpxQRw28%2FQDJc1e9%2F7E5EDCu%2F%2By09gpLDTois%3D; __stp={'visit':'returning','uuid':'7d8aa6fa-101f-4543-a785-a0bee1b9bae9','ck':'2018781610'}; listing_algo=algo13; aff_last_click=Affiliate|accesstrade|www_sendo_vn|nczKMaiJW4mS4q30nuTDY8OtdT9hOFO6EH3ETWthRSYoXHaR; _strs=1578896292.gclid=CjwKCAiAyeTxBRBvEiwAuM8dnZUTYBSiC69KSMMnf4iOdTKuP5yZrvY5jRzFpTPoGIqc7aj82idJBRoCwTMQAvD_BwE; _gac_UA-32891946-1=1.1578896300.CjwKCAiAyeTxBRBvEiwAuM8dnZUTYBSiC69KSMMnf4iOdTKuP5yZrvY5jRzFpTPoGIqc7aj82idJBRoCwTMQAvD_BwE; _gcl_aw=GCL.1580807099.CjwKCAiAyeTxBRBvEiwAuM8dnZUTYBSiC69KSMMnf4iOdTKuP5yZrvY5jRzFpTPoGIqc7aj82idJBRoCwTMQAvD_BwE; _gcl_dc=GCL.1580807099.CjwKCAiAyeTxBRBvEiwAuM8dnZUTYBSiC69KSMMnf4iOdTKuP5yZrvY5jRzFpTPoGIqc7aj82idJBRoCwTMQAvD_BwE; __utmz=147100981.1580807483.98.30.utmcsr=google|utmdsid=CjwKCAiAyeTxBRBvEiwAuM8dnZUTYBSiC69KSMMnf4iOdTKuP5yZrvY5jRzFpTPoGIqc7aj82idJBRoCwTMQAvD_BwE|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); _gac_UA-32891946-6=1.1580808520.CjwKCAiAyeTxBRBvEiwAuM8dnZUTYBSiC69KSMMnf4iOdTKuP5yZrvY5jRzFpTPoGIqc7aj82idJBRoCwTMQAvD_BwE; SSID=8j43gmds5nggqsik3ri39queg4; __utmc=147100981; __stdf=0; _gid=GA1.2.93775835.1582790483; __utma=147100981.215617723.1542848381.1582790483.1582852848.114; closed_banner=1; __stgeo='0'; fbsr_387062634759025=y6MywP3H_TJDe9pdiDfrEeJgfI0OOPBJOtaUvCk4JpQ.eyJ1c2VyX2lkIjoiMjMwOTM3Mzc2NTgzODYzOSIsImNvZGUiOiJBUUFJdjA2WUdua1ZlRVl1V0VxbmdlSDNiT2ZSVDg0TE85c0RhQlRKNEw2WTlGMlczZ01KTi1GS0NUdHFBUm5HeGtXbXpNdmluS2lfRlJ0N21ocVFCbE5faEV3akZYVHBwQUNLVGFFQm04ZlRjX0lSSGVid3hmY05LWDJMUFhZcDdWNjl6ODFQQWVGWjlFNE93X0FyMENSQmZKbmRzYk9Nek9yUEhrdlJvZGNsNHpQUzVuelVYZ2ZQM2pBaGlDTXl0cVVueC1pQy1BRHVJTktpXzZCWEppVFo4Si10LW9UWExfMHhnSWFZTVhjYWoxRGZ4YUhmb1lHNTBNUTVXNjZfU2NLbmtQRkdlWlFUcFQ2bDFkRHFRVjJDb3dvUjQ2ZkNQRlhJclJQWWJfcTdlXzVaUlc0bUFWaEo3UWxrUVo0NW1rLWtMNHpSVERjdGNFNnU3S0oxVmt0TiIsIm9hdXRoX3Rva2VuIjoiRUFBRmdDQXJaQXczRUJBSk9vQjNaQmE3RzQ5djFsSnFUVkRSNGVaQ2tUc295MkJxZXRiYmI1WkNSQkxjMHNZUkY4NEhmUDVmRGNucU5sdk9IWkN4VzJoQXRQVTZURzltWkFIT0VzUDY2czFQdWtTcktPNVFaQ3MzNk9IZ0tESE45UVBFT0drSWtNY0p0Uzh4RkYyWFpBOHlhM2hOUGlON2tpblVnWkFzTzZKMmpHWkNZbVpCMTM0T1ZxdXVaQ0xMcGpaQnRIazdKT1VjWkJTR0VaQjZRd1pEWkQiLCJhbGdvcml0aG0iOiJITUFDLVNIQTI1NiIsImlzc3VlZF9hdCI6MTU4Mjg1MzY2MX0; __sts={'sid':1582852920418,'tx':1582853673951,'url':'https%3A%2F%2Fwww.sendo.vn%2Flo-nuong-mishio-mk178-25l-mau-den-23349416.html%3FfromItem%3D51742092%26source_block_id%3DFS_products%26source_page_id%3DFS_popular%26source_info%3D4','pet':1582853673951,'set':1582852920418,'pUrl':'https%3A%2F%2Fwww.sendo.vn%2Floa-soundmax-soundbar-sb202-led-rgb-hang-chinh-hang-19806035.html%3FfromItem%3D51686654%26source_block_id%3DFS_products%26source_page_id%3DFS_popular%26source_info%3D2','pPet':1582852954744,'pTx':1582852954744}; __utmb=147100981.21.7.1582854073091";
            apiHelperCheckPrice.AddRequestHeader("cookie", cookie);
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
                    double dbPrice = Convert.ToDouble(((JObject)result.result).SelectToken("data.final_price"));
                    if(dbPrice <= 1000)
                    {
                        string body = string.Concat(@"{'shop_id':419322,'current_products':[{'product_id':23349416,'name':'Lò nướng Mishio MK178 25L – Màu đen','categories':'1/2/1108/3147/3148','source_page_id':'FS_popular','source_block_id':'FS_products','source_info':'4','price':",
                                dbPrice,",'final_price':", dbPrice,", 'weight':12000,'qty':1,'hash':'f6f0070476326013830a8a703a319e18','image':'https://media3.scdn.vn/img3/2019/11_5/cHCJFY.jpg','image_resize':{'image':'https://media3.scdn.vn/img3/2019/11_5/cHCJFY.jpg','image_50x50':'https://media3.scdn.vn/img3/2019/11_5/cHCJFY_simg_02d57e_50x50_maxb.jpg','image_100x100':'https://media3.scdn.vn/img3/2019/11_5/cHCJFY_simg_3a7818_100x100_maxb.jpg'},'sku':'3148_23349416','sku_user':'MK178','category_id':1108,'checkout_weight':12000,'cat_path':'lo-nuong-mishio-mk178-25l-mau-den-23349416.html','extended_shipping_package':{'is_using_in_day':true},'unit_id':1,'is_valid':true,'product_type':1}],'current_address_id':12058791,'current_carrier':'ecom_shipping_dispatch_cptc','current_payment_method':{'method':'cod_payment'},'current_voucher':{'voucher_code':'','old_voucher_code':'','voucher_value':0,'is_shop_voucher':false,'voucher_campaign_code':'','sub_total':0,'payment_method':'','error':'','is_enable_captcha':false,'captcha_response':'','enable_suggest_voucher':false,'tracking_order_source':0,'suggested_message':'','redeemed_at':0,'voucher_wallet_list':[]},'sendo_platform':'desktop2','ignore_invalid_product':-1,'product_hashes':['f6f0070476326013830a8a703a319e18'],'version':3.1,'order_type':1}");
                        var objBody = JsonConvert.DeserializeObject<Dictionary<string, object>>(body);
                        await apiHelperSaleOrder.Run("save-order", Services.HttpMethodType.Post, objBody);
                    }
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