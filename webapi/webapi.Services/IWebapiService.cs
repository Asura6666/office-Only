using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using webapi.Services.Dto;

namespace webapi.Services
{
    public interface IWebapiService
    {
        JObject CreateGood(CreateGoodDto dto);
        JObject DeleteGood(DeleteGoodDto dto);
        JObject AlterGood(AlterGoodDto dto);
        JObject OnlineGood(OnlineGoodDto dto);
        JObject OfflineGood(OnlineGoodDto dto);
        JObject ListGood(ListReceiveDto dto);
        JObject ShowGoodDetail(ShowGoodDetailDto dto);
        JObject CreateTag(CreateTagDto dto);
        JObject DeleteTag(DeleteTagDto dto);
        JObject AlterTag(AlterTagDto dto);
        JObject ListTag();
    }
}
