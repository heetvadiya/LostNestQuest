using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LostNestQuestService.Contracts
{
    [ServiceContract]
    public interface ILostNestQuestService
    {
        [OperationContract]
        string ReportLostItem(LostItem lostItem);

        [OperationContract]
        string ReportFoundItem(FoundItem foundItem);

        [OperationContract]
        List<LostItem> GetAllLostItems();

        [OperationContract]
        List<FoundItem> GetAllFoundItems();

        [OperationContract]
        string DeleteFoundItem(int foundItemId);
        [OperationContract]
        string DeleteLostItem(int foundItemId);

        [OperationContract]
        string UpdateFoundItem(FoundItem foundItem);

        [OperationContract]
        string UpdateLostItem(LostItem lostItem);

        [OperationContract]
        FoundItem GetFoundItemById(int itemId);

        [OperationContract]
        LostItem GetLostItemById(int itemId);
    }

    [DataContract]
    public class LostItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string ContactNumber { get; set; }
    }

    [DataContract]
    public class FoundItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public byte[] Image { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string ContactNumber { get; set; }
    }
}
