using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace consumer_kafka_tam
{
    public class ConfluentApprovedConsumerRepository
    {
        MobilMainanDealerEntities _dbContext = new MobilMainanDealerEntities();
        public void WriteTopicKeyValue(ConsumeResult<string,string> cr)
        {
            var KafkaKey = long.Parse(cr.Key);
            var item = _dbContext.VehiclePurchaseApprovements.Where(x => x.Topic == cr.Topic && x.KafkaKey == KafkaKey).FirstOrDefault();
            if(item == null) item = new VehiclePurchaseApprovement();
            item.Topic = cr.Topic;
            item.KafkaKey = KafkaKey;
            item.Value = cr.Value;
            item.DtmUpd = DateTime.Now;
            if (item.Id == 0) _dbContext.VehiclePurchaseApprovements.Add(item);
            _dbContext.SaveChanges();
        }
    }
}
