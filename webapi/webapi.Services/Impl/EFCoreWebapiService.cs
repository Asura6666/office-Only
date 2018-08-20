using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using webapi.Repository;
using webapi.Repository.Entity;
using webapi.Services.Dto;

namespace webapi.Services.Impl
{
    public class EFCoreWebapiService : IWebapiService
    {
        private readonly webapiDbContext WebapiDbContext;
        public EFCoreWebapiService(webapiDbContext webapiDbContext)
        {
            this.WebapiDbContext = webapiDbContext;
        }

        public JObject CreateGood(CreateGoodDto dto) {
            JObject jo = new JObject();
            if (dto.Number == "" || dto.Number.Length > 100)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品编号不合法,无法添加!";
                return jo;
            }
            else if (dto.Name == "" || dto.Name.Length > 100)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品名称不合法,无法添加!";
                return jo;
            }
            else if (dto.Price <= 0m)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品价格不合法,无法添加!";
                return jo;
            }
            else if (dto.TagsId.Count() > 5)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品标签数超过五个,无法添加!";
                return jo;
            }
            else if(dto.Description.Length>2000)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品描述超过2000个字符,无法添加!";
                return jo;
            }
            var isRepeat = WebapiDbContext.Goods.Where(o => o.Number == dto.Number).FirstOrDefault();
            if (isRepeat != null)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品编号重复,无法添加!";
                return jo;
            }
            dto.Price = System.Decimal.Round(System.Decimal.Floor(dto.Price * 100) / 100, 2);
            WebapiDbContext.Goods.Add(new Repository.Entity.Good() {
                Number = dto.Number,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                State = "待上架",
                Is_deleted = "否",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now
            });
            WebapiDbContext.SaveChanges();
            var length = dto.TagsId.Length;
            for (var i = 0; i < length; i++)
            {
                var result = WebapiDbContext.Goods.Where(o => o.Number == dto.Number).Select(o => new CreateGoodDto
                {
                    Id = o.Id
                }).FirstOrDefault();
                WebapiDbContext.GoodTags.Add(new Repository.Entity.Goodtags()
                {
                    Tag_id = dto.TagsId[i],
                    Good_id = result.Id
                });
            }
            WebapiDbContext.SaveChanges();
            jo["stateCode"] = 200;
            jo["message"] = "success!";
            return jo;
        }

        public JObject DeleteGood(DeleteGoodDto dto)
        {
            JObject jo = new JObject();
            var isExist = WebapiDbContext.Goods.Where(o => o.Id == dto.Id).FirstOrDefault();
            if (isExist == null)
            {
                isExist = WebapiDbContext.Goods.Where(o => o.Number == dto.Number).FirstOrDefault();
                if (isExist == null)
                {
                    jo["stateCode"] = 400;
                    jo["message"] = "商品不存在,无法删除!";
                    return jo;
                }
                else
                {
                    if (isExist.State == "待上架" || isExist.State == "已下架")
                    {
                        isExist.Is_deleted = "是";
                        WebapiDbContext.SaveChanges();
                        jo["stateCode"] = 200;
                        jo["message"] = "success!";
                        return jo;
                    }
                    jo["stateCode"] = 400;
                    jo["message"] = "商品已上架,无法删除!";
                    return jo;
                }
            }
            else
            {
                if (isExist.State == "待上架" || isExist.State == "已下架")
                {
                    isExist.Is_deleted = "是";
                    WebapiDbContext.SaveChanges();
                    jo["stateCode"] = 200;
                    jo["message"] = "success!";
                    return jo;
                }
                jo["stateCode"] = 400;
                jo["message"] = "商品已上架,无法删除!";
                return jo;
            }
        }

        public JObject AlterGood(AlterGoodDto dto)
        {
            JObject jo = new JObject();
            var isExist = WebapiDbContext.Goods.Where(o => o.Id == dto.Id).FirstOrDefault();
            if (isExist == null)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品不存在,无法修改!";
                return jo;
            }
            else if (dto.Description.Length > 2000)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品描述大于2000字符,无法修改!";
                return jo;
            }
            else if (dto.Name == "" || dto.Name.Length > 100)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品名称不合法,无法修改!";
                return jo;
            }
            else if (dto.Price<=0m)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品价格不合法,无法修改!";
                return jo;
            }
            else if (dto.TagsId.Count() > 5)
            {
                jo["stateCode"] = 400;
                jo["message"] = "商品标签数超过五个,无法修改!";
                return jo;
            }
            else
            {
                dto.Price = System.Decimal.Round(System.Decimal.Floor(dto.Price * 100) / 100, 2);
                var result = WebapiDbContext.GoodTags.Where(o => o.Good_id == dto.Id);
                foreach (Repository.Entity.Goodtags temp in result)
                {
                    WebapiDbContext.Remove(temp);
                }
                WebapiDbContext.SaveChanges();

                isExist.Name = dto.Name;
                isExist.Price = dto.Price;
                isExist.Description = dto.Description;
                var length = dto.TagsId.Length;
                for (var i = 0; i < length; i++)
                {
                    WebapiDbContext.GoodTags.Add(new Repository.Entity.Goodtags()
                    {
                        Tag_id = dto.TagsId[i],
                        Good_id = dto.Id
                    });
                }
                WebapiDbContext.SaveChanges();
                jo["stateCode"] = 200;
                jo["message"] = "success!";
                return jo;
            }
        }

        public JObject OnlineGood(OnlineGoodDto dto)
        {
            JObject jo = new JObject();
            var length = dto.IdList.Count;
            for (var i = 0; i < length; i++)
            {
                long temp = dto.IdList[i];
                var result = WebapiDbContext.Goods.Where(o => o.Id == temp).FirstOrDefault();
                if (result != null && result.Is_deleted=="否")
                {
                    result.State = "已上架";
                    WebapiDbContext.SaveChanges();
                }
            }
            jo["stateCode"] = 200;
            jo["message"] = "success!";
            return jo;
        }

        public JObject OfflineGood(OnlineGoodDto dto)
        {
            JObject jo = new JObject();
            var length = dto.IdList.Count;
            for (var i = 0; i < length; i++)
            {
                long temp = dto.IdList[i];
                var result = WebapiDbContext.Goods.Where(o => o.Id == temp).FirstOrDefault();
                if (result != null && result.Is_deleted == "否" && result.State=="已上架")
                {
                    result.State = "已下架";
                    WebapiDbContext.SaveChanges();
                }
            }
            jo["stateCode"] = 200;
            jo["message"] = "success!";
            return jo;
        }

        public JObject ListGood(ListReceiveDto dto)
        {
            JObject jo = new JObject();
            JArray container = new JArray();
            string name = dto.Name;
            string tag = dto.Tag;
            string state = dto.State;
            List<ListGoodDto> TempList = new List<ListGoodDto>();
            Regex NumberType = new Regex("^[0-9]*$");

            if (tag != "" && state != "")
            {
                var tagId = WebapiDbContext.Tags.Where(o => o.Tag_name == tag).FirstOrDefault().Id;
                if (name != null && name != "" && NumberType.IsMatch(name))
                {
                    TempList = WebapiDbContext.GoodTags.Where(o => o.Tag_id == tagId).Join(
                        WebapiDbContext.Goods, o => o.Good_id, p => p.Id, (o, p) => new ListGoodDto
                        {
                            Name = p.Name,
                            Number = p.Number,
                            Price = p.Price,
                            State = p.State,
                            Created_at = p.Created_at,
                            Updated_at = p.Updated_at
                        }).Where(o => o.Number == name && o.State == state)
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).ToList();
                }
                else if (name != null && name != "")
                {
                    TempList = WebapiDbContext.GoodTags.Where(o => o.Tag_id == tagId).Join(
                        WebapiDbContext.Goods, o => o.Good_id, p => p.Id, (o, p) => new ListGoodDto
                        {
                            Name = p.Name,
                            Number = p.Number,
                            Price = p.Price,
                            State = p.State,
                            Created_at = p.Created_at,
                            Updated_at = p.Updated_at
                        }).Where(o => o.Name.Contains(name) && o.State == state)
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).ToList();

                    if (TempList.Count == 0)
                    {
                        PinYin py = new PinYin();
                        TempList = WebapiDbContext.GoodTags.Where(o => o.Tag_id == tagId).Join(
                        WebapiDbContext.Goods, o => o.Good_id, p => p.Id, (o, p) => new ListGoodDto
                        {
                            Name = p.Name,
                            Number = p.Number,
                            Price = p.Price,
                            State = p.State,
                            Created_at = p.Created_at,
                            Updated_at = p.Updated_at
                        }).Where(o => py.GetPinyin(o.Name).Replace(" ", "").Trim().Contains(name.Replace(" ", "").Trim().ToLower()) && o.State == state)
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).ToList();
                    }
                }
                else
                {
                    TempList = WebapiDbContext.GoodTags.Where(o => o.Tag_id == tagId).Join(
                        WebapiDbContext.Goods, o => o.Good_id, p => p.Id, (o, p) => new ListGoodDto
                        {
                            Name = p.Name,
                            Number = p.Number,
                            Price = p.Price,
                            State = p.State,
                            Created_at = p.Created_at,
                            Updated_at = p.Updated_at
                        }).Where(o => o.State == state)
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).ToList();
                }
            }
            else if (tag == "" && state != "")
            {
                if (name != null && name != "" && NumberType.IsMatch(name))
                {
                    TempList = WebapiDbContext.Goods.Where(o => o.Number == name && o.State == state)
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).Select(o => new ListGoodDto
                        {
                            Name = o.Name,
                            Number = o.Number,
                            Price = o.Price,
                            State = o.State,
                            Created_at = o.Created_at,
                            Updated_at = o.Updated_at
                        }).ToList();
                }
                else if (name != null && name != "")
                {
                    TempList = WebapiDbContext.Goods.Where(o => o.Name.Contains(name) && o.State == state)
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).Select(o => new ListGoodDto
                        {
                            Name = o.Name,
                            Number = o.Number,
                            Price = o.Price,
                            State = o.State,
                            Created_at = o.Created_at,
                            Updated_at = o.Updated_at
                        }).ToList();

                    if (TempList.Count == 0)
                    {
                        PinYin py = new PinYin();
                        TempList = WebapiDbContext.Goods.Where(o => py.GetPinyin(o.Name).Replace(" ", "").Trim().Contains(name.Replace(" ", "").Trim().ToLower()) && o.State == state)
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).Select(o => new ListGoodDto
                        {
                            Name = o.Name,
                            Number = o.Number,
                            Price = o.Price,
                            State = o.State,
                            Created_at = o.Created_at,
                            Updated_at = o.Updated_at
                        }).ToList();
                    }
                }
                else
                {
                    TempList = WebapiDbContext.Goods.Where(o => o.State == state)
                           .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                           .Take(dto.PageSize).Select(o => new ListGoodDto
                           {
                               Name = o.Name,
                               Number = o.Number,
                               Price = o.Price,
                               State = o.State,
                               Created_at = o.Created_at,
                               Updated_at = o.Updated_at
                           }).ToList();
                }
            }
            else if (tag != "" && state == "")
            {
                var tagId = WebapiDbContext.Tags.Where(o => o.Tag_name == tag).FirstOrDefault().Id;
                if (name != null && name != "" && NumberType.IsMatch(name))
                {
                    TempList = WebapiDbContext.GoodTags.Where(o => o.Tag_id == tagId).Join(
                        WebapiDbContext.Goods, o => o.Good_id, p => p.Id, (o, p) => new ListGoodDto
                        {
                            Name = p.Name,
                            Number = p.Number,
                            Price = p.Price,
                            State = p.State,
                            Created_at = p.Created_at,
                            Updated_at = p.Updated_at
                        }).Where(o => o.Number == name)
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).ToList();
                }
                else if (name != null && name != "")
                {
                    TempList = WebapiDbContext.GoodTags.Where(o => o.Tag_id == tagId).Join(
                        WebapiDbContext.Goods, o => o.Good_id, p => p.Id, (o, p) => new ListGoodDto
                        {
                            Name = p.Name,
                            Number = p.Number,
                            Price = p.Price,
                            State = p.State,
                            Created_at = p.Created_at,
                            Updated_at = p.Updated_at
                        }).Where(o => o.Name.Contains(name))
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).ToList();

                    if (TempList.Count == 0)
                    {
                        PinYin py = new PinYin();
                        TempList = WebapiDbContext.GoodTags.Where(o => o.Tag_id == tagId).Join(
                        WebapiDbContext.Goods, o => o.Good_id, p => p.Id, (o, p) => new ListGoodDto
                        {
                            Name = p.Name,
                            Number = p.Number,
                            Price = p.Price,
                            State = p.State,
                            Created_at = p.Created_at,
                            Updated_at = p.Updated_at
                        }).Where(o => py.GetPinyin(o.Name).Replace(" ", "").Trim().Contains(name.Replace(" ", "").Trim().ToLower()))
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).ToList();
                    }
                }
                else
                {
                    TempList = WebapiDbContext.GoodTags.Where(o => o.Tag_id == tagId).Join(
                        WebapiDbContext.Goods, o => o.Good_id, p => p.Id, (o, p) => new ListGoodDto
                        {
                            Name = p.Name,
                            Number = p.Number,
                            Price = p.Price,
                            State = p.State,
                            Created_at = p.Created_at,
                            Updated_at = p.Updated_at
                        }).OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).ToList();
                }
            }
            else
            {
                if (name != null && name != "" && NumberType.IsMatch(name))
                {
                    TempList = WebapiDbContext.Goods.Where(o => o.Number == name)
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).Select(o => new ListGoodDto
                        {
                            Name = o.Name,
                            Number = o.Number,
                            Price = o.Price,
                            State = o.State,
                            Created_at = o.Created_at,
                            Updated_at = o.Updated_at
                        }).ToList();
                }
                else if (name != null && name != "")
                {
                    TempList = WebapiDbContext.Goods.Where(o => o.Name.Contains(name))
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).Select(o => new ListGoodDto
                        {
                            Name = o.Name,
                            Number = o.Number,
                            Price = o.Price,
                            State = o.State,
                            Created_at = o.Created_at,
                            Updated_at = o.Updated_at
                        }).ToList();

                    if (TempList.Count == 0)
                    {
                        PinYin py = new PinYin();
                        TempList = WebapiDbContext.Goods.Where(o => py.GetPinyin(o.Name).Replace(" ", "").Trim().Contains(name.Replace(" ", "").Trim().ToLower()))
                        .OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).Select(o => new ListGoodDto
                        {
                            Name = o.Name,
                            Number = o.Number,
                            Price = o.Price,
                            State = o.State,
                            Created_at = o.Created_at,
                            Updated_at = o.Updated_at
                        }).ToList();
                    }
                }
                else
                {
                    TempList = WebapiDbContext.Goods.OrderByDescending(o => o.Updated_at).Skip((dto.CurrentPage - 1) * dto.PageSize)
                        .Take(dto.PageSize).Select(o => new ListGoodDto
                        {
                            Name = o.Name,
                            Number = o.Number,
                            Price = o.Price,
                            State = o.State,
                            Created_at = o.Created_at,
                            Updated_at = o.Updated_at
                        }).ToList();
                }
            }

            foreach (ListGoodDto temp in TempList)
            {
                JObject j = new JObject()
                {
                    ["Name"] = temp.Name,
                    ["Number"] = temp.Number,
                    ["Price"] = temp.Price,
                    ["State"] = temp.State,
                    ["Created_at"] = temp.Created_at,
                    ["Updated_at"] = temp.Updated_at,
                };
                container.Add(j);
            }
            jo["stateCode"] = 200;
            jo["message"] = container;
            return jo;
        }

        public JObject ShowGoodDetail(ShowGoodDetailDto dto)
        {
            JObject jo = new JObject();
            var result = WebapiDbContext.Goods.Where(o => o.Id == dto.Id).Select(o => new ShowGoodDetailDto()
            {
                Id=o.Id,
                Number=o.Number,
                Name=o.Name,
                Price=o.Price,
                Description=o.Description,
                State=o.State,
                Created_at=o.Created_at,
                Updated_at=o.Updated_at
            }).ToList();
            List<string> tagName=new List<string>();
            List<long> tagsId = WebapiDbContext.GoodTags.Where(o => o.Good_id == dto.Id).Select(o => o.Tag_id).ToList();
            for (int i = 0; i < tagsId.Count; i++)
            {
                var resultT = WebapiDbContext.Tags.Where(o => o.Id == tagsId[i]).FirstOrDefault().Tag_name;
                tagName.Add(resultT);
            }
            JArray ja = new JArray();
            foreach (string temp in tagName)
            {
                ja.Add(temp);
            }
            JObject j = new JObject
            {
                ["Id"] = result[0].Id,
                ["Number"] = result[0].Number,
                ["Name"] = result[0].Name,
                ["Price"] = result[0].Price,
                ["Description"] = result[0].Description,
                ["State"] = result[0].State,
                ["Created_at"] = result[0].Created_at,
                ["Updated_at"] = result[0].Updated_at,
                ["tagNameList"] = ja
            };
            jo["stateCode"] = 200;
            jo["message"] = j;
            return jo;
        }

        public JObject CreateTag(CreateTagDto dto)
        {
            JObject jo = new JObject();
            if (dto.Tag_name.Length > 100)
            {
                jo["stateCode"] = 400;
                jo["message"] = "标签名长度大于100个字符,无法添加!";
                return jo;
            }
            else if (dto.Tag_name == "")
            {
                jo["stateCode"] = 400;
                jo["message"] = "标签名不能为空,无法添加!";
                return jo;
            }
            var isRepeat = WebapiDbContext.Tags.Where(o => o.Tag_name == dto.Tag_name).FirstOrDefault();
            if (isRepeat != null)
            {
                jo["stateCode"] = 400;
                jo["message"] = "存在重复标签名,无法添加!";
                return jo;
            }
            else
            {
                WebapiDbContext.Tags.Add(new Repository.Entity.Tag()
                {
                    Tag_name = dto.Tag_name,
                    Is_deleted = "否",
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now
                });
                WebapiDbContext.SaveChanges();
                jo["stateCode"] = 200;
                jo["message"] = "success!";
                return jo;
            }
        }

        public JObject DeleteTag(DeleteTagDto dto) {
            JObject jo = new JObject();
            var isExist = WebapiDbContext.Tags.Where(o => o.Id == dto.Tag_id).FirstOrDefault();
            if (isExist == null)
            {
                jo["stateCode"] = 400;
                jo["message"] = "标签名不存在,无法删除!";
                return jo;
            }
            else
            {
                isExist.Is_deleted = "是";
                WebapiDbContext.SaveChanges();
                jo["stateCode"] = 200;
                jo["message"] = "success!";
                return jo;
            }
        }

        public JObject AlterTag(AlterTagDto dto)
        {
            JObject jo = new JObject();
            if (dto.Tag_name.Length > 100)
            {
                jo["stateCode"] = 400;
                jo["message"] = "标签名长度大于100个字符,无法修改!";
                return jo;
            }
            else if (dto.Tag_name == "")
            {
                jo["stateCode"] = 400;
                jo["message"] = "标签名不能为空,无修改!";
                return jo;
            }
            var isExist = WebapiDbContext.Tags.Where(o => o.Id == dto.Tag_id).FirstOrDefault();
            if (isExist == null)
            {
                jo["stateCode"] = 400;
                jo["message"] = "标签名不存在,无法修改!";
                return jo;
            }
            else
            {
                var result= WebapiDbContext.Tags.Where(o => o.Tag_name == dto.Tag_name).FirstOrDefault();
                if (result != null)
                {
                    jo["stateCode"] = 400;
                    jo["message"] = "标签名重复,无法修改!";
                    return jo;
                }
                isExist.Tag_name = dto.Tag_name;
                WebapiDbContext.SaveChanges();
                jo["stateCode"] = 200;
                jo["message"] = "success!";
                return jo;
            }
        }

        public JObject ListTag()
        {
            JObject jo = new JObject();
            var result = WebapiDbContext.Tags.Where(o => o.Is_deleted == "否").Select(o => new ListTagDto()
            {
                Tag_id = o.Id,
                Tag_name = o.Tag_name
            }).ToList();
            JArray ja = new JArray();
            foreach (ListTagDto temp in result)
            {
                JObject j = new JObject();
                j["tagId"] = temp.Tag_id;
                j["tagName"] = temp.Tag_name;
                ja.Add(j);
            }
            jo["stateCode"] = 200;
            jo["message"] = ja;
            return jo;
        }
    }
}
