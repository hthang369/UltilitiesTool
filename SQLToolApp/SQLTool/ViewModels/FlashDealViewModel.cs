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

namespace SQLTool.ViewModels
{
    [POCOViewModel]
    public class FlashDealViewModel : BasePopupViewModel
    {
        private string _textContent;
        public string TextContent
        {
            get => _textContent;
            set => SetProperty(ref _textContent, value);
        }
        private DateTime _dteContent;
        public DateTime dteContent
        {
            get => _dteContent;
            set => SetProperty(ref _dteContent, value);
        }
        private List<CardProductInfo> _lstProducts;
        private FlashDealView view;

        public List<CardProductInfo> lstProducts
        {
            get => _lstProducts;
            set => SetProperty(ref _lstProducts, value);
        }
        public ICommand searchCommand { get; set; }
        public FlashDealViewModel()
        {
            dteContent = DateTime.Today;
            lstProducts = new List<CardProductInfo>();
            searchCommand = new RelayCommand<object>((x) => { return true; }, (x) => SearchFlashDeal(x));
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
                    else if(objItems.Count < 100)
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
    }

    public class CardProductInfo
    {
        public long id { get; set; }
        public long product_id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public double final_price { get; set; }
        public string url_key { get; set; }
    }
}