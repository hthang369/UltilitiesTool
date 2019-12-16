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
        public ICommand searchCommand { get; set; }
        public FlashDealViewModel()
        {
            searchCommand = new RelayCommand<object>((x) => { return true; }, (x) => SearchFlashDeal(x));
        }

        private async void SearchFlashDeal(object x)
        {
            Services.RestApiHelper apiHelper = new Services.RestApiHelper("https://api.sendo.vn", "");
            string json = @"{""page"":1,""limit"":30,""special_status"":0,""category_group_id"":0,""buy_limit"":0,""shoptype"":0,""is_new_app"":0,""tag"":0,""slot"":""2019-12-05 19:00:00""}";
            var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            //Services.ApiResult result = await apiHelper.Run("flash-deal/ajax-product/", Services.HttpMethodType.Post, obj);
            //if(result.statusCode == System.Net.HttpStatusCode.OK)
            //{
            //    JObject objData = result.data;
            //}
            Services.ApiResult result = await apiHelper.AddDefaultClientHeader().RunClient("flash-deal/category-group?special_status=0", Services.HttpMethodType.Get);
            if (result.statusCode == System.Net.HttpStatusCode.OK)
            {
                JObject objData = result.data;
            }

        }
    }
}