using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.ConstrainedExecution;

namespace ConsumerToDealerDB
{
    public class ConfluentApprovedConsumerRepository
    {
        MobilMainanDealerEntities _dbContext = new MobilMainanDealerEntities();
        public void WriteTopicKeyValue(ConsumeResult<string, string> cr)
        {
            var isNew = false;
            //var KafkaKey = cr.Key;
            var splitKey = cr.Key.Split('_');
            if (splitKey.Length < 4) return;
            var Id = long.Parse(splitKey[1]);
            NewPurchaseData obj = JsonConvert.DeserializeObject<NewPurchaseData>(cr.Value);

            var item = _dbContext.NewPurchaseDatas.Where(x => x.Id == Id).FirstOrDefault();
            if (item == null)
            {
                item = new NewPurchaseData();
                isNew = true;
            }
            item.Id = Id;
            item.UserId = obj.UserId;
            item.VehicleId = obj.VehicleId;
            item.ApprovalState = obj.ApprovalState;

            if (isNew) _dbContext.NewPurchaseDatas.Add(item);

            _dbContext.SaveChanges();
        }
    }
}
